using Refit;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Services
{
    public interface IOpenStreetMapClient
    {
        [Get("/search")]
        Task<ApiResponse<List<GetGelocationResponse>>> GetGelocation(string q, [Query] string format = "json", [Query] string limit = "1");
    }

    public class GetGelocationResponse
    {
        public string Lat { get; set; }
        public string Lon { get; set; }
    }
}
