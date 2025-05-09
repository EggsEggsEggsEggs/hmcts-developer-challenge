using hmcts.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace hmcts.Pages;

public class DetailsModel(hmcts.Data.HmctsContext context) : PageModel
{
    public Case Case { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var caseEntity = await context.Case.FirstOrDefaultAsync(m => m.Id == id);

        if (caseEntity is not null)
        {
            Case = caseEntity;

            return Page();
        }

        return NotFound();
    }
}
