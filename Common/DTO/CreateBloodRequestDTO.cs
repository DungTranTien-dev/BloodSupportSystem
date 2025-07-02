using Common.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.DTO
{
    public class CreateBloodRequestDTO
    {
        

        
        public string PatientName { get; set; }         // Tên người cần máu
        public string HospitalName { get; set; }        // Nơi cần máu (nếu khác bệnh viện hệ thống)

        public string BloodGroup { get; set; }          // Nhóm máu yêu cầu (A+, O-, ...)
        public string ComponentType { get; set; } // Loại máu: RBC, Plasma,...
        public double VolumeInML { get; set; }          // Lượng cần (VD: 350ml, 500ml)

        public string Reason { get; set; }              // Lý do (vd: tai nạn, truyền định kỳ,…)
        //public DateTime RequestedDate { get; set; }     // Ngày yêu cầu

        //public double Latitue { get; set; }
        //public double Longtitue { get; set; }
    }
}
