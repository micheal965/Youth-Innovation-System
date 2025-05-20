namespace Youth_Innovation_System.Core.Entities
{
    public class BaseEntity
    {
        public int Id { get; set; }
        public string? CreatedBy { get; set; }
        public string? ModifidBy { get; set; }
        public string? DeletedBy { get; set; }
        public bool isDeleted { get; set; } = false;

    }
}
