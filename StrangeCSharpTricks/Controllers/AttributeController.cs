using Microsoft.AspNetCore.Mvc;
using StrangeCSharpTricks.DictionaryIsTheNewIf.Model;
using System.Collections.Generic;
using System.Linq;


namespace StrangeCSharpTricks.DictionaryIsTheNewIf.Controllers
{
    [Route("Attribute")]
    public class AttributeController : Controller
    {
        private List<Attribute> attributes = new List<Attribute>()
        {
            new Attribute{Name="firstName", Type=AttributeType.Text },
            new Attribute{Name="middelName", Type=AttributeType.Text },
            new Attribute{Name="lastName", Type=AttributeType.Text },
            new Attribute{Name="age" , Type=AttributeType.Decimal},
            new Attribute{Name="id", Type=AttributeType.Number },
            new Attribute{Name="isActive", Type=AttributeType.Bool },
            new Attribute{Name="rank" , Type=AttributeType.Number}
        };

        [HttpGet("{name}")]
        public IActionResult GetByName(string name)
        {
            return Ok(attributes.FirstOrDefault(f => f.Name == name));
        }

        [HttpGet("")]
        public IActionResult GetAll()
        {
            return Ok(attributes);
        }

        [HttpPost("")]
        public IActionResult Create(Attribute attribute)
        {
            attributes.Add(attribute);
            return Created($"Attribute/{attribute.Name}", attribute);
        }

        [HttpPut("")]
        public IActionResult Update(Attribute attribute)
        {
            var attr = attributes.FirstOrDefault(f => f.Name == attribute.Name);
            attr.MinLength = attribute.MinLength;
            attr.MaxLength = attribute.MaxLength;
            return Ok(attribute);
        }

        [HttpDelete("{name}")]
        public IActionResult Delete(string name)
        {
            attributes.Remove(attributes.FirstOrDefault(f => f.Name == name));
            return NoContent();
        }
    }
}
