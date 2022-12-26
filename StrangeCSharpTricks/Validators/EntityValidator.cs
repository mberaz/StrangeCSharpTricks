using StrangeCSharpTricks.DictionaryIsTheNewIf.Model;
using System;
using System.Collections.Generic;
using Attribute = StrangeCSharpTricks.DictionaryIsTheNewIf.Model.Attribute;

namespace StrangeCSharpTricks.DictionaryIsTheNewIf.Validators
{
    public interface IEntityValidator
    {
        public List<string> Validate(Entity entity);
    }

    public class EntityValidator : IEntityValidator
    {
        Dictionary<AttributeType, Func<object, Attribute, bool>> dictionary = new Dictionary<AttributeType, Func<object, Attribute, bool>>();
        public EntityValidator(IEnumerable<IAttributeValidator> attributeValidators)
        {
            foreach (var item in attributeValidators)
            {
                dictionary.Add(item.Name(), item.Validate);
            }
        }

        public List<string> Validate(Entity entity)
        {
            var errors = new List<string>();

            foreach (var attribute in entity.Attributes)
            {
                var value = entity.Values.ContainsKey(attribute.Name) ? entity.Values[attribute.Name] : null;

                var isValid = dictionary[attribute.Type](value, attribute);
                if (!isValid)
                {
                    errors.Add($"attribute {attribute.Name} is not valid");
                }
            }

            return errors;
        }
    }
}
