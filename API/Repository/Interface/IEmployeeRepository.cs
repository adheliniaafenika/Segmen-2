// harusnya dihapus
using API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Repository.Interface
{
    interface IEmployeeRepository
    {
        IEnumerable<Employee> Get(); //krn datanya biasanya > 1 -> khusus utk get | getAll
        Employee GetByNIK(string NIK);
        int Insert(Employee employee); //-> berhasil >= 1 / 2 / 3 sesuai jml datanya, kalo null berarti gagal
        int Update(Employee employee);
        int Delete(string NIK);
    }
}
