﻿using System;
using System.Collections.Generic;

namespace WizardMaker.DataDomain.Models
{
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

        public T? LookupByName(string canonName)
        {
            T? result = this.ObjByCanonName[canonName];
            if (result == null)
            {
                // TODO:
                // Based on the intended mode of use, do we prefer (returns-null) or (throws-exception) upon lookup failure?

                // Option I: Return null on lookup failure
                return default(T);

                // Option II: Throw an exception on lookup failure
                //string msg = string.Format("Lookup failed: Did not find {0} with canonical name {1}", typeof(T).ToString(), canonName);
                //throw new ArgumentException(msg);
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
                return default(T);

                // Option II: Throw an exception on lookup failure
                //string msg = string.Format("Lookup failed: Did not find {0} with canonical name {1}", typeof(T).ToString(), canonName);
                //throw new ArgumentException(msg);
            }
            return result;
        }
    }

    public interface IObjectForRegistrar
    {
        public string CanonName { get; }
        public Guid   Id        { get; }
    }
}
