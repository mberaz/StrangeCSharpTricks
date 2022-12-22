using StrangeCSharpTricks.DictionaryIsTheNewIf.Model;

namespace StrangeCSharpTricks.DictionaryIsTheNewIf.Validators
{
    public interface IAttributeValidator
    {
        AttributeType Name();
        bool Validate(object value, Attribute attribute);
    }

    public class NumberValidator : IAttributeValidator
    {
        public bool Validate(object value, Attribute attribute)
        {
            if (attribute.IsRequired && value == null)
            {
                return false;
            }

            //min
            //max

            return true;
        }

        public AttributeType Name() => AttributeType.Number;
    }

    public class DecimalValidator : IAttributeValidator
    {
        public bool Validate(object value, Attribute attribute)
        {
            if (attribute.IsRequired && value == null)
            {
                return false;
            }

            //min length
            //max length 

            return true;
        }

        public AttributeType Name() =>AttributeType.Decimal;
    }

     public class TextValidator : IAttributeValidator
    {
        public bool Validate(object value, Attribute attribute)
        {
            if (attribute.IsRequired && value == null)
            {
                return false;
            }

            //min length
            //max length 

            return true;
        }

        public AttributeType Name() => AttributeType.Text;
    }
}
