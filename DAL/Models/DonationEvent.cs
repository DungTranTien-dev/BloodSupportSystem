﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class DonationEvent
    {
        public Guid DonationEventId { get; set; }

        public string Title { get; set; }

        public string Location { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

        public string Description { get; set; }

        public ICollection<BloodRegistration> Registrations { get; set; }
    }

}
