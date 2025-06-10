using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class Blood
    {
        public Guid BloodId { get; set; }
        public string BloodName { get; set; }
        public int TotalQuantity { get; set; }
    }
}
