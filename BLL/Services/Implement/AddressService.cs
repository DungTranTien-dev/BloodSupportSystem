using BLL.Services.Interface;
using Common.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services.Implement
{
    public class AddressService : IAddressService
    {
        public async Task<(double lat, double lon)> GetCoordinatesFromAddress(string address)
        {
            var accessToken = "pk.eyJ1IjoiZHVuZ2RldjExMyIsImEiOiJjbWNicWJnd2owYzF2MmtvbHRjbTI3c3Z6In0.GxTBXw4sDwC2RAzMpNPMRA"; // 👉 Bạn thay bằng token thật
            var encodedAddress = Uri.EscapeDataString(address);
            var url = $"https://api.mapbox.com/geocoding/v5/mapbox.places/{encodedAddress}.json?access_token={accessToken}";

            using var httpClient = new HttpClient();
            var response = await httpClient.GetAsync(url);

            if (!response.IsSuccessStatusCode)
                throw new Exception("Không thể lấy tọa độ từ MapBox");

            var json = await response.Content.ReadAsStringAsync();

            using var doc = System.Text.Json.JsonDocument.Parse(json);
            var features = doc.RootElement.GetProperty("features");
            if (features.GetArrayLength() == 0)
                throw new Exception("Không tìm thấy địa chỉ phù hợp");

            var center = features[0].GetProperty("center"); // [lon, lat]
            double longitude = center[0].GetDouble();
            double latitude = center[1].GetDouble();

            return (latitude, longitude);
        }
    }
}
