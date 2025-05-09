using hmcts.Data;
using hmcts.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace hmcts.Pages;
public class IndexModel(HmctsContext context) : PageModel
{
    public List<Case> Case { get; set; } = []; // Initialize the Case property

    public async Task OnGetAsync()
    {
        Case = await context.Case.ToListAsync();
    }
}
