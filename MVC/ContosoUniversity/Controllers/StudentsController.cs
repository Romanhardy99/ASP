
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ContosoUniversity.Models;
using ContosoUniversity;

public class StudentsController : Controller
{
    private readonly ContosoUniversityContext _context;

    public StudentsController(ContosoUniversityContext context)
    {
        _context = context;
    }

    // GET: STUDENTS
    public async Task<IActionResult> Index(
        string sortOrder, 
        string currentFilter, 
        string searchString, 
        int? pageIndex,
        int? itemsPerPage)    
    {
        int pageSize = itemsPerPage is 3 or 5 or 10 ? itemsPerPage.Value : 5;
        ViewData["ItemsPerPage"] = pageSize;
        ViewData["CurrentFilter"] = searchString;
        ViewData["NameSortParam"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
        ViewData["DateSortParam"] = sortOrder == "date_asc" ? "date_desc" : "date_asc";

        //if (searchString != null) pageIndex = 1;

        if (searchString != null)
        {
            pageIndex = 1;
        }
        else
        {
            searchString = currentFilter;
        }

        ViewData["CurrentFilter"] = searchString;

        IQueryable<Student> students = _context.Students;
        
        if(!string.IsNullOrWhiteSpace(searchString))
        {
            students = students.Where(student =>
            student.LastName.Contains(searchString) ||
            student.FirstName.Contains(searchString));
        }

        students = sortOrder switch
        {
            "name_desc" => students = students.OrderByDescending(s => s.LastName),
            "date_desc" => students = students.OrderByDescending(s => s.EnrollmentDate),
            "date_asc" => students = students.OrderBy(s => s.EnrollmentDate),
            _ => students = students.OrderBy(s => s.LastName)
        };

        return View(await PaginatedList<Student>.CreateAsync(students.AsNoTracking(), pageIndex ?? 1, pageSize));
        //var students = _context.Students.AsQueryable();
        //if (!string.IsNullOrEmpty(searchString))
        //{
        //    students = students.Where(s => s.LastName.Contains(searchString)
        //                                || s.FirstName.Contains(searchString));
        //}
        //return View(await students.OrderBy(s => s.LastName).ToListAsync());
        //return View(await students.ToListAsync());
    }

    // GET: STUDENTS/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var student = await _context.Students
            .FirstOrDefaultAsync(m => m.ID == id);
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
    public async Task<IActionResult> Create([Bind("LastName,FirstName,EnrollmentDate")] Student student)
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
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var student = await _context.Students.FindAsync(id);
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
    public async Task<IActionResult> Edit(int? id, [Bind("ID,LastName,FirstName,EnrollmentDate")] Student student)
    {
        if (id != student.ID)
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
                if (!StudentExists(student.ID))
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
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var student = await _context.Students
            .FirstOrDefaultAsync(m => m.ID == id);
        if (student == null)
        {
            return NotFound();
        }

        return View(student);
    }

    // POST: STUDENTS/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int? id)
    {
        var student = await _context.Students.FindAsync(id);
        if (student != null)
        {
            _context.Students.Remove(student);
        }

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool StudentExists(int? id)
    {
        return _context.Students.Any(e => e.ID == id);
    }
}
