using System;
using System.IO;
using System.Threading.Tasks;
using AutoMapper;
using Leandro.Estudos.CursosOnline.Api.Entidades;
using Leandro.Estudos.CursosOnline.Api.Interfaces;
using Leandro.Estudos.CursosOnline.Api.Interfaces.Servicos;
using Leandro.Estudos.CursosOnline.Api.Models;
using Leandro.Estudos.CursosOnline.Api.Notificacoes;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Leandro.Estudos.CursosOnline.Api.Controllers.V2
{
  [ApiController]
  [ApiVersion("2.0")]
  [Route("api/v{version:apiVersion}/[controller]")]
  public class AlunosController : ControllerBase
  {
    private readonly IAlunoServico _servico;
    private readonly INotificador _notificador;
    private readonly IMapper _mapper;
    private readonly IArquivoServico _arquivoServico;

    public AlunosController(IAlunoServico servico,
                            INotificador notificador,
                            IMapper mapper,
                            IArquivoServico arquivoServico)
    {
      _servico = servico;
      _notificador = notificador;
      _mapper = mapper;
      _arquivoServico = arquivoServico;
    }

    [RequestSizeLimit(40000000)]
    [HttpPost]
    public async Task<ActionResult> Post(AlunoComImagemModel model)
    {
      var prefixo = Guid.NewGuid() + "_";
      model.Imagem = prefixo + model.ImagemUpload.FileName;

      var mensagemErro = "Ocorreram um ou mais erros ao tentar cadastrar o aluno";
      if (!await _arquivoServico.Upload(model.ImagemUpload, prefixo))
        return BadRequest(new BadRequestResponse(mensagemErro, _notificador.ObterNotificacoes(), model));

      var aluno = _mapper.Map<Aluno>(model);
      if (await _servico.Incluir(aluno))
        return Ok(new OkResponse("Aluno cadastrado com sucesso", aluno));

      _arquivoServico.Remover(model.Imagem);
      return BadRequest(new BadRequestResponse(mensagemErro, _notificador.ObterNotificacoes(), model));
    }
  }
}