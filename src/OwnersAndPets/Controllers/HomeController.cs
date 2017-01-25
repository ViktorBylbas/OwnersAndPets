using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using OwnersAndPets.Models;
using Microsoft.EntityFrameworkCore;

namespace OwnersAndPets.Controllers
{
    public class HomeController : Controller
    {
        public string AddOwner([FromBody] Owner owner)
        {
            try
            {
                using (DatabaseContext dbContext = new DatabaseContext())
                {
                    Owner newOwner = new Owner { Name = owner.Name, PetsCount = 0 };
                    dbContext.Owners.Add(newOwner);
                    dbContext.SaveChanges();
                }

                return "Added new owner '" + owner.Name + "'";
            }
            catch
            {
                return "Error. New owner did`t add";
            }
        }

        public JsonResult GetOwners()
        {
            List<Owner> owners = new List<Owner>();

            using (DatabaseContext dbContext = new DatabaseContext())
            {
                owners = dbContext.Owners.ToList();
            }

            return Json(owners);
        }

        public int GetTotalCount()
        {
            int count = 0;

            using (DatabaseContext dbContext = new DatabaseContext())
            {
                count = dbContext.Owners.ToList().Count();
            }

            return count;
        }

        public string DeleteOwner([FromBody] Owner owner)
        {
            try
            {
                using (DatabaseContext dbContext = new DatabaseContext())
                {
                    List<Pet> delPets = dbContext.Pets.Where(x => x.OwnerId == owner.Id).ToList();
                    dbContext.RemoveRange(delPets);
                    dbContext.SaveChanges();

                    Owner delOwner = dbContext.Owners.Where(x => x.Id == owner.Id).FirstOrDefault();
                    owner.Name = delOwner.Name;
                    dbContext.Owners.Remove(delOwner);
                    dbContext.SaveChanges();
                }

                return "Deleted owner '" + owner.Name + "'";
            }
            catch
            {
                return "Error. New owner did`t delete";
            }
        }
    }
}
