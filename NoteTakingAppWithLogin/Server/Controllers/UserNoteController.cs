using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NoteTakingAppWithLogin.Server.Data;
using NoteTakingAppWithLogin.Shared;
using SQLitePCL;
using System.Security.Claims;

namespace NoteTakingAppWithLogin.Server.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserNoteController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public UserNoteController(ApplicationDbContext context)
        {
            _context = context;
        }

        // FOR SHOWING ALL THE GAME FROM THE DATABASE IN THE HOME PAGE
        public async Task<ActionResult<List<UserNote>>> GetAllUserNotes()
        {
            var user = await _context.Users
                .Include(u => u.UserNotes)
                .FirstOrDefaultAsync(u => u.Id == User.FindFirstValue(ClaimTypes.NameIdentifier));  // this means user that is authenticated 

            if(user == null)
                 return NotFound();
            return Ok(user.UserNotes);
           
        }
    }
}
