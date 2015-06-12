using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace natp.DataRepos
{
    public interface IAddressRepository
    {
        List<Address> getAllAddresses();
        Address getAddress(int id);
        int addAddress(Address address);
        bool updateAddress(Address address);
    }
}