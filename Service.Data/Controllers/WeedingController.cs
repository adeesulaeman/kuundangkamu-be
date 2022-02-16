using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.Base.ViewModels.Common;
using Service.Base.ViewModels.Identity;
using Service.Data.CQRS.Commands;
using Service.Data.CQRS.Queries;
using Service.Data.ViewModels;

namespace Service.Data.Controllers
{
    [ApiVersion("1.0")]
    [Route("v{version:apiVersion}/[controller]")]
    [ApiController]
    public class WeedingController : ControllerBase
    {
        private readonly IMediator _mediator;
        public WeedingController(IMediator mediator, IHttpContextAccessor accessor)
        {
            _mediator = mediator;
        }

        [AllowAnonymous]
        [HttpPost("ucapan")]
        public async Task<ActionResult<UcapanVM>> CreateUcapan([FromBody] UcapanVM ucapan)
        {
            try
            {
                var result = await _mediator.Send(new CreateUcapanCommand
                {
                    Payload = ucapan,
                    Actor = new ProfileVM { Name = "System" }
                });

                return Ok(result);
            }
            catch (Exception exception)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, exception);
            }
        }

        [AllowAnonymous]
        [HttpGet("ucapan")]
        public async Task<ActionResult> GetUcapanByQuery([FromQuery] PagedQueryVM query, [FromQuery] SortableVM sortable, [FromQuery] string code)
        {
            try
            {
                var result = await _mediator.Send(new GetUcapanQuery
                {
                    PageQuery = query,
                    SortAble = sortable,
                    Code = code
                });

                return Ok(result);
            }
            catch (Exception exception)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, exception);
            }
        }
    }
}
