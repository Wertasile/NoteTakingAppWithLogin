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

        
        public async Task<ActionResult<List<UserNote>>> GetAllUserNotes() // FOR SHOWING ALL THE GAME FROM THE DATABASE IN THE HOME PAGE
        {
            var user = await _context.Users
                .Include(u => u.UserNotes)
                .FirstOrDefaultAsync(u => u.Id == User.FindFirstValue(ClaimTypes.NameIdentifier));  // this means user that is authenticated 

            if(user == null)
                 return NotFound();
            
            return Ok(user.UserNotes);
           
        }

        
        [HttpGet("{id}")]   // FOR GETTING A GAME FROM THE DATABASE TO EDIT IN EDIT PAGE
        public async Task<ActionResult<List<UserNote>>> GetNote(int id)
        {
            var dbNote = await _context.UserNotes.FindAsync(id); // firstly, we get the note from database
            if (dbNote == null)
            {
                return NotFound("This note does not exist");
            }
            return Ok(dbNote);
        }

        // FOR ADDING A GAME
        [HttpPost]
        public async Task<ActionResult<List<UserNote>>> AddNotes(UserNote note)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            // Construct the SQL command to insert values into UserNotes, including ApplicationUserId
            string sqlCommand = "INSERT INTO UserNotes (Title, Content, Tag, ReleaseDate, ApplicationUserId, LatestDate) " +
                                "VALUES ({0}, {1}, {2}, {3}, '" + userId + "',{4})";

            
            await _context.Database.ExecuteSqlRawAsync(sqlCommand,
                note.Title, note.Content, note.Tag, note.ReleaseDate, note.LatestDate); 

            return await GetAllUserNotes();
        }

        [HttpPut("{id}")]

        // FOR UPDATING A NOTE
        public async Task<ActionResult<List<UserNote>>> UpdateNotes(int id, UserNote note)
        {
            var dbNote = await _context.UserNotes.FindAsync(id); // firstly, we get the note from database
            if (dbNote == null){
                return NotFound("This note does not exist");
            }

            dbNote.Title = note.Title; dbNote.Content = note.Content; dbNote.Tag = note.Tag; dbNote.LatestDate = DateTime.UtcNow; dbNote.Favourite = note.Favourite;

            await _context.SaveChangesAsync();
            return await GetAllUserNotes();
        }

        // FOR DELETING A NOTE
        [HttpDelete("{id}")]
        public async Task<ActionResult<List<UserNote>>> DeleteNotes(int id)
        {
            var dbNote = await _context.UserNotes.FindAsync(id); // firstly, we get the note from database
            if (dbNote == null){
                return NotFound("This note does not exist");
            }

            _context.UserNotes.Remove(dbNote);
            await _context.SaveChangesAsync();

            return await GetAllUserNotes();
        }
    }
}
