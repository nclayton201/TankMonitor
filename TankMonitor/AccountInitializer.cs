using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TankMonitor.Areas.Identity.Data;
using TankMonitor.Data;
using TankMonitor.Models;

namespace TankMonitor
{
    public static class AccountInitializer
    {
        public static void SeedUsers(AccountContext _context)
        {
            var context = _context;


            // Find if there is an admin account in the user database
            var userStore = new UserStore<TankMonitorUser>(context);
            /**bool adminExists = false;
            foreach (var u in context.Users.AsEnumerable<TankMonitorUser>())
            {
                var role = context.Roles.Find(u.Id);
                if (role != null && role.Name == "Administrator")
                {
                    adminExists = true;
                }
            }**/

            // If there isn't an admin account, create the default admin account
            if (userStore.GetUsersInRoleAsync("Administrator").Result.Count == 0)
            {
                TankMonitorUser user = new TankMonitorUser
                {
                    UserName = "admin@admin.com",
                    Email = "admin@admin.com",
                    FirstName = "Admin",
                    LastName = "Admin"
                };

                user.NormalizedUserName = user.UserName.ToUpper();
                user.NormalizedEmail = user.Email.ToUpper();
                user.SecurityStamp = Guid.NewGuid().ToString();

                var password = new PasswordHasher<TankMonitorUser>();
                var hashed = password.HashPassword(user, "adminadmin");
                user.PasswordHash = hashed;

                var storedUser = userStore.FindByEmailAsync(user.Email);
                if (storedUser.Result == null)
                {
                    userStore.CreateAsync(user).Wait();
                    userStore.AddToRoleAsync(user, "Administrator").Wait();
                }
                else
                {
                    userStore.AddToRoleAsync(storedUser.Result, "Administrator").Wait();
                }
                context.SaveChangesAsync().Wait();
            }
        }
    }
}
