using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace natp.DataRepos
{
    public interface IAccountRepository
    {
        List<Account> getAllAccounts();
        Account getAccount(int id);
        Account getAccount(string email);
        Account getAccount(string email, string pwd);
        int addAccount(Account account);
        bool updateAccount(int id, string email, string pwd);
        bool updateAccountPassword(int id,  string NewPassword);
        bool updateAccountProfile(int id, string FirstName, string LastName);
        void setIsActive(int id, bool active);
       // AccountInfoModel login(LoginModel model);
       // bool emailAvailable(string email);
    }
}