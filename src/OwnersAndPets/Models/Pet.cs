namespace OwnersAndPets.Models
{
    public class Pet
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public int OwnerId { get; set; }
        public virtual Owner Owner { get; set; }
    }
}
