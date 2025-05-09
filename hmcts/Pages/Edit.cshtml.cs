using hmcts.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace hmcts.Pages;

public class EditModel(Data.HmctsContext context) : PageModel
{
    [BindProperty]
    public Case Case { get; set; } = new();

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id == null)
            return NotFound();

        var caseEntity = await context.Case.FirstOrDefaultAsync(m => m.Id == id);
        if (caseEntity == null)
            return NotFound();

        Case = caseEntity;
        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
            return Page();

        // Retrieve the existing entity from the database
        var existingCase = await context.Case.FirstOrDefaultAsync(c => c.Id == Case.Id);
        if (existingCase == null)
            return NotFound();

        // Update the properties of the existing entity
        existingCase.CaseNumber = Case.CaseNumber;
        existingCase.Title = Case.Title;
        existingCase.Description = Case.Description;
        existingCase.Status = Case.Status;
        existingCase.CreatedDate = Case.CreatedDate;

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
        => context.Case.Any(e => e.Id == id);

}
