using Common.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services.Interface
{
    public interface IAddressService
    {
        Task<(double lat, double lon)> GetCoordinatesFromAddress(string address);
    }
}
