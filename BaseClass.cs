namespace BasicContactList
{
    public abstract class BaseClass
    {
        public string? Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? ModifiedAt { get; set; }
    }
}