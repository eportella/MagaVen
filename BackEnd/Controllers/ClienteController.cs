using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using System;

namespace BackEnd.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ClienteController : ControllerBase
    {
        Cliente.Service Service { get; }

        public ClienteController(Cliente.Service service)
        {
            Service = service;
        }
        [HttpGet]
        public IStatusCodeActionResult Get() => Ok(Service.ReadAsync());
        [HttpPost]
        public IStatusCodeActionResult Post([FromBody] Cliente.Model model) => Ok(Service.CreateAsync(model));
        [HttpPut("{nome}")]
        public IStatusCodeActionResult Put(string nome, Cliente.Model model) => Ok(Service.UpdateAsync(nome, model));
        [HttpDelete]
        public IStatusCodeActionResult Delete(Guid code) => Ok(Service.DeleteAsync());
    }
}