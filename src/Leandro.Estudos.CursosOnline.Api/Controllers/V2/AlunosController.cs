using System;
using System.IO;
using System.Threading.Tasks;
using AutoMapper;
using Leandro.Estudos.CursosOnline.Api.Entidades;
using Leandro.Estudos.CursosOnline.Api.Interfaces;
using Leandro.Estudos.CursosOnline.Api.Interfaces.Repositorios;
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
    private readonly IAlunoRepositorio _repositorio;
    private const string _mensagemErro = "Ocorreram um ou mais erros ao tentar cadastrar o aluno";

    public AlunosController(IAlunoServico servico,
                            INotificador notificador,
                            IMapper mapper,
                            IArquivoServico arquivoServico,
                            IAlunoRepositorio repositorio)
    {
      _servico = servico;
      _notificador = notificador;
      _mapper = mapper;
      _arquivoServico = arquivoServico;
      _repositorio = repositorio;
    }

    [RequestSizeLimit(40000000)]
    [HttpPost]
    public async Task<ActionResult> Post(AlunoComImagemModel model)
    {
      var prefixo = Guid.NewGuid() + "_";
      model.Imagem = prefixo + model.ImagemUpload.FileName;

      if (!await _arquivoServico.Upload(model.ImagemUpload, prefixo))
        return BadRequest(new BadRequestResponse(_mensagemErro, _notificador.ObterNotificacoes(), model));

      var aluno = _mapper.Map<Aluno>(model);
      if (await _servico.Incluir(aluno))
        return Ok(new OkResponse("Aluno cadastrado com sucesso", aluno));

      _arquivoServico.Remover(model.Imagem);
      return BadRequest(new BadRequestResponse(_mensagemErro, _notificador.ObterNotificacoes(), model));
    }

    [RequestSizeLimit(40000000)]
    [HttpPut("{id:guid}")]
    public async Task<ActionResult> Put(Guid id, AlunoComImagemModel model)
    {
      if (id != model.Id)
        return BadRequest(new BadRequestResponse("O id da rota precisa ser igual ao id do aluno"));

      var alunoBanco = await _repositorio.ObterPorId(id);
      if (alunoBanco == null)
        return NotFound(new NotFoundResponse("Aluno não localizado na base de dados"));

      string prefixo;
      if (model.ImagemUpload != null)
      {
        prefixo = Guid.NewGuid() + "_";
        model.Imagem = prefixo + model.ImagemUpload.FileName;
        if (!await _arquivoServico.Upload(model.ImagemUpload, prefixo))
          return BadRequest(new BadRequestResponse(_mensagemErro, _notificador.ObterNotificacoes(), model));
      }

      var imagemAntiga = alunoBanco.Imagem;
      var aluno = _mapper.Map<Aluno>(model);
      alunoBanco.MergearDados(aluno);
      if (await _servico.Editar(alunoBanco))
      {
        if (model.ImagemUpload != null) _arquivoServico.Remover(imagemAntiga);
        return Ok(new OkResponse("Aluno atualizado com sucesso", model));
      }

      if (model.ImagemUpload != null) _arquivoServico.Remover(model.Imagem);
      return BadRequest(new BadRequestResponse(_mensagemErro, _notificador.ObterNotificacoes(), model));
    }
  }
}