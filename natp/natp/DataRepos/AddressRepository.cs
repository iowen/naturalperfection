using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace natp.DataRepos
{
    public class AddressRepository :IAddressRepository
    {
        private npDataContext db;
        public AddressRepository(npDataContext context)
        {
            this.db = context;
        }
        public List<Address> getAllAddresses()
        {
            return this.db.Addresses.ToList<Address>();
        }
        public Address getAddress(int id)
        {
            Address result;
            try
            {
                Address address = this.db.Addresses.Single((Address a) => a.AddressId == id);
                result = address;
            }
            catch
            {
                result = new Address
                {
                    AddressId = -1
                };
            }
            return result;
        }

        public int addAddress(Address address)
        {
            this.db.Addresses.InsertOnSubmit(address);
            this.db.SubmitChanges();
            return address.AddressId;
        }
        public bool updateAddress(Address address)
        {
            Address adr = this.getAddress(address.AddressId);
            if (adr.AddressId > 0)
            {
               
                this.db.SubmitChanges();
                return true;
            }
            return false;
        }
        //public AccountInfoModel login(LoginModel model)
        //{
        //    AccountInfoModel accountInfoModel = new AccountInfoModel();
        //    AccountInfoModel result;
        //    try
        //    {
        //        Account account = this.db.Accounts.Single((Account a) => a.Email == model.Email && a.Password == model.Password);
        //        if (account.AccountId > 0)
        //        {
        //            accountInfoModel.account = account;
        //        }
        //        else
        //        {
        //            accountInfoModel.account = new Account
        //            {
        //                AccountId = -1
        //            };
        //        }
        //        result = accountInfoModel;
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.Write(ex.Message);
        //        accountInfoModel.account = new Account
        //        {
        //            AccountId = -1
        //        };
        //        result = accountInfoModel;
        //    }
        //    return result;
        //}
        //public bool emailAvailable(string email)
        //{
        //    return (
        //        from a in this.db.Accounts
        //        where a.Email == email
        //        select a).Any<Account>();
        //}
    }
}