namespace Abstraction.Entities
{
    public class Profile : BaseEntity
    {
        public string Name { get; set; }
        public string Document { get; set; }
        public string Picture { get; set; }

        public Profile() : base()
        {
        }
    }
}