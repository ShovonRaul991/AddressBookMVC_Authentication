using AddressBookMVC_Authentication.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AddressBookMVC_Authetication.Data
{
    public class AddressBookDbContextMVC:IdentityDbContext
    {
        public AddressBookDbContextMVC(DbContextOptions<AddressBookDbContextMVC> options) : base(options) { }

        public DbSet<Contact> AddressesMVC { get; set; }
    }
}
