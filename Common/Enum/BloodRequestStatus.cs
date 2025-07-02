using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Enum
{
    public enum BloodRequestStatus
    {
        PENDING,
        APPROVED,
        WAITING_PAYMENT, // Đang chờ thanh toán
        REJECTED,
        FULFILLED      // Đã cấp máu thành công
    }

}
