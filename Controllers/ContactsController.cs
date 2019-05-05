using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ContactManager;
using ContactManager.Models;

namespace ContactManager.Controllers
{
    [Authorize]
    public class ContactsController : Controller
    {
        private ContactDbContext db = new ContactDbContext();
        private IContactRepository contactRepo;
        public ContactsController()
        {
            this.contactRepo = new EFContactRepository(new ContactDbContext());
        }

        public ContactsController(IContactRepository contactRepository)
        {
            this.contactRepo = contactRepository;
        }
        
        // GET: Contacts
        public async Task<ActionResult> Index()
        {
            var contacts = await contactRepo.GetAll();
            return View(contacts);
        }

        // GET: Contacts/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Contact contacts = await contactRepo.GetById(id);
            if (contacts == null)
            {
                return HttpNotFound();
            }
            return View(contacts);//
        }

        // GET: Contacts/Create
        public ActionResult Create()
        {
            Contact contact = new Contact();
           // ViewBag.AddressID = new SelectList(db.Addresses, "Id", "AddressLine1", contact.AddressID);
            return View();
        }

        // POST: Contacts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,FirstName,LastName,EmailAddress,Birthdate,NumberOfComputers,count,AddressID")] Contact contact)
        {
            if (ModelState.IsValid)
            {
                //db.Contacts.Add(contact);
                //await db.SaveChangesAsync();
               await  contactRepo.Insert(contact);
              await  contactRepo.Save();
                return RedirectToAction("Index");
            }

            //ViewBag.AddressID = new SelectList(db.Addresses, "Id", "AddressLine1", contact.AddressID);
            return View(contact);
        }

        // GET: Contacts/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Contact contact = await contactRepo.GetById(id);
            if (contact == null)
            {
                return HttpNotFound();
            }
            //ViewBag.AddressID = new SelectList(db.Addresses, "Id", "AddressLine1", contact.AddressID);
            return View(contact);
        }

        // POST: Contacts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,FirstName,LastName,EmailAddress,Birthdate,NumberOfComputers,count,AddressID")] Contact contact)
        {
            if (ModelState.IsValid)
            {
                //db.Entry(contact).State = EntityState.Modified;
                //await db.SaveChangesAsync();
                await contactRepo.Update(contact);
                await contactRepo.Save();
                return RedirectToAction("Index");
            }
            //ViewBag.AddressID = new SelectList(db.Addresses, "Id", "AddressLine1", contact.AddressID);
            return View(contact);
        }

        // GET: Contacts/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Contact contact = await db.Contacts.FindAsync(id);
            if (contact == null)
            {
                return HttpNotFound();
            }
            return View(contact);
        }

        // POST: Contacts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            //Contact contact = await db.Contacts.FindAsync(id);
            //db.Contacts.Remove(contact);
            //await db.SaveChangesAsync();
            Contact contact = await contactRepo.GetById(id);
            await contactRepo.Delete(id);
            await contactRepo.Save();
            return RedirectToAction("Index");
        }

        // GET: Summary
        public async Task<ActionResult> Summary()
        {
            var contacts = await contactRepo.GetAll();
            SummaryViewModel sum = new SummaryViewModel();

            foreach (var item in contacts)
            {
                sum.Id++;
                sum.Computers += item.NumberOfComputers;
                sum.Contacts++;
                sum.Addresses += item.AddressList.Count;
                foreach (var addr in item.AddressList)
                {
                    if (addr.AddressType == "Home")
                        sum.HomeAddresses ++;
                    else if (addr.AddressType == "Business")
                        sum.BusinessAddresses ++;
                    else
                        sum.OtherAddresses++;
                }
            }
            return View(sum);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
