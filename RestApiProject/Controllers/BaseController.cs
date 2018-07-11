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
    public class BaseController<T> : ApiController
        where T: BaseDTO
    {
        protected internal IBaseService<T> _service;

        public BaseController(IBaseService<T> service)
        {
            _service = service;
        }

        public IEnumerable<T> Get()
        {
            return _service.GetAll();
        }


        public T Get(Guid id)
        {
            return _service.GetById(id);
        }


        public void Post([FromBody]T model)
        {
            _service.Create(model);
        }


        public void Put(Guid id, [FromBody]T model)
        {
            _service.Update(model);
        }


        public void Delete(Guid id)
        {
            _service.Delete(id);
        }
    }
}
