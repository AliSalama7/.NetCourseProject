namespace project1.Data
{
    public class User : BaseEntity
    {
        public string FName { get; set; }
        public string SName { get; set; }
        public string Email { get; set; }
        public DateTime RegistrationDate { get; set; }
        public string ShippingAddress { get; set; }
        public string PhoneNumber { get; set; }
    }
}
