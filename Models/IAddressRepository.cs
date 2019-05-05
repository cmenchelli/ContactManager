using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactManager.Models
{
    public interface IAddressRepository
    {
        Task<IEnumerable<Address>> GetAll();

        Task<Address> GetById(int? id);
        Task Insert(Address address);

        Task Update(Address address);
        Task Delete(int ID);
        Task Save();
        IEnumerable<Address> GetByContactId(int contactId);
    }
}
