using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace ContactManager.Models
{
    public class EFAddressRepository : IAddressRepository
    {
        private readonly ContactDbContext _context;

        public EFAddressRepository()
        {
            _context = new ContactDbContext();
        }

        public EFAddressRepository(ContactDbContext context)
        {
            _context = context;
        }

        public async Task <IEnumerable<Address>> GetAll()
        {
            return await _context.Addresses.ToListAsync();
        }

        public async Task<Address> GetById(int? Id)
        {
            return await _context.Addresses.FindAsync(Id);
        }

        public async Task Insert(Address address)
        {
              _context.Addresses.Add(address);
        }

        public async Task Update(Address address)
        {
            _context.Entry(address).State = EntityState.Modified;
        }

        public async Task Delete(int ID)
        {
            Address address = _context.Addresses.Find(ID);
            _context.Addresses.Remove(address);
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }

        private bool disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this.disposed = true;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public  IEnumerable<Address> GetByContactId(int contactId)
        {
            return _context
                .Contacts
                .Find(contactId)
                .AddressList;
        }
    }
}