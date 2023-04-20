using System;
using System.Collections.Generic;

namespace WizardMaker.DataDomain.Models;

public class ObjRegistrar<T> where T: IObjectForRegistrar
{
    private Dictionary<string, T> ObjByCanonName = new Dictionary<string, T>();
    private Dictionary<Guid, T>   ObjById        = new Dictionary<Guid, T>();

    public ObjRegistrar()
    {
    }

    public void Register(T obj)
    {
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
