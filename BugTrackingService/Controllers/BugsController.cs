using System.Collections.Generic;
using System.Threading.Tasks;
using Abstractions;
using BugTrackingService.ServiceModels;
using BugTrackingService.Validators;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;

namespace BugTrackingService.Controllers
{
    /// <summary>
    /// REST Services for Buugs
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class BugsController : ControllerBase
    {
        private readonly Bug.IBugRepository _bugRepository;

        /// <summary>
        /// Creates new instance of this class
        /// </summary>
        /// <param name="bugRepository"></param>
        public BugsController(Bug.IBugRepository bugRepository)
        {
            _bugRepository = bugRepository;
        }

        /// <summary>
        /// Returns all Bugs from the Buug repository
        /// </summary>
        /// <returns>The requested list of Bugs</returns>
        /// <response code="200">If operation was successful</response>
        [HttpGet]
        [ProducesResponseType(200)]
        public async Task<ActionResult<IEnumerable<Bug>>> Get()
        {
            return Ok(await _bugRepository.GetBugs());
        }

        /// <summary>
        /// Returns the Buug with the given Id
        /// </summary>
        /// <param name="aBugId"></param>
        /// <returns>The requested Buug</returns>
        /// <response code="200">If Buug exists</response>
        /// <response code="400">If Buug doesn't exist</response>
        [HttpGet("{aBugId}")]
        public async Task<ActionResult<Bug>> GetSingle([ObjectIdValidator] string aBugId)
        {
            Bug aBug = await _bugRepository.GetBug(ObjectId.Parse(aBugId));

            if (aBug == null)
            {
                return BadRequest();
            }

            return Ok(aBug);
        }

        /// <summary>
        /// Adds a new Buug to the database
        /// </summary>
        /// <param name="aCreateBugModel"></param>
        /// <response code="204">If the operation was successful</response>
        [HttpPut]
        [ProducesResponseType(204)]
        public void AddBug(CreateBugModel aCreateBugModel)
        {
            _bugRepository.AddBug(new Bug()
            {
                Id = ObjectId.GenerateNewId(),
                State = aCreateBugModel.State,
                Title = aCreateBugModel.Title
            });
        }
    }
}