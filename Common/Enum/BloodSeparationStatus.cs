using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Enum
{
    public enum BloodSeparationStatus
    {
        UNPROCESSED,    // Chưa tách
        PROCESSING,     // Đang xử lý
        PROCESSED,      // Đã tách
        ERROR           // Lỗi
    }

}
