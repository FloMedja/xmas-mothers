using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ChristmasMothers.Business.Interface;
using ChristmasMothers.Web.Api.V1.Models;
using ChristmasMothers.Business.Interface;
using ChristmasMothers.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace ChristmasMothers.Web.Api.V1.Controllers
{

    [Route("api/v1/parents")]
    public class ParentController : Controller
    {
        private readonly IParentBusiness _parentBusiness;
        private readonly IHttpGetService _httpGet;

        public ParentController(IParentBusiness parentBusiness)
        {
            _parentBusiness = parentBusiness;
        }

        [HttpGet("")]
        public async Task<IActionResult> GetAllParents()
        {
            try
            {
                var allResult = await _parentBusiness.GetAllAsync<IEnumerable<ParentRequestModel>>();
                return Ok(allResult);

            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }

        [HttpGet("nbRequistion", Name = "GetNumberOfRequsitions")]
        public async Task<IActionResult> GetNumberOfParents()
        {
            try
            {
                var nbRequsitionResult = await _parentBusiness.GetNumberOfParent();
                return Ok(nbRequsitionResult);

            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }

        [HttpGet("{parentId:Guid}",Name= "GetParent")]
        public async Task<IActionResult> GetParent(Guid parentId,[FromQuery]bool includeChildren = true)
        {
            try
            {
                var parent = await _parentBusiness.GetByIdAsync<ParentRequestModel>(parentId,includeChildren);
                return Ok(parent);

            }
            catch (NotFoundException e)
            {
                return NotFound();
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }

        [HttpGet("parentId/{parentId:Guid}", Name = "GetParentByParentId")]
        [ApiExplorerSettings(GroupName = "v1")]
        public async Task<IActionResult> GetParentByParentId(string parentId, [FromQuery]bool includeChildren = true)
        {
            try
            {
                var parent = await _parentBusiness.GetByParentIdAsync<ParentRequestModel>(parentId, includeChildren);
                return Ok(parent);

            }
            catch (NotFoundException e)
            {
                return NotFound();
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }

        public async Task<IActionResult> SearchParent()
        {
            return Ok();

        }

        public async Task<IActionResult> AdvanceSearchParent()
        {
            return Ok();

        }
        public async Task<IActionResult> CreateParent()
        {
            return Ok();

        }

        public async Task<IActionResult> UpdateParent()
        {
            return Ok();
        }

        public async Task<IActionResult> DeleteParent()
        {
            return Ok();

        }




    }
}