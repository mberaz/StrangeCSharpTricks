using System.Collections.Generic;

namespace StrangeCSharpTricks.DictionaryIsTheNewIf.Model
{
    public class Entity
    {
        public string Name { get; set; }
        public List<Attribute> Attributes { get; set; }
        public Dictionary<string, object> Values { get; set; }
    }
}
