
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Academy.Models;
using Academy.Data;

public class GroupsController : Controller
{
    private readonly AcademyContext _context;

    public GroupsController(AcademyContext context)
    {
        _context = context;
    }

    // GET: GROUPS
    public async Task<IActionResult> Index()    
    {
        return View(await _context.Groups.ToListAsync());
    }

    // GET: GROUPS/Details/5
    public async Task<IActionResult> Details(int? groupid)
    {
        if (groupid == null)
        {
            return NotFound();
        }

        var group = await _context.Groups
            .FirstOrDefaultAsync(m => m.GroupId == groupid);
        if (group == null)
        {
            return NotFound();
        }

        return View(group);
    }

    // GET: GROUPS/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: GROUPS/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("GroupId,GroupName,Direction,StartDate,StartTime,LearningDays,DirectionNavigation,Schedules,Students,Disciplines")] Group group)
    {
        if (ModelState.IsValid)
        {
            _context.Add(group);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(group);
    }

    // GET: GROUPS/Edit/5
    public async Task<IActionResult> Edit(int? groupid)
    {
        if (groupid == null)
        {
            return NotFound();
        }

        var group = await _context.Groups.FindAsync(groupid);
        if (group == null)
        {
            return NotFound();
        }
        return View(group);
    }

    // POST: GROUPS/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int? groupid, [Bind("GroupId,GroupName,Direction,StartDate,StartTime,LearningDays,DirectionNavigation,Schedules,Students,Disciplines")] Group group)
    {
        if (groupid != group.GroupId)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(group);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GroupExists(group.GroupId))
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
        return View(group);
    }

    // GET: GROUPS/Delete/5
    public async Task<IActionResult> Delete(int? groupid)
    {
        if (groupid == null)
        {
            return NotFound();
        }

        var group = await _context.Groups
            .FirstOrDefaultAsync(m => m.GroupId == groupid);
        if (group == null)
        {
            return NotFound();
        }

        return View(group);
    }

    // POST: GROUPS/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int? groupid)
    {
        var group = await _context.Groups.FindAsync(groupid);
        if (group != null)
        {
            _context.Groups.Remove(group);
        }

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool GroupExists(int? groupid)
    {
        return _context.Groups.Any(e => e.GroupId == groupid);
    }
}
