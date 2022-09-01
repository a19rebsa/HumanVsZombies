﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HumansVsZombies_Backend.Data;
using HumansVsZombies_Backend.Models;

namespace HumansVsZombies_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SquadsController : ControllerBase
    {
        private readonly HvZDbContext _context;

        public SquadsController(HvZDbContext context)
        {
            _context = context;
        }

        // GET: api/Squads
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Squad>>> GetSquad()
        {
            return await _context.Squad.ToListAsync();
        }

        // GET: api/Squads/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Squad>> GetSquad(int id)
        {
            var squad = await _context.Squad.FindAsync(id);

            if (squad == null)
            {
                return NotFound();
            }

            return squad;
        }

        // PUT: api/Squads/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSquad(int id, Squad squad)
        {
            if (id != squad.SquadId)
            {
                return BadRequest();
            }

            _context.Entry(squad).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SquadExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Squads
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Squad>> PostSquad(Squad squad)
        {
            _context.Squad.Add(squad);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSquad", new { id = squad.SquadId }, squad);
        }

        // DELETE: api/Squads/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSquad(int id)
        {
            var squad = await _context.Squad.FindAsync(id);
            if (squad == null)
            {
                return NotFound();
            }

            _context.Squad.Remove(squad);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SquadExists(int id)
        {
            return _context.Squad.Any(e => e.SquadId == id);
        }
    }
}