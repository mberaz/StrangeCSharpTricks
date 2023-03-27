using StrangeCSharpTricks.Excel.Reading;

namespace StrangeCSharpTricks.DictionaryIsTheNewIf.Model.ReadModels
{
    public class ReadByColumnName
    {
        [PropertyColumn("A")]
        public int Id { get; set; }
       
        [PropertyColumnAttribute("B")]
        public string FirstName { get; set; }
        
        [PropertyColumnAttribute("C")]
        public string LastName { get; set; }
       
        [PropertyColumnAttribute("D")]
        public string Email { get; set; }

        [PropertyColumnAttribute("E")]
        public double? Rank { get; set; }
    }
}
