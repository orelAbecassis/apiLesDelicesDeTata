using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MaApiCrud.Bdd;
using MaApiCrud.Models;

[Route("api/[controller]")]
[ApiController]
public class CategorieController : ControllerBase
{
    private readonly ConnectBdd _context;

    public CategorieController(ConnectBdd context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Categorie>>> GetCategorie()
    {
        return await _context.Categorie.ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Categorie>> GetCategory(int id)
    {
        var category = await _context.Categorie.FindAsync(id);

        if (category == null)
        {
            return NotFound();
        }

        return category;
    }

    [HttpPost]
    public async Task<ActionResult<Categorie>> PostCategory(Categorie categorie)
    {
        _context.Categorie.Add(categorie);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetCategory), new { id = categorie.id }, categorie);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutCategory(int id, Categorie categorie)
    {
        if (id != categorie.id)
        {
            return BadRequest();
        }

        _context.Entry(categorie).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!CategoryExists(id))
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

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCategory(int id)
    {
        var category = await _context.Categorie.FindAsync(id);
        if (category == null)
        {
            return NotFound();
        }

        _context.Categorie.Remove(category);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool CategoryExists(int id)
    {
        return _context.Categorie.Any(e => e.id == id);
    }
}
