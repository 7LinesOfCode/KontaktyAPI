using KontaktyAPI.Application.DTO;
using KontaktyAPI.Application.Interfaces;
using KontaktyAPI.Application.Viewmodels.Collections;
using KontaktyAPI.Application.Viewmodels.Single;
using KontaktyAPI.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KontaktyAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactController : ControllerBase
    {
        private readonly IContactService _service;

        public ContactController(IContactService service)
        {
            _service = service;
        }

        [HttpGet]
        [Route("/Get/Contacts")]
        public async Task<ActionResult<ListContactForListVm>> GetContacts()
        {
            var contacts = await _service.GetContactsAsync(20, 1, "", null);
            return Ok(contacts);
        }

        [HttpPost]
        [Route("/Post/Contacts")]
        public async Task<ActionResult<ListContactForListVm>> GetContacts([FromBody] ContactsPageDTO dtoInfo)
        {
            int? PageNo = dtoInfo.pageNo;
            if (!PageNo.HasValue)
            {
                PageNo = 1;
            }
            var contacts = await _service.GetContactsAsync(dtoInfo.PageSize, PageNo.Value, dtoInfo.SearchString, dtoInfo.SelectedCategoryId);
            return Ok(contacts);
        }

        [HttpGet]
        [Route("/Get/Contact/{id}")]
        public async Task<ActionResult<ContactDetailsVm>> GetContact([FromRoute] int id)
        {
            var contact = await _service.GetContactAsync(id);
            if (contact == null)
            {
                return NotFound();
            }
            return Ok(contact);
        }

        [HttpGet]
        [Route("/Get/Categories")]
        public async Task<ActionResult<IEnumerable<CategoryVm>>> GetCategories()
        {
            var categories = await _service.GetCategoriesAsync();
            return Ok(categories);
        }

        [HttpGet]
        [Route("/Get/SubCategories")]
        public async Task<ActionResult<IEnumerable<SubCategoryVm>>> GetSubCategories()
        {
            var subCategories = await _service.GetSubCategoriesAsync();
            return Ok(subCategories);
        }

        [HttpPost]
        [Route("/New/SubCategory")]
        public async Task<ActionResult> CreateSubCategory([FromBody] SubCategoryVm subCategory)
        {
            if (subCategory == null)
            {
                return BadRequest();
            }
            var id = await _service.CreateSubCategoryAsync(subCategory);
            return Ok(id);
        }

        [HttpPost]
        [Authorize]
        [Route("/New/Contact")]
        public async Task<ActionResult> CreateContact([FromBody] ContactDetailsVm contactDetailsVm)
        {
            if (contactDetailsVm == null)
            {
                return BadRequest();
            }
            var id = await _service.CreateContactAsync(contactDetailsVm);
            return Ok(id);
        }

        [HttpDelete]
        [Authorize]
        [Route("/Delete/Contact/{id}")]
        public async Task<ActionResult> DeleteContact([FromRoute] int id)
        {
            var result = await _service.DeleteContactAsync(id);
            if (result)
            {
                return Ok();
            }
            return BadRequest();
        }

        [HttpPut]
        [Authorize]
        [Route("/Edit/Contact/{id}")]
        public async Task<ActionResult> EditContact([FromRoute] int id, [FromBody] ContactDetailsVm contactDetailsVm)
        {
            if (contactDetailsVm == null)
            {
                return BadRequest();
            }
            var result = await _service.EditContactAsync(id, contactDetailsVm);
            if (!result)
            {
                return NotFound();
            }
            return Ok();
        }
    }
}
