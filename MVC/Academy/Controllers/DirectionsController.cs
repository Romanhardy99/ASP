
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Academy.Models;
using Academy.Data;

public class DirectionsController : Controller
{
    private readonly AcademyContext _context;

    public DirectionsController(AcademyContext context)
    {
        _context = context;
    }

    // GET: DIRECTIONS
    public async Task<IActionResult> Index()    
    {
        return View(await _context.Directions.ToListAsync());
    }

    // GET: DIRECTIONS/Details/5
    public async Task<IActionResult> Details(byte? directionid)
    {
        if (directionid == null)
        {
            return NotFound();
        }

        var direction = await _context.Directions
            .FirstOrDefaultAsync(m => m.DirectionId == directionid);
        if (direction == null)
        {
            return NotFound();
        }

        return View(direction);
    }

    // GET: DIRECTIONS/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: DIRECTIONS/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("DirectionId,DirectionName,Groups,Disciplines")] Direction direction)
    {
        if (ModelState.IsValid)
        {
            _context.Add(direction);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(direction);
    }

    // GET: DIRECTIONS/Edit/5
    public async Task<IActionResult> Edit(byte? directionid)
    {
        if (directionid == null)
        {
            return NotFound();
        }

        var direction = await _context.Directions.FindAsync(directionid);
        if (direction == null)
        {
            return NotFound();
        }
        return View(direction);
    }

    // POST: DIRECTIONS/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(byte? directionid, [Bind("DirectionId,DirectionName,Groups,Disciplines")] Direction direction)
    {
        if (directionid != direction.DirectionId)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(direction);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DirectionExists(direction.DirectionId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction(nameof(Index));
        }
        return View(direction);
    }

    // GET: DIRECTIONS/Delete/5
    public async Task<IActionResult> Delete(byte? directionid)
    {
        if (directionid == null)
        {
            return NotFound();
        }

        var direction = await _context.Directions
            .FirstOrDefaultAsync(m => m.DirectionId == directionid);
        if (direction == null)
        {
            return NotFound();
        }

        return View(direction);
    }

    // POST: DIRECTIONS/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(byte? directionid)
    {
        var direction = await _context.Directions.FindAsync(directionid);
        if (direction != null)
        {
            _context.Directions.Remove(direction);
        }

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool DirectionExists(byte? directionid)
    {
        return _context.Directions.Any(e => e.DirectionId == directionid);
    }
}
