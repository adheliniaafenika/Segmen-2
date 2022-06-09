using API.Context;
using API.Models;
using API.Models.ViewModels;
using API.Repository.Interface;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Repository.Data
{
    public class AccountRoleRepository 
    {
        private readonly MyContext myContext;

        public AccountRoleRepository(MyContext myContext) 
        {
            this.myContext = myContext;
        }

        public int InsertDataManager(SignManagerVM signManagerVM)
        {
            // mencari data employee yg akan menjadi manager (bedasarkan nik)

            // apakah role di nik tsb sudah manager atau blm
            AccountRole AccountRole1 = myContext.AccountRoles.Where(ar => ar.Account_NIK == signManagerVM.NIK && ar.Role_Id == 2).SingleOrDefault();

            if (AccountRole1 != null) // jika role di nik tsb sudah manager
            {
                return 400;
            }

            AccountRole AccountRole2 = new AccountRole // jika role di nik tsb blm jd manager
            {
                Account_NIK = signManagerVM.NIK,
                Role_Id = 2
            };

            myContext.AccountRoles.Add(AccountRole2);
            var result = myContext.SaveChanges();
            return result;
        }
    }
}


