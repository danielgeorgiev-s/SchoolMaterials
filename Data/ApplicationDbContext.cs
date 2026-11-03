
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SchoolMaterials.Models;

namespace SchoolMaterials.Data;

public class ApplicationDbContext
    : IdentityDbContext<IdentityUser>
{
    public ApplicationDbContext(
        DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<MaterialRequest> MaterialRequests =>
        Set<MaterialRequest>();
}
