using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace UserManagement.data_base_context
{
    public class UserDbContext : IdentityDbContext<IdentityUser>

    {
        public UserDbContext(DbContextOptions<UserDbContext> options) : base(options)
        {
        }
    }

}
