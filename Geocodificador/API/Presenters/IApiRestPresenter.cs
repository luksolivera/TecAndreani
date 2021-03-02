using Domain.Errors;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace API.Presenters
{
    public interface IApiRestPresenter
    {
        IActionResult GetActionResult<T>(ApiResponse<T> result) where T : class;
        IActionResult CustomStatusCodeResult<T>(ApiResponse<T> result, HttpStatusCode statusCode) where T : class;
        IActionResult CreatedAtActionResult<T>(ApiResponse<T> result) where T : class;
    }
}
