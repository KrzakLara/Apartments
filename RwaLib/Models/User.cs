using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RwaLib.Models
{
    public class User
    {
        public int Id { get; set; }
        public Guid Guid { get; set; }
        public string UserName { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string Details { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public DateTime CreatedAt { get; set; }
        public string FirstName
        {
            get
            {
                return UserName.Split(' ')[0];
            }
        }
        public string LastName
        {
            get
            {
                if (!UserName.Contains(" "))
                    return UserName.Split(' ')[0];

                return UserName.Split(' ')[1];
            }
        }
    }
}
