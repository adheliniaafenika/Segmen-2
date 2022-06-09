using API.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Repository.Interface
{
    interface IAccountRoleRepository
    {
        bool InsertDataManager(string nik);
    }
}

