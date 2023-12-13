using KontaktyAPI.Application.Interfaces;
using KontaktyAPI.Application.Viewmodels.Collections;
using KontaktyAPI.Application.Viewmodels.Single;
using KontaktyAPI.Domain.Entities;
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
        public ActionResult<ListContactForListVm> GetContacts()
        {
            var contacts = _service.GetContacts(20, 1, "", null);
            return (Ok(contacts));
        }

        [HttpPost]
        [Route("/Post/Contacts")]
        public ActionResult<ListContactForListVm> GetContacts(int PageSize, int? PageNo, string? SearchString, int? SelectedCategoryId)
        {

            if (!PageNo.HasValue)
            {
                PageNo = 1;
            }
            var contacts = _service.GetContacts(PageSize, PageNo.Value, SearchString, SelectedCategoryId);
            return Ok(contacts);
        }

        [HttpGet]
        [Route("/Get/Contact/{id}")]
        public ActionResult<ContactDetailsVm> GetContact([FromRoute] int id)
        {
            var contact = _service.GetContact(id);
            if (contact == null)
            {
                return NotFound();
            }
            return Ok(contact);
        }

        [HttpGet]
        [Route("/Get/Categories")]
        public ActionResult<IEnumerable<CategoryVm>> GetCategories()
        {
            var categories = _service.GetCategories();
            return Ok(categories);
        }

        [HttpGet]
        [Route("/Get/SubCategories")]
        public ActionResult<IEnumerable<SubCategoryVm>> GetSubCategories()
        {
            var subCategories = _service.GetSubCategories();
            return Ok(subCategories);
        }

        [HttpPost]
        [Route("/New/SubCategory")]
        public ActionResult CreateSubCategory([FromBody] SubCategoryVm subCategory)
        {
            if (subCategory == null)
            {
                return BadRequest();
            }
            var id = _service.CreateSubCategory(subCategory);
            return Ok();
        }

        [HttpPost]
        [Route("/New/Contact")]
        public ActionResult CreateContact([FromBody] ContactDetailsVm contactDetailsVm)
        {
            if(contactDetailsVm == null)
            {
                return BadRequest();
            }
            var id = _service.CreateContact(contactDetailsVm);
            return Ok();
        }

        [HttpDelete]
        [Route("/Delete/Contact/{id}")]
        public ActionResult DeleteContact([FromRoute]int id)
        {
            if (_service.DeleteContact(id))
            {
                return Ok();
            }
            return BadRequest();
        }

        [HttpPut]
        [Route("/Edit/Contact/{id}")]
        public ActionResult EditContact([FromRoute] int id, [FromBody] ContactDetailsVm contactDetailsVm)
        {
            if(contactDetailsVm == null)
            {
                return BadRequest();
            }
            var result =  _service.EditContact(id, contactDetailsVm);
            if (!result)
            {
                return NotFound();
            }
            return Ok();
        }

    }
}
