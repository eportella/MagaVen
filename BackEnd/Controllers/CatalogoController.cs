using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace BackEnd.Controllers
{
    [ApiController]
    [Route("catalogo")]
    public class CatalogoController : ControllerBase
    {
        Catalogo.Service Service { get; }

        public CatalogoController(Catalogo.Service service)
        {
            Service = service;
        }
        [HttpGet]
        public IStatusCodeActionResult Get()
        {
            return Ok(Service.Read());
        }
        [HttpPost]
        public async Task<IStatusCodeActionResult> Post([FromBody] Catalogo.Model model)
        {
            await Service.CreateAsync(model);
            return Ok();
        }
        [HttpPut("{code}")]
        public async Task<IStatusCodeActionResult> Put(string name, Catalogo.Model model)
        {
            await Service.UpdateAsync(name, model);
            return Ok();
        }
        [HttpDelete]
        public async Task<IStatusCodeActionResult> Delete()
        {
            if (await Service.EstaNoCliente())
                ModelState.AddModelError(nameof(Service.EstaNoCliente), "Cliente Existente");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!Service.Read().Any())
                return NoContent();

            await Service.DeleteAsync();
            return Ok();
        }
    }
}