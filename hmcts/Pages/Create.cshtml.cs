using hmcts.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace hmcts.Pages;

public class CreateModel(Data.HmctsContext Context) : PageModel
{
    [BindProperty]
    public Case Case { get; set; } = default!;

    public IActionResult OnGet()
        => Page();

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
            return Page();

        Context.Case.Add(Case);
        await Context.SaveChangesAsync();

        return RedirectToPage("./Index");
    }
}
