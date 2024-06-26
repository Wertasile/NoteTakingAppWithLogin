﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using NoteTakingAppWithLogin.Server.Data;
using NoteTakingAppWithLogin.Shared;
using SQLitePCL;
using System.Security.Claims;

namespace NoteTakingAppWithLogin.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TagController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public TagController(ApplicationDbContext context)
        {
            _context = context;
        }

        // FOR SHOWING ALL THE TAG FROM THE DATABASE IN THE HOME PAGE
        public async Task<ActionResult<List<Tag>>> GetAllTags()
        {
            var user = await _context.Users
                .Include(t => t.Tags)
                .FirstOrDefaultAsync(t => t.Id == User.FindFirstValue(ClaimTypes.NameIdentifier));  // this means user that is authenticated 

            if (user == null)
                return NotFound();

            return Ok(user.Tags);
            //var list = await _context.Tags.ToListAsync();

            

            //return Ok(list);

        }

        // FOR GETTING A SINGLE TAG
        [HttpGet("{id}")]
        public async Task<ActionResult<Tag>> GetTag(int id)
        {

            var dbTag = await _context.Tags.FindAsync(id); // firstly, we get the note from database
            if (dbTag == null)
            {
                return NotFound("This note does not exist");
            }


            return Ok(dbTag);

        }

        // FOR ADDING A TAG

        [HttpPost]
        public async Task<ActionResult<List<Tag>>> AddTag(Tag tag)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            // Construct the SQL command to insert values into UserNotes, including ApplicationUserId
            string sqlCommand = "INSERT INTO Tags (TagName, ApplicationUserId) " +
                                "VALUES ({0}, '" + userId + "')";

            // Execute the SQL command
            await _context.Database.ExecuteSqlRawAsync(sqlCommand,
                tag.TagName);

            //_context.UserNotes.Add(note);

            //await _context.SaveChangesAsync();


            return await GetAllTags();

            //_context.Tags.Add(tag);

            //await _context.SaveChangesAsync();

            //var list = await _context.Tags.ToListAsync(); 
            //return Ok(list);
        }

        [HttpPut("{id}")]

        // FOR UPDATING A TAG
        public async Task<ActionResult<List<Tag>>> UpdateTag(int id, Tag tag)
        {
            var dbTag = await _context.Tags.FindAsync(id); // firstly, we get the note from database
            if (dbTag == null)
            {
                return NotFound("This note does not exist");
            }

            dbTag.TagName = tag.TagName;
           

            await _context.SaveChangesAsync();


            return await GetAllTags();
        }

        // FOR DELETING A TAG
        [HttpDelete("{id}")]
        public async Task<ActionResult<List<Tag>>> DeleteTag(int id)
        {
            var dbTag = await _context.Tags.FindAsync(id); // firstly, we get the note from database
            if (dbTag == null)
            {
                return NotFound("This note does not exist");
            }

            _context.Tags.Remove(dbTag);
            await _context.SaveChangesAsync();

            return await GetAllTags();
        }


    }
}
