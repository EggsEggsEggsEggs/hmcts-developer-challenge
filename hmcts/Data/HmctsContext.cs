using Microsoft.EntityFrameworkCore;

namespace hmcts.Data;

public class HmctsContext(DbContextOptions<HmctsContext> options) : DbContext(options)
{
    public DbSet<Models.Case> Case { get; set; } = default!;
}
