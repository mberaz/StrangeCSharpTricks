using Microsoft.AspNetCore.Http;
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

        /// <response code="200">Returns the attribute</response>
        /// <response code="404">If no attribute was found</response>
        [HttpGet("{name}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<Attribute> GetByName([FromRoute] string name)
        {
            return Ok(attributes.FirstOrDefault(f => f.Name == name));
        }



        /// <response code="200">Returns all current attributes</response>
        /// <response code="404">If no attribute was found</response>
        [HttpGet("")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<List<Attribute>> GetAll()
        {
            return Ok(attributes);
        }

        /// <response code="201">Returns the newly created item</response>
        /// <response code="400">If the item is null</response>
        [HttpPost("")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<Attribute> Create([FromBody] Attribute attribute)
        {
            attributes.Add(attribute);
            return Created($"Attribute/{attribute.Name}", attribute);
        }

        /// <response code="200">If the attribute was updated</response>
        /// <response code="404">If no attribute was found</response>
        [HttpPut("")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<Attribute> Update([FromBody] Attribute attribute)
        {
            var attr = attributes.FirstOrDefault(f => f.Name == attribute.Name);
            attr.MinLength = attribute.MinLength;
            attr.MaxLength = attribute.MaxLength;
            return Ok(attribute);
        }

        /// <response code="204">if the attribute was deleted</response>
        /// <response code="404">If no attribute was found</response>
        [HttpDelete("{name}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult Delete([FromRoute] string name)
        {
            attributes.Remove(attributes.FirstOrDefault(f => f.Name == name));
            return NoContent();
        }
    }
}
