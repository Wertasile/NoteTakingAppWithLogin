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

        // FOR GETTING A GAME FROM THE DATABASE TO EDIT IN EDIT PAGE
        [HttpGet("{id}")]
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
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier); // Get the current user's ID from claims
            note.ApplicationUserId = userId; // Set the ApplicationUserId

            _context.UserNotes.Add(note);
            await _context.SaveChangesAsync();


            return await GetAllUserNotes();
        }

        [HttpPut("{id}")]

        // FOR UPDATING A NOTE
        public async Task<ActionResult<List<UserNote>>> UpdateNotes(int id, UserNote note)
        {
            var dbNote = await _context.UserNotes.FindAsync(id); // firstly, we get the note from database
            if (dbNote == null)
            {
                return NotFound("This note does not exist");
            }

            dbNote.Title = note.Title;
            dbNote.Content = note.Content;
            dbNote.Tag = note.Tag;
            dbNote.ReleaseDate = note.ReleaseDate;



            await _context.SaveChangesAsync();


            return await GetAllUserNotes();
        }

        // FOR DELETING A NOTE
        [HttpDelete("{id}")]
        public async Task<ActionResult<List<UserNote>>> DeleteNotes(int id)
        {
            var dbNote = await _context.UserNotes.FindAsync(id); // firstly, we get the note from database
            if (dbNote == null)
            {
                return NotFound("This note does not exist");
            }

            _context.UserNotes.Remove(dbNote);
            await _context.SaveChangesAsync();

            return await GetAllUserNotes();
        }
    }
}
