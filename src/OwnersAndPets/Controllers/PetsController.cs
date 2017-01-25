using Microsoft.AspNetCore.Mvc;
using OwnersAndPets.Models;
using System.Collections.Generic;
using System.Linq;

namespace OwnersAndPets.Controllers
{
    public class PetsController : Controller
    {
        public JsonResult GetPets([FromBody] Owner owner)
        {
            List<Pet> pets = new List<Pet>();

            using (DatabaseContext dbContext = new DatabaseContext())
            {
                pets = dbContext.Pets.Where(x => x.Owner.Id == owner.Id).ToList();
            }

            return Json(pets);
        }

        public string GetOwner([FromBody] Owner owner)
        {
            string newOwner = "";

            using (DatabaseContext dbContext = new DatabaseContext())
            {
                newOwner = dbContext.Owners.Where(x => x.Id == owner.Id).Select(y => y.Name).First().ToString();
            }

            return newOwner;
        }

        public string AddPet([FromBody] Pet pet)
        {
            try
            {
                using (DatabaseContext dbContext = new DatabaseContext())
                {
                    Pet newPet = new Pet { Name = pet.Name, OwnerId = pet.OwnerId };
                    dbContext.Pets.Add(newPet);
                    dbContext.SaveChanges();

                    Owner owner = dbContext.Owners.Where(x => x.Id == pet.OwnerId).FirstOrDefault();
                    owner.PetsCount++;
                    dbContext.Owners.Update(owner);
                    dbContext.SaveChanges();
                }

                return "Added new pet '" + pet.Name + "'";
            }
            catch
            {
                return "Error. New pet did`t add";
            }
        }

        public int GetTotalCount([FromBody] Pet pet)
        {
            int count = 0;

            using (DatabaseContext dbContext = new DatabaseContext())
            {
                count = dbContext.Pets.Where(x => x.OwnerId == pet.OwnerId).Count();
            }

            return count;
        }

        public string DeletePet([FromBody] Pet pet)
        {
            try
            {
                using (DatabaseContext dbContext = new DatabaseContext())
                {
                    Pet delPet = dbContext.Pets.Where(x => x.Id == pet.Id).FirstOrDefault();
                    pet.Name = delPet.Name;
                    dbContext.Remove(delPet);
                    dbContext.SaveChanges();

                    Owner owner = dbContext.Owners.Where(x => x.Id == delPet.OwnerId).FirstOrDefault();
                    owner.PetsCount--;
                    dbContext.Owners.Update(owner);
                    dbContext.SaveChanges();
                }

                return "Deleted new pet '" + pet.Name + "'";
            }
            catch
            {
                return "Error. New pet did`t delete";
            }
        }
    }
}