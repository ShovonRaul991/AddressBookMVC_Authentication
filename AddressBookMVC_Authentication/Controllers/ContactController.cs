
using AddressBookMVC_Authentication.Models;
using AddressBookMVC_Authetication.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;

namespace AddressBookMVC_AUthentication.Controllers
{
    [Authorize]
    public class ContactController : Controller
    {
        private readonly AddressBookDbContextMVC _context;

        public ContactController(AddressBookDbContextMVC db)
        {
            _context = db;
        }
        public IActionResult Index()
        {
            //var AddressSelected = _context.AddressesMVC.FirstOrDefault();

            return View();
        }

        public IActionResult Display(int id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var AddressSelected = _context.AddressesMVC.Find(id);
            if (AddressSelected == null)
            {
                return NotFound();
            }
            return View(AddressSelected);
        }

        [HttpGet]
        public IActionResult Edit(int id) 
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var AddressSelected = _context.AddressesMVC.Find(id);
            if (AddressSelected == null)
            {
                return NotFound();
            }
            return View(AddressSelected);
        }

        public IActionResult Add()
        {
            var AddressSelected = _context.AddressesMVC.FirstOrDefault();
            return View();
        }

        [HttpPost]
        public IActionResult Update(string Name, string Email, long mobile, long landline, string website, string address, int id)
        {
            Regex regexforEmail = new Regex(@"^[0-9a-z.\s+_]+@[0-9a-z-.+]+\.[a-z]{2,4}$", RegexOptions.CultureInvariant | RegexOptions.Singleline);

            var AddressSelected = _context.AddressesMVC.Find(id);
            if (Name != null && Email != null && landline != 0 && mobile != 0 && landline.ToString().Length == 10 && mobile.ToString().Length == 10
                && website != null && address != null)
            {
                bool isValidEmail = regexforEmail.IsMatch(Email);
                if (isValidEmail)
                {
                    var x = _context.AddressesMVC.Where(p => p.Id == id);
                    x.ToList()[0].Name = Name;
                    x.ToList()[0].Email = Email;
                    x.ToList()[0].MobileNumber = mobile;
                    x.ToList()[0].LandLineNumber = landline;
                    x.ToList()[0].Website = website;
                    x.ToList()[0].AddressDetails = address;
                    _context.SaveChanges();

                }

            }
            return RedirectToAction("Display", "Contact", new { @id = AddressSelected?.Id });
        }

        [HttpPost]
        public IActionResult Create(string Name, string Email, long mobile, long landline, string website, string address)
        {
            Regex regexforEmail = new Regex(@"^[0-9a-z.\s+_]+@[0-9a-z-.+]+\.[a-z]{2,4}$", RegexOptions.CultureInvariant | RegexOptions.Singleline);


            if (Name != null && Email != null && landline != 0 && mobile != 0 && landline.ToString().Length == 10 && mobile.ToString().Length == 10
                && website != null && address != null)
            {
                bool isValidEmail = regexforEmail.IsMatch(Email);
                if (isValidEmail)
                {
                    _context.AddressesMVC.Add(new Contact() { Name = Name, Email = Email, MobileNumber = mobile, LandLineNumber = landline, Website = website, AddressDetails = address });
                    _context.SaveChanges();
                    var SelectedAddress = _context.AddressesMVC.OrderBy(e => e.Id).Last();
                    return RedirectToAction("Display", "Contact", new { @id = SelectedAddress.Id });

                }

            }

            return RedirectToAction("Showfirst", "Contact");
        }

       
        public IActionResult Delete(int id)
        {
            var x = _context.AddressesMVC.Where(x => x.Id == id).ToList()[0];
            //var PreviousAddress = _context.Addresses.OrderBy(e => e.Id).Last();
            _context.AddressesMVC.Remove(x);
            _context.SaveChanges();
            if (_context.AddressesMVC.Count() != 0)
            {
                var PreviousAddress = _context.AddressesMVC.OrderBy(e => e.Id).Last();
                return RedirectToAction("Display", "Contact", new { @id = PreviousAddress.Id });
            }
            return RedirectToAction("Showfirst", "Contact");
        }
        
    }
}
