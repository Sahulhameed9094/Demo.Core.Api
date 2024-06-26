﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Demo.Core.Api.Models;
using Microsoft.Extensions.Caching.Distributed;
using Demo.Core.Api.Cache;

namespace Demo.Core.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BrandsController : ControllerBase
    {
        private readonly BrandContext _context;
        private readonly IDistributedCache _cache;
        public BrandsController(BrandContext context,
            IDistributedCache distributedCache)
        {
            _context = context;
            _cache = distributedCache;
        }

        // GET: api/Brands
        [HttpGet]
        public async Task<ActionResult<List<Brand>>> GetBrands()
        {
            var cacheKey = "GET_ALL_STUDENTS";
            if (_context.Brands == null)
            {
                return NotFound();
            }

            var data = await _cache.GetRecordAsync<List<Brand>>(cacheKey);
            if (data is null)
            {
                Thread.Sleep(10000);
                data = _context.Brands.ToList();
                await _cache.SetRecordAsync(cacheKey, data);
            }
            return data;
        }

        // GET: api/Brands/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Brand>> GetBrand(int id)
        {
            if (_context.Brands == null)
            {
                return NotFound();
            }
            var brand = await _context.Brands.FindAsync(id);

            if (brand == null)
            {
                return NotFound();
            }

            return brand;
        }

        // PUT: api/Brands/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBrand(int id, Brand brand)
        {
            if (id != brand.ID)
            {
                return BadRequest();
            }

            _context.Entry(brand).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BrandExists(id))
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

        // POST: api/Brands
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Brand>> PostBrand(Brand brand)
        {
            if (_context.Brands == null)
            {
                return Problem("Entity set 'BrandContext.Brands'  is null.");
            }
            _context.Brands.Add(brand);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBrand", new { id = brand.ID }, brand);
        }

        // DELETE: api/Brands/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBrand(int id)
        {
            if (_context.Brands == null)
            {
                return NotFound();
            }
            var brand = await _context.Brands.FindAsync(id);
            if (brand == null)
            {
                return NotFound();
            }

            _context.Brands.Remove(brand);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool BrandExists(int id)
        {
            return (_context.Brands?.Any(e => e.ID == id)).GetValueOrDefault();
        }
    }
}
