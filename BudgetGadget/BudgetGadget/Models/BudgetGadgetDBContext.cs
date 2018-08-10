using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using BudgetGadget.Entity;

namespace BudgetGadget.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class BudgetGadgetDBContext : IdentityDbContext<ApplicationUser>
    {
        public BudgetGadgetDBContext()
            : base("BudgetGadgetDB", throwIfV1Schema: false)
        {
        }

        public static BudgetGadgetDBContext Create()
        {
            return new BudgetGadgetDBContext();
        }

        public DbSet<Registration> Registrations { get; set; }
        public DbSet<Earning> Earnings { get; set; }
        public DbSet<Expense> Expenses { get; set; }
    }
}