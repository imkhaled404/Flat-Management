using FlatManage.Domain.Entities;
using FlatManage.Domain.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlatManage.Infrastructure.Data
{
    public static class DbSeeder
    {
        public static async Task SeedAsync(ApplicationDbContext context, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            await context.Database.EnsureCreatedAsync();

            // Seed Roles
            string[] roleNames = { "Admin", "Owner", "Tenant", "Staff" };
            foreach (var roleName in roleNames)
            {
                if (!await roleManager.RoleExistsAsync(roleName))
                {
                    await roleManager.CreateAsync(new IdentityRole(roleName));
                }
            }

            // Seed Admin User
            var adminEmail = "admin@flatmanage.com";
            if (await userManager.FindByEmailAsync(adminEmail) == null)
            {
                var adminUser = new ApplicationUser
                {
                    UserName = adminEmail,
                    Email = adminEmail,
                    FullName = "System Admin",
                    UserType = UserType.Admin,
                    EmailConfirmed = true
                };
                await userManager.CreateAsync(adminUser, "Admin@123");
                await userManager.AddToRoleAsync(adminUser, "Admin");
            }

            // Seed Building
            if (!context.Buildings.Any())
            {
                var building = new Building
                {
                    Name = "Skyline Residency",
                    Address = "123 Green Road",
                    City = "Dhaka",
                    TotalFloors = 5,
                    TotalUnits = 20,
                    Description = "Modern residential building with premium amenities.",
                    IsActive = true
                };
                context.Buildings.Add(building);
                await context.SaveChangesAsync();

                // Seed Floors
                for (int i = 1; i <= 3; i++)
                {
                    var floor = new Floor
                    {
                        BuildingId = building.Id,
                        FloorNumber = i,
                        Name = $"Floor {i}",
                        TotalUnits = 4
                    };
                    context.Floors.Add(floor);
                    await context.SaveChangesAsync();

                    // Seed Units
                    for (int j = 1; j <= 4; j++)
                    {
                        var unit = new Unit
                        {
                            BuildingId = building.Id,
                            FloorId = floor.Id,
                            UnitNumber = $"{i}0{j}",
                            UnitType = UnitType.BHK2,
                            SizeInSqFt = 1200,
                            MonthlyRent = 25000 + (i * 1000),
                            Status = UnitStatus.Available,
                            IsActive = true
                        };
                        context.Units.Add(unit);
                    }
                    await context.SaveChangesAsync();
                }

                // Seed Tenants & Agreements
                var units = await context.Units.Take(5).ToListAsync();
                for (int t = 1; t <= 5; t++)
                {
                    var tenantEmail = $"tenant{t}@example.com";
                    var tenantUser = new ApplicationUser
                    {
                        UserName = tenantEmail,
                        Email = tenantEmail,
                        FullName = $"Tenant User {t}",
                        UserType = UserType.Tenant,
                        EmailConfirmed = true
                    };
                    await userManager.CreateAsync(tenantUser, "Tenant@123");
                    await userManager.AddToRoleAsync(tenantUser, "Tenant");

                    var tenant = new Tenant
                    {
                        UserId = tenantUser.Id,
                        UnitId = units[t - 1].Id,
                        NID = $"123456789{t}",
                        PermanentAddress = "Vill: X, P.O: Y, Dist: Z",
                        EmergencyContact = "Parent",
                        EmergencyContactPhone = "01711000000",
                        Occupation = "Service",
                        WorkAddress = "Dhaka IT Park"
                    };
                    context.Tenants.Add(tenant);
                    await context.SaveChangesAsync();

                    var agreement = new Agreement
                    {
                        TenantId = tenant.Id,
                        UnitId = tenant.UnitId,
                        StartDate = DateTimeOffset.UtcNow.AddMonths(-3),
                        EndDate = DateTimeOffset.UtcNow.AddYears(1),
                        MonthlyRent = units[t - 1].MonthlyRent,
                        SecurityDeposit = units[t - 1].MonthlyRent * 2,
                        AdvanceAmount = units[t - 1].MonthlyRent,
                        Status = AgreementStatus.Active,
                        SignedDate = DateTimeOffset.UtcNow.AddMonths(-3)
                    };
                    context.Agreements.Add(agreement);
                    
                    // Update unit status
                    units[t - 1].Status = UnitStatus.Occupied;
                    context.Units.Update(units[t - 1]);
                }
                await context.SaveChangesAsync();
            }
        }
    }
}
