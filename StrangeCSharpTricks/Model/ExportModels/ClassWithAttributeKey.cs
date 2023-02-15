using StrangeCSharpTricks.Excel;

namespace StrangeCSharpTricks.DictionaryIsTheNewIf.Model.ExportModels;

public class ClassWithAttributeKey
{
    [ExcelColumnResourceKey("IdKey")]
    public int Id { get; set; }
       
    [ExcelColumnResourceKey("FirstNameKey")]
    public string FirstName { get; set; }
        
    [ExcelColumnResourceKey("LastNameKey")]
    public string LastName { get; set; }
       
    [ExcelColumnResourceKey("EmailKey")]
    public string Email { get; set; }
}