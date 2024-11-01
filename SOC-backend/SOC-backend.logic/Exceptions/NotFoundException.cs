using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SOC_backend.logic.Exceptions
{
    public class NotFoundException : Exception
    {
        public override string Message { get; }
        public HttpStatusCode StatusCode;

        public NotFoundException(string item, int id)
        {
            Message = $"{item} with the id: {id} could not be found.";
            StatusCode = HttpStatusCode.NotFound;
        }

        public NotFoundException(string item)
        {
            Message = $"No {item} could be found..";
            StatusCode = HttpStatusCode.NotFound;
        }
    }
}
