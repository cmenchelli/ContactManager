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
    public class AddressesController : Controller
    {
        //  private ContactDbContext db = new ContactDbContext();
        private IAddressRepository addressRepo;
        private IContactRepository contactRepo;

        public AddressesController()
        {
            var db = new ContactDbContext();

            this.addressRepo = new EFAddressRepository(db);
            this.contactRepo = new EFContactRepository(db);
        }

        public AddressesController(IAddressRepository addressRepository)
        {
            this.addressRepo = addressRepository;
        }

        // GET: Addresses
        public async Task<ViewResult> ListAll()
        {
            return View( await addressRepo.GetAll());
        }

        // GET: Addresses
        public ViewResult Index(int contactId)
        {
            ViewBag.ContactId = contactId;
            return View(addressRepo.GetByContactId(contactId));
        }

        // GET: Addresses/Details/5
        public async Task<ActionResult> Details(int? id, int? contactId)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Address address =  await addressRepo.GetById(id);
            if (address == null)
            {
                return HttpNotFound();
            }

            ViewBag.ContactId = contactId;
            return View(address);
        }

        // GET: Addresses/Create
        public ActionResult Create(int contactId)
        {
            //ViewBag.ContactId = contactId;
            AddressModel addressModel = new AddressModel()
            {
                Id = 0,
                ContactId = contactId
            };
            return View(addressModel);
        }

        // POST: Addresses/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,AddressLine1,AddressLine2,City,StateCode,Zip,AddressType,ContactId")] AddressModel addressModel)
        {
            if (ModelState.IsValid)
            {
                var address = AddressModel.ToAddress(addressModel);

                var contact = await contactRepo.GetById(addressModel.ContactId);
                contact.AddressList.Add(address);
                await contactRepo.Save();
                return RedirectToAction("Index", new { contactId = addressModel.ContactId });
            }

            return View(addressModel);
        }

        // GET: Addresses/Edit/5
        public async Task<ActionResult> Edit(int? id, int? contactId)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Address address =  await addressRepo.GetById(id);
            var addressModel = AddressModel.FromAddress(address, contactId.Value);
            if (address == null)
            {
                return HttpNotFound();
            }
            return View(addressModel);
        }

        // POST: Addresses/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,AddressLine1,AddressLine2,City,StateCode,Zip,AddressType,contactId")] AddressModel addressModel)
        {
            if (ModelState.IsValid)
            {
                var address = AddressModel.ToAddress(addressModel);

                await addressRepo.Update(address);
                await addressRepo.Save();
                return RedirectToAction("Index", new { contactId = addressModel.ContactId });
            }
            return View(addressModel);
        }

        // GET: Addresses/Delete/5
        public async Task<ActionResult> Delete(int? id, int? contactId)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Address address = await addressRepo.GetById(id);
            var addressModel = AddressModel.FromAddress(address, contactId.Value);

            if (address == null)
            {
                return HttpNotFound();
            }

            //ViewBag.ContactId = contactId;
            return View(addressModel);
        }

        // POST: Addresses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id, int contactId)
        {
           
            Address address = await addressRepo.GetById(id);
            await addressRepo.Delete(id);
            await addressRepo.Save();
            return RedirectToAction("Index", new { contactId = contactId });
        }

        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        db.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}
    }
}
