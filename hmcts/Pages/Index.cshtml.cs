using hmcts.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace hmcts.Pages;

public class IndexModel(Data.HmctsContext context) : PageModel
{
    public IList<Case> Case { get; set; } = default!;

    public async Task OnGetAsync()
    {
        Case = await context.Case.ToListAsync();
    }
}
