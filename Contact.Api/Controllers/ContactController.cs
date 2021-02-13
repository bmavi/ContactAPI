using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using Contact.Data;
using Contact.Models;
using Contact.Api.DTOs;
using Microsoft.AspNetCore.Http;

namespace Contact.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class ContactController : Controller
    {
        private readonly IGenericRepository<ContactInfo> _genericRepository;
        private readonly IMapper _mapper;
        public ContactController(IGenericRepository<ContactInfo> genericRepository, IMapper mapper)
        {
            _genericRepository = genericRepository;
            _mapper = mapper;

        }

        // GET list of all Contacts
        [HttpGet("ListAllContacts")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<ContactInfo>>> GetAllAsync()
        {
            IEnumerable<ContactInfo> resultList = await _genericRepository.GetAllAsync();

            if (!resultList.Any())
            {
                return NotFound("No Contact Data");
            }
             
            return Ok(resultList);
        }
         

        // CREATE new Contact
        [HttpPost("AddNewContact")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ContactInfoDTO>> AddNewContactAsync([FromBody] ContactInfo newContact)
        {
            if (newContact == null)
            {
                return NotFound("Input object cannot be null");
            }
            newContact.Id = Guid.NewGuid().ToString();
            int created = await _genericRepository.AddTAsync(newContact);

            if (created == 0)
            {

                return NotFound("No New Contact created");
            }

            var newRecord = await _genericRepository.GetOneByAsync(t => t.Id == newContact.Id);

            ContactInfoDTO contactInfoDTO = _mapper.Map<ContactInfoDTO>(newRecord);

            return Created("New Contact Created", contactInfoDTO);
        }

        // UPDATE Contact
        [HttpPut("UpdateContact/{id}")]
        [HttpPatch("UpdateContact/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ContactInfoDTO>> UpdateContactAsync([FromRoute] string id, [FromBody] ContactInfo updateContact)
        {
            
            updateContact.Id = id;
            int result = await _genericRepository.UpdateTAsync(updateContact);
            if (result == 0)
            {
                return NotFound("No Contact Updated");
            }

            ContactInfoDTO contactInfoDTO = _mapper.Map<ContactInfoDTO>(updateContact);           

            return Ok(contactInfoDTO);
        }



        // DELETE Contact
        [HttpDelete("DeleteContact/{id}")]         
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<int>> DeleteContactAsync([FromRoute] string id)
        {
             
            var contactRecord = await _genericRepository.GetOneByAsync(t=>t.Id == id);

            if (contactRecord == null)
            {               
                return NotFound("No Contact Data");
            }

            int result = await _genericRepository.DeleteTAsync(contactRecord);

            return Ok(result);
        }

    }
}
