
using System;
using System.Collections.Generic;
using System.Text;
using BusinessLayer.Interfaces;
using RepositoryLayer.Interfaces;
using CommonLayer.Models;

namespace BusinessLayer.Services
{
    public class EmployeeBusiness:IEmployeeBusiness
    {
        private readonly IEmployeeRepository _emprepository;
        public EmployeeBusiness(IEmployeeRepository emprepository)
        {
            _emprepository = emprepository;
        }
    
        public IEnumerable<Employee> GetAllEmployees()
        {
            return _emprepository.GetAllEmployees();

        }
        public bool AddEmployee(Employee employee)
        {
            return _emprepository.AddEmployee(employee);
        }
        public bool EmployeeUpdate(Employee employee)
        {
            return _emprepository.EmployeeUpdate(employee);
        }

        public bool DeleteEmployee(int id)
        {
            return _emprepository.DeleteEmployee(id);
        }
        public Employee GetEmployeeById(int id)
        {
            return _emprepository.GetEmployeeById(id);
        }
        public Employee Login(LoginModel model)
        {
            return _emprepository.Login(model);
        }

        public Employee GetEmployeeBYName(string name)
        {
            return _emprepository.GetEmployeeBYName(name);
        }
        public bool InsertUpdate(Employee employee)
        {
            return _emprepository.InsertUpdate(employee);
        }



    }
}
