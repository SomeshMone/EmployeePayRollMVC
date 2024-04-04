using CommonLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interfaces
{
    public interface  IEmployeeBusiness
    {
        public IEnumerable<Employee> GetAllEmployees();
        public bool AddEmployee(Employee employee);
        public bool EmployeeUpdate(Employee employee);
        public bool DeleteEmployee(int id);
        public Employee GetEmployeeById(int id);
        public Employee Login(LoginModel model);

        public Employee GetEmployeeBYName(string name);

        public bool InsertUpdate(Employee employee);


    }
}
