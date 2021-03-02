using Flunt.Notifications;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace Domain.Errors
{
    public class ApiResponse<T>
    {
        public T Content { get; set; }
        public IReadOnlyCollection<Notification> Notifications { get; set; }
        public bool Valid { get => !Notifications.Any(); }
        public bool Invalid { get => Notifications.Any(); }
        public HttpStatusCode StatusCode { get; set; }
        public ApiResponse(T content)
        {
            Content = content;
            Notifications = new List<Notification>();
        }

        public ApiResponse(IReadOnlyCollection<Notification> notifications, HttpStatusCode statusCode = HttpStatusCode.BadRequest)
        {
            Notifications = notifications;
            StatusCode = statusCode;
        }
    }
}
