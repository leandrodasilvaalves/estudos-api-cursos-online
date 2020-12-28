using Microsoft.AspNetCore.Mvc;

namespace Leandro.Estudos.CursosOnline.Api.Controllers.V2
{
  [ApiController]
  [ApiVersion("2.0")]
  [Route("api/v{version:apiVersion}/[controller]")]
  public class TesteController : ControllerBase
  {
    [HttpGet]
    public string Get()
    {
      return "Sou endpoint de teste v2";
    }
  }
}