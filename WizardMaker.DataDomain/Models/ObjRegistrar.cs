using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace WizardMaker.DataDomain.Models;

public class ObjRegistrar<T> where T: IObjectForRegistrar
{
    #region Fields and Properties
    private Dictionary<string, T> ObjByCanonName = new Dictionary<string, T>();
    private Dictionary<Guid, T>   ObjById        = new Dictionary<Guid, T>();
    #endregion

    #region Constructors
    public ObjRegistrar()
    {
    }
    #endregion

    #region Methods (various)
    public void Register(T obj)
    {
        if (obj == null)
        {
            string msg = string.Format("ObjRegistrar.Register(): Got null obj");
            throw new ArgumentNullException(msg);
        }

        string canonName = obj.CanonName;
        Guid   id        = obj.Id;

        bool alreadyRegisteredById        = this.ObjById.ContainsKey(id);
        bool alreadyRegisteredByCanonName = this.ObjByCanonName.ContainsKey(canonName);

        if (alreadyRegisteredById || alreadyRegisteredByCanonName)
        {
            string msg = string.Format("Register failed: Was already registered by {0}{1}{2}.",
                alreadyRegisteredById ? "Id" : "",
                (alreadyRegisteredById && alreadyRegisteredByCanonName) ? " and " : "",
                alreadyRegisteredByCanonName ? "Canon Name" : ""
            );
            throw new ArgumentException(msg);
        }
        this.ObjByCanonName[canonName] = obj;
        this.ObjById[id]               = obj;
    }

    public IQueryable<T> AllRegisteredObjs()
    {
        // This may be useful to do LINQ queries upon
        var query = this.ObjByCanonName.Values.AsQueryable();
        return query;
    }

    public IQueryable<string> AllRegisteredCanonNames()
    {
        // This may be useful.  If not, remove it later.
        var query = this.ObjByCanonName.Keys.AsQueryable();
        return query;
    }

    public T? LookupByName(string canonName)
    {
        T? result = this.ObjByCanonName[canonName];
        if (result == null)
        {
            // TODO:
            // Based on the intended mode of use, do we prefer (returns-null) or (throws-exception) upon lookup failure?

            // Option I: Return null on lookup failure
            //return default(T);

            // Option II: Throw an exception on lookup failure
            string msg = string.Format("Lookup failed: Did not find {0} with canonical name {1}", typeof(T).ToString(), canonName);
            throw new ArgumentException(msg);
        }
        return result;
    }

    public T LookupById(Guid id)
    {
        T? result = this.ObjById[id];
        if (result == null)
        {
            // TODO:
            // Based on the intended mode of use, do we prefer (returns-null) or (throws-exception) upon lookup failure?

            // Option I: Return null on lookup failure
            //return default(T);

            // Option II: Throw an exception on lookup failure
            string msg = string.Format("Lookup failed: Did not find {0} with Id {1}", typeof(T).ToString(), id);
            throw new ArgumentException(msg);
        }
        return result;
    }
    #endregion

    #region Static methods
    public static string MakeCanonName(string name)
    {
        // This should suffice for simple sequences of words: "Artes Liberales" and the like.
        // However, many strange inputs are possible.
        // For instance, "Foo! Bar" contains non-word non-whitespace characters (punctuation).
        // On the bright side, "word character" includes the various European letters with accents the the like...
        // 
        // Will need to give this a think, and revisit...

        string trimmed = name.TrimStart().TrimEnd();

        Regex oneOrMoreWords = new Regex(@"^([\S]+)( [\S]+)*$");  // Need to think upon a better approach...
        Match mm = oneOrMoreWords.Match(trimmed);
        if (!mm.Success)
        {
            string msg = string.Format("Got name without one or more words");
            throw new ArgumentException(msg);
        }
        var words = (from cc in mm.Captures select cc.Value).ToList();
        
        var sb = new StringBuilder();
        foreach (string ww in words)
        {
            // The 2nd+ words will start with a space, so we want to get rid of that...
            sb.Append('_');
            sb.Append( ww.TrimStart().TrimEnd() );
        }
        string result = sb.ToString();
        return result;
    }
    #endregion
}

public interface IObjectForRegistrar
{
    /// <summary>
    /// The purpose is to identify (while looking at some JSON) the sort of thing the which an object is,
    /// in a human-readable manner, potentially across multiple similar objects. 
    /// Useful to be able to see at a glance in the JSON what a thing is.
    /// (Example: Two variants of an Ability "Artes Liberales", one being a variant instantiated in another Saga)
    /// </summary>
    public string CanonName { get; }

    /// <summary>
    /// The purpose is to uniquely identify a __particular__ object, as distinct from other similar objects.
    /// Since a Guid is not human-readable, this fulfills a different purpose from CanonName.
    /// </summary>
    public Guid   Id        { get; }
}
