using System.Collections.Generic;

namespace OwnersAndPets.Models
{
    public class Owner
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int PetsCount { get; set; }
        public virtual List<Pet> Pets { get; set; }
        public Owner()
        {
            Pets = new List<Pet>();
        }
    }
}