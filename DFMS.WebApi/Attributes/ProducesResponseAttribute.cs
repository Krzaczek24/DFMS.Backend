using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace DFMS.WebApi.Attributes
{
    public class ProducesResponseAttribute<TResponse> : ProducesResponseTypeAttribute
    {
        public ProducesResponseAttribute(HttpStatusCode httpStatus) : base(typeof(TResponse), (int)httpStatus) { }
    }
}
