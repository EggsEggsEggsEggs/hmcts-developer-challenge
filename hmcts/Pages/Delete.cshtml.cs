using hmcts.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace hmcts.Pages;

public class DeleteModel(hmcts.Data.HmctsContext context) : PageModel
{

    [BindProperty]
    public Case Case { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id == null)
            return NotFound();

        var caseEntity = await context.Case.FirstOrDefaultAsync(m => m.Id == id);

        if (caseEntity is not null)
        {
            Case = caseEntity;

            return Page();
        }

        return NotFound();
    }

    public async Task<IActionResult> OnPostAsync(int? id)
    {
        if (id == null)
            return NotFound();

        var caseEntity = await context.Case.FindAsync(id);
        if (caseEntity != null)
        {
            Case = caseEntity;
            context.Case.Remove(Case);
            await context.SaveChangesAsync();
        }

        return RedirectToPage("./Index");
    }
}
