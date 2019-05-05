namespace ContactManager
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using ContactManager.Models;

    public partial class ContactDbContext : DbContext
    {
        public ContactDbContext()
            : base("name=ContactDbContext")
        {
        }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<Contact>()
                .HasMany(c => c.AddressList);

            modelBuilder
                .Entity<Address>()
                .HasMany(c => c.ContactList);
        }

        public System.Data.Entity.DbSet<ContactManager.Models.Address> Addresses { get; set; }
        public System.Data.Entity.DbSet<ContactManager.Models.Contact> Contacts { get; set; }

        public System.Data.Entity.DbSet<ContactManager.Models.SummaryViewModel> SummaryViewModels { get; set; }
    }
}
