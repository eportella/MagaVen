using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using System.Threading.Tasks;

namespace BackEnd.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ArmazemController : ControllerBase
    {
        Armazem.Service Service { get; }

        public ArmazemController(Armazem.Service service)
        {
            Service = service;
        }

        [HttpGet]
        public IStatusCodeActionResult Get() => Ok(Service.ReadAsync());

        [HttpPost]
        public IStatusCodeActionResult Post([FromBody] Armazem.Model model) => Ok(Service.CreateAsync(model));

        [HttpPut("{nome}")]
        public IStatusCodeActionResult Put(string nome, Armazem.Model model) => Ok(Service.UpdateAsync(nome, model));

        [HttpDelete("{nome}")]
        public async Task<IStatusCodeActionResult> Delete(string nome)
        {
            if (await Service.TemQualquerProduto(nome))
                ModelState.AddModelError(nameof(Service.TemQualquerProduto), "Armazem Contém produtos");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(Service.DeleteAsync(nome));
        }
    }
}