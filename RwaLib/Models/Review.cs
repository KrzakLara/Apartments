using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RwaLib.Models
{
    public class Review
    {
        public int UserId { get; set; }
        public int ApartmentId { get; set; }
        public string Description { get; set; }
        public int Stars { get; set; }
    }
}
