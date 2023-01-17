using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StrangeCSharpTricks.DictionaryIsTheNewIf.Model;
using StrangeCSharpTricks.DictionaryIsTheNewIf.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using Attribute = StrangeCSharpTricks.DictionaryIsTheNewIf.Model.Attribute;

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


        /// <response code="201">Returns the newly created item</response>
        /// <response code="400">If the item is null</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created) ]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public int CreateEntity(Dictionary<string, object> values)
        {
            var entity = new Entity
            {
                Name = EntityTemplate.Name,
                Attributes = EntityTemplate.Attributes,
                Values = values,
            };

            var errors = _entityValidator.Validate(entity);

            if (errors.Any())
            {
                throw new Exception(string.Join(", ", errors));
            }
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
            Attributes = new List<Attribute>
                {
                    new  Attribute
                    {
                        Name= "firstName",
                        MaxLength= 100,
                        Type = AttributeType.Text,
                        IsRequired= true,
                    },
                    new Attribute
                    {
                        Name= "middleName",
                        Type = AttributeType.Text,
                        IsRequired = false,
                    },
                    new Attribute
                    {
                        Name= "lastName",
                        MaxLength= 100,
                        Type= AttributeType.Text,
                        IsRequired= true,
                    },
                    new Attribute
                    {
                        Name= "age",
                        Type= AttributeType.Number,
                        IsRequired= true,
                    },
                }
        };
    }
}

