using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.DTO
{
    public class CreateEventDTO
    {
        public string EventName { get; set; } = string.Empty;
        public DateTime EventDate { get; set; }
        public string EventLocation { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public Guid OrganizerId { get; set; }
    }
}
