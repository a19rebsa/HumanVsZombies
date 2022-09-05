﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HumansVsZombies_Backend.Data;
using HumansVsZombies_Backend.Models;
using System.Net.Mime;
using HumansVsZombies_Backend.DTOs.SquadCheckinDTO;

namespace HumansVsZombies_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces(MediaTypeNames.Application.Json)]
    [Consumes(MediaTypeNames.Application.Json)]
    [ApiConventionType(typeof(DefaultApiConventions))]
    public class SquadCheckinsController : ControllerBase
    {
        private readonly HvZDbContext _context;

        public SquadCheckinsController(HvZDbContext context)
        {
            _context = context;
        }

        // GET: api/SquadCheckins
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SquadCheckinReadDTO>>> GetSquadCheckin()
        {
            return await _context.SquadCheckin.ToListAsync();
        }

        // GET: api/SquadCheckins/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SquadCheckinReadDTO>> GetSquadCheckin(int id)
        {
            var squadCheckin = await _context.SquadCheckin.FindAsync(id);

            if (squadCheckin == null)
            {
                return NotFound();
            }

            return squadCheckin;
        }

        // PUT: api/SquadCheckins/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSquadCheckin(int id, SquadCheckinUpdateDTO squadCheckinDto)
        {
            if (id != squadCheckinDto.SquadCheckinId)
            {
                return BadRequest();
            }

            _context.Entry(squadCheckin).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SquadCheckinExists(id))
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

        // POST: api/SquadCheckins
        [HttpPost]
        public async Task<ActionResult<SquadCheckin>> PostSquadCheckin(SquadCheckinCreateDTO dtoSquadCheckin)
        {
            _context.SquadCheckin.Add(squadCheckin);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSquadCheckin", new { id = squadCheckin.SquadCheckinId }, squadCheckin);
        }

        // DELETE: api/SquadCheckins/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSquadCheckin(int id)
        {
            var squadCheckin = await _context.SquadCheckin.FindAsync(id);
            if (squadCheckin == null)
            {
                return NotFound();
            }

            _context.SquadCheckin.Remove(squadCheckin);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SquadCheckinExists(int id)
        {
            return _context.SquadCheckin.Any(e => e.SquadCheckinId == id);
        }
    }
}
