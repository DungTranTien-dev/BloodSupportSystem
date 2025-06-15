using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class Location
    {
        public int Id { get; set; }
        public string Address { get; set; }
        public float ?Latitude { get; set; }
        public float ?Longitude { get; set; }

        // Navigation property
        public ICollection<User> Users { get; set; }
        public ICollection<DonationHistory> DonationHistories { get; set; }
    }
}
