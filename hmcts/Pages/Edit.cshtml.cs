using hmcts.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace hmcts.Pages;

public class EditModel(Data.HmctsContext context) : PageModel
{
    [BindProperty]
    public Case Case { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id == null)
            return NotFound();

        var caseEntity = await context.Case.FirstOrDefaultAsync(m => m.Id == id);
        if (caseEntity == null)
        {
            return NotFound();
        }
        Case = caseEntity;
        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
            return Page();

        context.Attach(Case).State = EntityState.Modified;

        try
        {
            await context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!CaseExists(Case.Id))
                return NotFound();
            else
                throw;
        }

        return RedirectToPage("./Index");
    }

    private bool CaseExists(int id)
    {
        return context.Case.Any(e => e.Id == id);
    }
}
