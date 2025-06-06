using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class BloodUnit
    {
        public int Id { get; set; }
        public int BloodTypeId { get; set; }
        public string ComponentType { get; set; }
        public int Quantity { get; set; }
        public DateTime UpdatedAt { get; set; }

        // Navigation property
        public BloodType BloodType { get; set; }
    }
}
