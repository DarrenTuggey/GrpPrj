using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GroupProject.Areas.Identity.Data
{
    // Database Context class for the Identity Framework.
    public class GroupProjectAuthContext : IdentityDbContext<GroupProjectUser>
    {
        public GroupProjectAuthContext(DbContextOptions<GroupProjectAuthContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
