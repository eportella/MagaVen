using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace BackEnd.Controllers
{
    [ApiController]
    [Route("produto-disponivel")]
    public class ProdutoDisponivelController : ControllerBase
    {
        Armazem.Service Service { get; }

        public ProdutoDisponivelController(Armazem.Service service)
        {
            Service = service;
        }

        [HttpGet]
        public IStatusCodeActionResult Get() => Ok(Service.ReadAsync());
    }
}