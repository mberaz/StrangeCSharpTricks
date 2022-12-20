using Microsoft.AspNetCore.Mvc;
using StrangeCSharpTricks.DictionaryIsTheNewIf.Model;
using StrangeCSharpTricks.DictionaryIsTheNewIf.Validators;
using System.Collections.Generic;

namespace StrangeCSharpTricks.DictionaryIsTheNewIf.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EntityController : ControllerBase
    {
        private readonly IEntityValidator _entityValidator;

        public EntityController(IEntityValidator entityValidator)
        {
            _entityValidator = entityValidator;
        }

        [HttpPost]
        public int CreateEntity(Dictionary<string, object> values)
        {
            var entity = new Entity
            {
                Name = EntityTemplate.Name,
                Attributes = EntityTemplate.Attributes,
                Values = values,
            };

            var isValid = _entityValidator.Validate(entity);

            return 0;
        }


        Entity EntityTemplate = new Entity
        {
            Name = "contact",
            //Values = new Dictionary<string, object>
            //    {
            //        { "firstName","Donald" },
            //        { "lastName","Duck" },
            //        { "age",10 }
            //    },
            Attributes = new List<Model.Attribute>
                {
                    new Model.Attribute
                    {
                        Name= "firstName",
                        MaxLength= 100,
                        Type = AttributeType.Text,
                        IsRequired= true,
                    },
                    new Model.Attribute
                    {
                        Name= "middleName",
                        Type = AttributeType.Text,
                        IsRequired = false,
                    },
                    new Model.Attribute
                    {
                        Name= "lastName",
                        MaxLength= 100,
                        Type= AttributeType.Text,
                        IsRequired= true,
                    },
                    new Model.Attribute
                    {
                        Name= "age",
                        Type= AttributeType.Number,
                        IsRequired= true,
                    },
                }
        };
    }
}

