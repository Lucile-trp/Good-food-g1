using System;

namespace Host.Dto
{
    public class UserDto
    {
        public int UserId { get; set; }
        public String Mail { get; set; }
        public String Password { get; set; }
        public String FirstName { get; set; }
        public String LastName { get; set; }
        public String Phone { get; set; }
        public String Address { get; set; }
        public String Zip { get; set; }
        public String City { get; set; }
        public String Country { get; set; }
    }
}