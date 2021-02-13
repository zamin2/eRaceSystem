using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Web;
using eRaceProject.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Configuration;
using eRaceSystem.BLL;
using eRaceSystem.ViewModels;



namespace eRaceProject.Admin.Security
{
    public class SecurityDbInitializer : CreateDatabaseIfNotExists<ApplicationDbContext>
    {
        protected override void Seed(ApplicationDbContext context)
        {
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var startupRoles = ConfigurationManager.AppSettings["startupRoles"].Split(';');
            foreach (var role in startupRoles)
            {
                roleManager.Create(new IdentityRole {Name = role});
            }
            string adminUser = ConfigurationManager.AppSettings["adminUserName"];
            string adminEmail = ConfigurationManager.AppSettings["adminEmail"];
            string adminPassword = ConfigurationManager.AppSettings["adminPassword"];
            

            var userManager = new ApplicationUserManager(new UserStore<ApplicationUser>(context));
            var result = userManager.Create(new ApplicationUser
            {
                UserName = adminUser,
                Email = adminEmail,
                EmailConfirmed = true


            }, adminPassword);
            if (result.Succeeded)
            {
                userManager.AddToRole(userManager.FindByName(adminUser).Id, Settings.AdminRole);
            }

            string defaultPassword = ConfigurationManager.AppSettings["defaultPassword"];
            var controller = new SetupUserRegistrationController();
            
            
            IEnumerable<SetupUserInfo> directors = controller.GetDirectors();
            foreach (var person in directors)
            {
                result = userManager.Create(new ApplicationUser
                {
                    UserName = person.UserName,
                    EmployeeID = int.Parse(person.UserId)

                }, defaultPassword);
                if (result.Succeeded)
                    userManager.AddToRole(userManager.FindByName(person.UserName).Id, Settings.DirectorRole);
            }

            IEnumerable<SetupUserInfo> coordinators = controller.GetCoordinators();
            foreach (var person in coordinators)
            {
                result = userManager.Create(new ApplicationUser
                {
                    UserName = person.UserName,
                    EmployeeID = int.Parse(person.UserId)

                }, defaultPassword);
                if (result.Succeeded)
                    userManager.AddToRole(userManager.FindByName(person.UserName).Id, Settings.CoordinatorRole);
            }

            IEnumerable<SetupUserInfo> investigators = controller.GetInvestigators();
            foreach (var person in investigators)
            {
                result = userManager.Create(new ApplicationUser
                {
                    UserName = person.UserName,
                    EmployeeID = int.Parse(person.UserId)

                }, defaultPassword);
                if (result.Succeeded)
                    userManager.AddToRole(userManager.FindByName(person.UserName).Id, Settings.InvestigatorRole);
            }

            IEnumerable<SetupUserInfo> seniormechanics = controller.GetSeniorMechanics();
            foreach (var person in seniormechanics)
            {
                result = userManager.Create(new ApplicationUser
                {
                    UserName = person.UserName,
                    EmployeeID = int.Parse(person.UserId)

                }, defaultPassword);
                if (result.Succeeded)
                    userManager.AddToRole(userManager.FindByName(person.UserName).Id, Settings.SeniorMechanicRole);
            }

            IEnumerable<SetupUserInfo> mechanics = controller.GetMechanics();
            foreach (var person in mechanics)
            {
                result = userManager.Create(new ApplicationUser
                {
                    UserName = person.UserName,
                    EmployeeID = int.Parse(person.UserId)

                }, defaultPassword);
                if (result.Succeeded)
                    userManager.AddToRole(userManager.FindByName(person.UserName).Id, Settings.MechanicRole);
            }

            IEnumerable<SetupUserInfo> trackservices = controller.GetTrackServices();
            foreach (var person in trackservices)
            {
                result = userManager.Create(new ApplicationUser
                {
                    UserName = person.UserName,
                    EmployeeID = int.Parse(person.UserId)

                }, defaultPassword);
                if (result.Succeeded)
                    userManager.AddToRole(userManager.FindByName(person.UserName).Id, Settings.TrackServiceRole);
            }

            IEnumerable<SetupUserInfo> foodservices = controller.GetFoodServices();
            foreach (var person in foodservices)
            {
                result = userManager.Create(new ApplicationUser
                {
                    UserName = person.UserName,
                    EmployeeID = int.Parse(person.UserId)

                }, defaultPassword);
                if (result.Succeeded)
                    userManager.AddToRole(userManager.FindByName(person.UserName).Id, Settings.FoodServiceRole);
            }

            IEnumerable<SetupUserInfo> shops = controller.GetShops();
            foreach (var person in shops)
            {
                result = userManager.Create(new ApplicationUser
                {
                    UserName = person.UserName,
                    EmployeeID = int.Parse(person.UserId)

                }, defaultPassword);
                if (result.Succeeded)
                    userManager.AddToRole(userManager.FindByName(person.UserName).Id, Settings.ShopRole);
            }

            IEnumerable<SetupUserInfo> clerks = controller.GetClerks();
            foreach (var person in clerks)
            {
                result = userManager.Create(new ApplicationUser
                {
                    UserName = person.UserName,
                    EmployeeID = int.Parse(person.UserId)

                }, defaultPassword);
                if (result.Succeeded)
                    userManager.AddToRole(userManager.FindByName(person.UserName).Id, Settings.ClerkRole);
            }

            IEnumerable<SetupUserInfo> officemanagers = controller.GetOfficeManagers();
            foreach (var person in officemanagers)
            {
                result = userManager.Create(new ApplicationUser
                {
                    UserName = person.UserName,
                    EmployeeID = int.Parse(person.UserId)

                }, defaultPassword);
                if (result.Succeeded)
                    userManager.AddToRole(userManager.FindByName(person.UserName).Id, Settings.OfficeManagerRole);
            }



            base.Seed(context);
        }
    }
}