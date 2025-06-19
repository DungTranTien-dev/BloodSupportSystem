using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class ChronicDisease
    {
        public Guid ChronicDiseaseId { get; set; }
        public string ChronicDiseaseName { get; set; }

        // Navigation - Many-to-Many
        public ICollection<UserMedicalChronicDisease> UserMedicalChronicDiseases { get; set; }
    }
}
