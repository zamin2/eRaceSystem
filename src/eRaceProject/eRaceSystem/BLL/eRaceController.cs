using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Threading.Tasks;
using eRaceSystem.Entities;
using eRaceSystem.DAL;

using eRaceSystem.ViewModels;


namespace eRaceSystem.BLL
{
    [DataObject]
    public class eRaceController
    {
        [DataObjectMethod(DataObjectMethodType.Select)]
        public List<Users> ListEmployeebyPosition()
        {
            using (var context = new eRaceContext())
            {

                var result = from staff in context.Employees
                             orderby staff.PositionID ascending
                             select new Users
                             {
                                 UserName = staff.FirstName + "." + staff.LastName,
                                 EmployeeRole = staff.Position.Description,
                                 Password = "Pa$$word1"

                             };
                return result.ToList();
            }
        }

        public Users GetEmployeeName(int? EmployeeID)
        {
            using(var context = new eRaceContext())
            {
                var result = from staff in context.Employees
                             where staff.EmployeeID == EmployeeID
                             select new Users
                             {
                                 UserName = staff.FirstName + " " + staff.LastName,
                                 EmployeeRole = staff.Position.Description
                             };
                return result.SingleOrDefault();
            }
        }

    }
}
