using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using System;

namespace BackEnd.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProdutoController : ControllerBase
    {
        Produto.Service Service { get; }

        public ProdutoController(Produto.Service service)
        {
            Service = service;
        }

        [HttpGet]
        public IStatusCodeActionResult Get() => Ok(Service.ReadAsync());
        [HttpPost]
        public IStatusCodeActionResult Post([FromBody] Produto.Lote.Model model) => Ok(Service.CreateAsync(model));
        [HttpDelete("{Id}")]
        public IStatusCodeActionResult Delete(Guid id) => Ok(Service.DeleteAsync(id));
    }
}