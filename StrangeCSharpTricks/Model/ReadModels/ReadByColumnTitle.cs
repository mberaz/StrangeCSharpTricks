using StrangeCSharpTricks.Excel.Reading;

namespace StrangeCSharpTricks.DictionaryIsTheNewIf.Model.ReadModels;

public class ReadByColumnTitle
{
    [PropertyTitleName("Id")]
    public int Id { get; set; }
       
    [PropertyTitleName("FirstName")]
    public string FirstName { get; set; }
        
    [PropertyTitleName("LastName")]
    public string LastName { get; set; }
       
    [PropertyTitleName("Email")]
    public string Email { get; set; }

    [PropertyTitleName("Rank")]
    public double? Rank { get; set; }
}