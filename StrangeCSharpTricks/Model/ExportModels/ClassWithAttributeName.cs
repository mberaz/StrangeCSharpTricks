using StrangeCSharpTricks.Excel;

namespace StrangeCSharpTricks.DictionaryIsTheNewIf.Model.ExportModels
{
    public class ClassWithAttributeName
    {
        [ExcelColumnName("id")]
        public int Id { get; set; }
       
        [ExcelColumnName("given_name")]
        public string FirstName { get; set; }
        
        [ExcelColumnName("family_name")]
        public string LastName { get; set; }
       
        [ExcelColumnName("email")]
        public string Email { get; set; }
    }
}
