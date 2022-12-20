namespace StrangeCSharpTricks.DictionaryIsTheNewIf.Model
{
    public class Attribute
    {
        public string Name { get; set; }
        public AttributeType Type { get; set; }
        public bool IsRequired { get; set; }
        public int Min { get; set; }
        public int Max { get; set; }
        public int MinLength { get; set; }
        public int MaxLength { get; set; }
    }
}
