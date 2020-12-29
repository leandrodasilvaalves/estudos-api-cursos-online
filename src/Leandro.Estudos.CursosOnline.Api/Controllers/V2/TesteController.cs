using KissLog;
using Microsoft.AspNetCore.Mvc;

namespace Leandro.Estudos.CursosOnline.Api.Controllers.V2
{
  [ApiController]
  [ApiVersion("2.0")]
  [Route("api/v{version:apiVersion}/[controller]")]
  public class TesteController : ControllerBase
  {
    private readonly ILogger _logger;

    public TesteController(ILogger logger)
    {
      _logger = logger;
    }

    [HttpGet]
    public string Get()
    {
      _logger.Debug("Sou um log de debug");
      _logger.Info("Sou um log de info");
      _logger.Warn("Sou um log de warning");
      _logger.Error("Sou um log de erro");
      _logger.Critical("Sou um log de critical");
      return "Sou endpoint de teste v2";
    }
  }
}