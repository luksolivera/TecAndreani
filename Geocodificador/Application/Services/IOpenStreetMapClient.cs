using Refit;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public interface IOpenStreetMapClient
    {
        [Get("/search")]
        Task<ApiResponse<GetGelocationResponse>> GetGelocation([Query] string q, [Query] string format = "json");
    }

    public class GetGelocationResponse
    {
        public string Lat { get; set; }
        public string Lon { get; set; }
    }
}
