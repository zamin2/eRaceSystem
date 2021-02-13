using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eRaceSystem.ViewModels;
using eRaceSystem.DAL;

namespace eRaceSystem.BLL
{
    public class SetupUserRegistrationController
    {
        public IEnumerable<SetupUserInfo> GetDirectors()
        {
            using (var context = new eRaceContext())
            {
                var results = from staff in context.Employees.ToList()
                              where staff.PositionID == 1
                              select new SetupUserInfo
                              {
                                  UserId = staff.EmployeeID.ToString(),
                                  UserName = $"{staff.FirstName}.{staff.LastName}"
                                  
                              };
                return results;
            }
        }

        public IEnumerable<SetupUserInfo> GetCoordinators()
        {
            using (var context = new eRaceContext())
            {
                var results = from staff in context.Employees.ToList()
                              where staff.PositionID == 2
                              select new SetupUserInfo
                              {
                                  UserId = staff.EmployeeID.ToString(),
                                  UserName = $"{staff.FirstName}.{staff.LastName}"

                              };
                return results;
            }
        }

        public IEnumerable<SetupUserInfo> GetInvestigators()
        {
            using (var context = new eRaceContext())
            {
                var results = from staff in context.Employees.ToList()
                              where staff.PositionID == 3
                              select new SetupUserInfo
                              {
                                  UserId = staff.EmployeeID.ToString(),
                                  UserName = $"{staff.FirstName}.{staff.LastName}"

                              };
                return results;
            }
        }

        public IEnumerable<SetupUserInfo> GetSeniorMechanics()
        {
            using (var context = new eRaceContext())
            {
                var results = from staff in context.Employees.ToList()
                              where staff.PositionID == 4
                              select new SetupUserInfo
                              {
                                  UserId = staff.EmployeeID.ToString(),
                                  UserName = $"{staff.FirstName}.{staff.LastName}"

                              };
                return results;
            }
        }

        public IEnumerable<SetupUserInfo> GetMechanics()
        {
            using (var context = new eRaceContext())
            {
                var results = from staff in context.Employees.ToList()
                              where staff.PositionID == 5
                              select new SetupUserInfo
                              {
                                  UserId = staff.EmployeeID.ToString(),
                                  UserName = $"{staff.FirstName}.{staff.LastName}"

                              };
                return results;
            }
        }

        public IEnumerable<SetupUserInfo> GetTrackServices()
        {
            using (var context = new eRaceContext())
            {
                var results = from staff in context.Employees.ToList()
                              where staff.PositionID == 6
                              select new SetupUserInfo
                              {
                                  UserId = staff.EmployeeID.ToString(),
                                  UserName = $"{staff.FirstName}.{staff.LastName}"

                              };
                return results;
            }
        }

        public IEnumerable<SetupUserInfo> GetFoodServices()
        {
            using (var context = new eRaceContext())
            {
                var results = from staff in context.Employees.ToList()
                              where staff.PositionID == 7
                              select new SetupUserInfo
                              {
                                  UserId = staff.EmployeeID.ToString(),
                                  UserName = $"{staff.FirstName}.{staff.LastName}"

                              };
                return results;
            }
        }

        public IEnumerable<SetupUserInfo> GetShops()
        {
            using (var context = new eRaceContext())
            {
                var results = from staff in context.Employees.ToList()
                              where staff.PositionID == 8
                              select new SetupUserInfo
                              {
                                  UserId = staff.EmployeeID.ToString(),
                                  UserName = $"{staff.FirstName}.{staff.LastName}"

                              };
                return results;
            }
        }

        public IEnumerable<SetupUserInfo> GetClerks()
        {
            using (var context = new eRaceContext())
            {
                var results = from staff in context.Employees.ToList()
                              where staff.PositionID == 9
                              select new SetupUserInfo
                              {
                                  UserId = staff.EmployeeID.ToString(),
                                  UserName = $"{staff.FirstName}.{staff.LastName}"

                              };
                return results;
            }
        }

        public IEnumerable<SetupUserInfo> GetOfficeManagers()
        {
            using (var context = new eRaceContext())
            {
                var results = from staff in context.Employees.ToList()
                              where staff.PositionID == 10
                              select new SetupUserInfo
                              {
                                  UserId = staff.EmployeeID.ToString(),
                                  UserName = $"{staff.FirstName}.{staff.LastName}"

                              };
                return results;
            }
        }


    }
}
