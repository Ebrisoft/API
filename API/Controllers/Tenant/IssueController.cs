using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abstractions.Models;
using Abstractions.Repositories;
using API.Endpoints;
using API.Models;
using API.Requests;
using API.Requests.Tenant;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.Tenant
{
    [ApiController]
    public class IssueController : ControllerBase
    {
        //  Variables
        //  =========

        private readonly IIssueRepository issueRepository;

        //  Constructors
        //  ============

        public IssueController(IIssueRepository issueRepository)
        {
            this.issueRepository = issueRepository;
        }

        //  Methods
        //  =======

        [HttpPost(TenantEndpoints.GetIssue)]
        public async Task<ActionResult<IEnumerable<Issue>>> GetIssue(GetIssue getIssue)
        {
            if (getIssue == null)
            {
                return BadRequest();
            }

            IIssue searchResult = await issueRepository.GetIssueById(getIssue.Id).ConfigureAwait(false);

            if (searchResult == null)
            {
                return NotFound();
            }

            Issue result = new Issue
            {
                Content = searchResult.Content
            };

            return Ok(result);
        }

        [HttpPost(TenantEndpoints.CreateIssue)]
        public async Task<ActionResult<IEnumerable<Issue>>> CreateIssue(CreateIssue createIssue)
        {
            if (createIssue == null)
            {
                return BadRequest();
            }

            bool success = await issueRepository.CreateIssue(createIssue.Content).ConfigureAwait(false);

            return success ? NoContent() : StatusCode(500);
        }
    }
}