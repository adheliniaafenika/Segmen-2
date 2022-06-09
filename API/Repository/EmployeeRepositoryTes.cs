// harusnya dihapus
using API.Context;
using API.Models;
using API.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Repository
{
    public class EmployeeRepositoryTes : IEmployeeRepository
    {
        private readonly MyContext context;
        public EmployeeRepositoryTes(MyContext context)
        {
            this.context = context;
        }
        public int Delete(string NIK)
        {
            var entity = context.Employees.Find(NIK);
            context.Remove(entity);
            var result = context.SaveChanges();
            return result;
        }

        public Employee GetByNIK(string NIK)
        {
            //var tes = context.Employees.Where(e => e.FirstName == "Nia").SingleOrDefault<Employee>();
            return context.Employees.Find(NIK);
            //return context.Employees.First(e => e.FirstName = "Andi");
        }

        public IEnumerable<Employee> Get()
        {
            return context.Employees.ToList();
        }

        public int Insert(Employee employee)
        {
            context.Employees.Add(employee);
            var result = context.SaveChanges();
            return result;
        }

        public int Update(Employee employee)
        {
            context.Entry(employee).State = EntityState.Modified;
            var result = context.SaveChanges();
            return result;
        }
    }
}
