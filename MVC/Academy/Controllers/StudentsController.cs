
using Academy;
using Academy.Data;
using Academy.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

public class StudentsController : Controller
{
    private readonly AcademyContext _context;

    public StudentsController(AcademyContext context)
    {
        _context = context;
    }

    // GET: STUDENTS
    public async Task<IActionResult> Index(
    string sortOrder, string currentFilter, string searchString,
    int? pageIndex, int? itemsPerPage)
    {
        int pageSize = itemsPerPage is 3 or 5 or 10 ? itemsPerPage.Value : 5;
        ViewData["ItemsPerPage"] = pageSize;

        ViewData["CurrentSort"] = sortOrder;
        ViewData["NameSortParam"] = string.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
        ViewData["GroupSortParam"] = sortOrder == "group_asc" ? "group_desc" : "group_asc";
        ViewData["BirthSortParam"] = sortOrder == "birth_asc" ? "birth_desc" : "birth_asc";

        if (searchString != null)
            pageIndex = 1;
        else
            searchString = currentFilter;

        ViewData["CurrentFilter"] = searchString;

        IQueryable<Student> students = _context.Students
            .Include(s => s.GroupNavigation);

        if (!string.IsNullOrWhiteSpace(searchString))
        {
            students = students.Where(s =>
                s.LastName.Contains(searchString) ||
                s.FirstName.Contains(searchString) ||
                (s.MiddleName != null && s.MiddleName.Contains(searchString)) ||
                (s.GroupNavigation != null && s.GroupNavigation.GroupName.Contains(searchString)));
        }

        students = sortOrder switch
        {
            "name_desc" => students.OrderByDescending(s => s.LastName).ThenByDescending(s => s.FirstName),
            "group_asc" => students.OrderBy(s => s.GroupNavigation!.GroupName).ThenBy(s => s.LastName),
            "group_desc" => students.OrderByDescending(s => s.GroupNavigation!.GroupName).ThenBy(s => s.LastName),
            "birth_asc" => students.OrderBy(s => s.BirthDate),
            "birth_desc" => students.OrderByDescending(s => s.BirthDate),
            _ => students.OrderBy(s => s.LastName).ThenBy(s => s.FirstName)
        };

        return View(await PaginatedList<Student>.CreateAsync(
            students.AsNoTracking(), pageIndex ?? 1, pageSize));
    }

    // GET: STUDENTS/Details/5
    public async Task<IActionResult> Details(int? studid)
    {
        if (studid == null)
        {
            return NotFound();
        }

        var student = await _context.Students
            .FirstOrDefaultAsync(m => m.StudId == studid);
        if (student == null)
        {
            return NotFound();
        }

        return View(student);
    }

    // GET: STUDENTS/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: STUDENTS/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("StudId,LastName,FirstName,MiddleName,BirthDate,Email,Phone,Photo,Group,Attendances,Exams,Grades,GroupNavigation")] Student student)
    {
        if (ModelState.IsValid)
        {
            _context.Add(student);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(student);
    }

    // GET: STUDENTS/Edit/5
    public async Task<IActionResult> Edit(int? studid)
    {
        if (studid == null)
        {
            return NotFound();
        }

        var student = await _context.Students.FindAsync(studid);
        if (student == null)
        {
            return NotFound();
        }
        return View(student);
    }

    // POST: STUDENTS/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int? studid, [Bind("StudId,LastName,FirstName,MiddleName,BirthDate,Email,Phone,Photo,Group,Attendances,Exams,Grades,GroupNavigation")] Student student)
    {
        if (studid != student.StudId)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(student);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StudentExists(student.StudId))
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
        return View(student);
    }

    // GET: STUDENTS/Delete/5
    public async Task<IActionResult> Delete(int? studid)
    {
        if (studid == null)
        {
            return NotFound();
        }

        var student = await _context.Students
            .FirstOrDefaultAsync(m => m.StudId == studid);
        if (student == null)
        {
            return NotFound();
        }

        return View(student);
    }

    // POST: STUDENTS/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int? studid)
    {
        var student = await _context.Students.FindAsync(studid);
        if (student != null)
        {
            _context.Students.Remove(student);
        }

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool StudentExists(int? studid)
    {
        return _context.Students.Any(e => e.StudId == studid);
    }
}
