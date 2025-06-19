using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class UserMedicalChronicDisease
    {
        public Guid UserMedicalChronicDiseaseId { get; set; }
        public Guid UserMedicalId { get; set; }
        public UserMedical UserMedical { get; set; }

        public Guid ChronicDiseaseId { get; set; }
        public ChronicDisease ChronicDisease { get; set; }
    }
}
