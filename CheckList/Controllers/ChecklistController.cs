using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CheckList.Data;
using CheckList.Models;
using CheckList.Helpers;
using System.Threading.Tasks;
using Swashbuckle.AspNetCore.Annotations; // Adicione esta linha

namespace CheckList.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ChecklistController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly ChecklistHelper _checklistHelper;

        public ChecklistController(ApplicationDbContext context, ChecklistHelper checklistHelper)
        {
            _context = context;
            _checklistHelper = checklistHelper;
        }

        /// <summary>
        /// Creates a new checklist.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /api/checklist
        ///     {
        ///        "vehicleId": 1,
        ///        "items": [
        ///            {
        ///                "name": "Headlights",
        ///                "description": "Check if headlights are working properly."
        ///            },
        ///            {
        ///                "name": "Brake System",
        ///                "description": "Check the condition of the brake system."
        ///            }
        ///        ]
        ///     }
        ///
        /// </remarks>
        /// <param name="checklist">The checklist to create.</param>
        /// <returns>The created checklist.</returns>
        [HttpPost]
        [SwaggerOperation(Summary = "Creates a new checklist.")]
        [SwaggerResponse(201, "The checklist was created successfully.")]
        [SwaggerResponse(400, "The checklist is invalid.")]
        public async Task<IActionResult> CreateChecklist([FromBody] Checklist checklist)
        {
            checklist.CreatedAt = DateTime.UtcNow;
            checklist.IsCompleted = false;
            checklist.IsStarted = false;

            // Adicionar SupervisorApproval se existir
            if (checklist.SupervisorApproval != null)
            {
                _context.SupervisorApprovals.Add(checklist.SupervisorApproval);
            }

            // Adicionar checklist sem itens
            _context.Checklists.Add(checklist);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetChecklist), new { id = checklist.Id }, checklist);
        }

        [HttpPut("update/{id}")]
        [SwaggerOperation(Summary = "Updates an existing checklist with items.")]
        [SwaggerResponse(200, "The checklist was updated successfully.")]
        [SwaggerResponse(404, "The checklist was not found.")]
        public async Task<IActionResult> UpdateChecklist(int id, [FromBody] Checklist checklist)
        {
            var existingChecklist = await _context.Checklists.FindAsync(id);
            if (existingChecklist == null)
            {
                return NotFound();
            }

            // Atualizar propriedades da checklist
            existingChecklist.VehicleId = checklist.VehicleId;
            existingChecklist.SupervisorApproval = checklist.SupervisorApproval;
            existingChecklist.Items.Clear();

            // Adicionar novos itens
            foreach (var item in checklist.Items)
            {
                item.ChecklistId = id;
                _context.ChecklistItems.Add(item);
            }

            await _context.SaveChangesAsync();

            return Ok(existingChecklist);
        }
        /// <summary>
        /// Gets a checklist by ID.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /api/checklist/1
        ///
        /// Sample response:
        ///
        ///     {
        ///         "id": 1,
        ///         "createdAt": "2024-05-28T12:00:00Z",
        ///         "isCompleted": false,
        ///         "isStarted": false,
        ///         "items": [
        ///             {
        ///                 "id": 1,
        ///                 "name": "Headlights",
        ///                 "description": "Check if headlights are working properly."
        ///             },
        ///             {
        ///                 "id": 2,
        ///                 "name": "Brake System",
        ///                 "description": "Check the condition of the brake system."
        ///             }
        ///         ],
        ///         "supervisorApproval": null
        ///     }
        ///
        /// </remarks>
        /// <param name="id">The ID of the checklist.</param>
        /// <returns>The checklist with the specified ID.</returns>
        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Gets a checklist by ID.")]
        [SwaggerResponse(200, "The checklist was retrieved successfully.")]
        [SwaggerResponse(404, "The checklist was not found.")]
        public async Task<IActionResult> GetChecklist(int id)
        {
            var checklist = await _context.Checklists
                .Include(c => c.Items)
                .Include(c => c.SupervisorApproval)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (checklist == null)
            {
                return NotFound();
            }

            return Ok(checklist);
        }


        /// <summary>
        /// Updates an existing checklist.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     PUT /api/checklist/1
        ///     {
        ///        "id": 1,
        ///        "createdAt": "2024-05-28T12:00:00Z",
        ///        "isCompleted": true,
        ///        "isStarted": true,
        ///        "items": [
        ///            {
        ///                "id": 1,
        ///                "name": "Headlights",
        ///                "description": "Check if headlights are working properly."
        ///            },
        ///            {
        ///                "id": 2,
        ///                "name": "Brake System",
        ///                "description": "Check the condition of the brake system."
        ///            }
        ///        ],
        ///        "supervisorApproval": {
        ///            "id": 1,
        ///            "approvedAt": "2024-05-29T12:00:00Z"
        ///        }
        ///     }
        ///
        /// </remarks>
        /// <param name="id">The ID of the checklist.</param>
        /// <param name="checklist">The updated checklist.</param>
        /// <returns>No content if the update was successful.</returns>
        //[HttpPut("{id}")]
        //[SwaggerOperation(Summary = "Updates an existing checklist.")]
        //[SwaggerResponse(204, "The checklist was updated successfully.")]
        //[SwaggerResponse(400, "The checklist is invalid.")]
        //[SwaggerResponse(404, "The checklist was not found.")]
        //public async Task<IActionResult> UpdateChecklist(int id, [FromBody] Checklist checklist)
        //{
        //    if (id != checklist.Id)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(checklist).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!_checklistHelper.ChecklistExists(id)) // Use ChecklistHelper aqui
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return NoContent();
        //}


        /// <summary>
        /// Approves or disapproves a checklist.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /api/checklist/1/approve
        ///     {
        ///        "approved": true
        ///     }
        ///
        /// </remarks>
        /// <param name="id">The ID of the checklist.</param>
        /// <param name="approval">The supervisor's approval or disapproval.</param>
        /// <returns>No content if the approval was successful.</returns>
        [HttpPost("{id}/approve")]
        [SwaggerOperation(Summary = "Approves or disapproves a checklist.")]
        [SwaggerResponse(204, "The checklist was approved or disapproved successfully.")]
        [SwaggerResponse(404, "The checklist was not found.")]
        public async Task<IActionResult> ApproveChecklist(int id, [FromBody] SupervisorApproval approval)
        {
            var checklist = await _context.Checklists.FindAsync(id);
            if (checklist == null)
            {
                return NotFound();
            }

            approval.ApprovedAt = DateTime.UtcNow;
            checklist.SupervisorApproval = approval;
            checklist.IsCompleted = true;

            await _context.SaveChangesAsync();

            return NoContent();
        }


        /// <summary>
        /// Starts the execution of a checklist.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /api/checklist/1/start
        ///
        /// </remarks>
        /// <param name="id">The ID of the checklist.</param>
        /// <returns>No content if the start was successful.</returns>
        [HttpPost("{id}/start")]
        [SwaggerOperation(Summary = "Starts the execution of a checklist.")]
        [SwaggerResponse(204, "The checklist execution was started successfully.")]
        [SwaggerResponse(404, "The checklist was not found.")]
        [SwaggerResponse(409, "The checklist is already started by another executor.")]
        public async Task<IActionResult> StartChecklist(int id)
        {
            var checklist = await _context.Checklists.FindAsync(id);
            if (checklist == null)
            {
                return NotFound();
            }

            if (checklist.IsStarted)
            {
                return Conflict("The checklist is already started by another executor.");
            }

            checklist.IsStarted = true;
            await _context.SaveChangesAsync();

            return NoContent();
        }

    }
}
