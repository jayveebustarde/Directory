using DTO;
using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace RestApiProject.Controllers
{
    public class DiscountController : BaseController<DiscountDTO>
    {
        public DiscountController(IBaseService<DiscountDTO> service) : base(service)
        {

        }
    }
}
