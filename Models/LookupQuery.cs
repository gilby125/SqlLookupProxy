using System.Collections.Generic;

namespace SqlLookupProxy.Models
{
    public class LookupQuery
    {
        public string Name {get;set;}
        public Dictionary<string, string> ReplaceableValues {get;set;}
    }
    public class ReplaceableValue
    {
        public string Name {get;set;}
        public string Value {get;set;}
    }
}