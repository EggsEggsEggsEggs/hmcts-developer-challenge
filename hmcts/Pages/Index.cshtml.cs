using hmcts.Data;
using hmcts.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace hmcts.Pages;
public class IndexModel : PageModel
{
    private readonly HmctsContext context;

    public IndexModel(HmctsContext context)
    {
        this.context = context;
        Case = new List<Case>(); // Initialize the Case property
    }

    public List<Case> Case { get; set; }

    public async Task OnGetAsync()
    {
        Case = await context.Case.ToListAsync();
    }
}
