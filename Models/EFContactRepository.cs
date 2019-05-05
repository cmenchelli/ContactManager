using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace ContactManager.Models
{
    public class EFContactRepository : IContactRepository
    {
        private readonly ContactDbContext _context;
        public EFContactRepository()
        {
            _context = new ContactDbContext();
        }
        public EFContactRepository(ContactDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Contact>> GetAll()
        {
            return await _context.Contacts.ToListAsync();
        }
        public async Task<Contact>  GetById(int? Id)
        {
            return await _context.Contacts.FindAsync(Id);
        }
        public async Task Insert(Contact contact)
        {
             _context.Contacts.Add(contact);
        }
        public async Task  Update(Contact contact)
        {
             _context.Entry(contact).State = EntityState.Modified;
        }
        public async Task Delete(int ID)
        {
            Contact contact = await _context.Contacts.FindAsync(ID);
            _context.Contacts.Remove(contact);
        }
        public async Task Save()
        {
           await  _context.SaveChangesAsync();
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
    }
}
