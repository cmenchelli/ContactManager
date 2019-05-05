using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactManager.Models
{
    public interface IContactRepository
    {
        Task<IEnumerable<Contact>>  GetAll();
        Task<Contact> GetById(int? id);
        Task Insert(Contact contact);
        Task Update(Contact contact);
        Task Delete(int ID);
        Task Save();
    }
}
