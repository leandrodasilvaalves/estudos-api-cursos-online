using AutoMapper;
using Leandro.Estudos.CursosOnline.Api.Entidades;
using Leandro.Estudos.CursosOnline.Api.Models;
using Microsoft.AspNetCore.Http;

namespace Leandro.Estudos.CursosOnline.Api.Configuracoes
{
  public class AutoMapperConfig : Profile
  {
    public AutoMapperConfig(IHttpContextAccessor context)
    {
      var request = context.HttpContext.Request;
      var _urlBase = $"{request.Scheme}://{request.Host}";

      CreateMap<AlunoComImagemModel, Aluno>()
        .ForMember(a => a.Imagem, src => src.MapFrom(x => x.Imagem))
        .ReverseMap();

      CreateMap<AlunoSemImagemModel, Aluno>().ReverseMap();

      CreateMap<Aluno, Aluno>()
        .ForMember(a => a.Imagem, src =>
          src.MapFrom(x =>
           x.Imagem == null ? x.Imagem : $"{_urlBase}/assets/images/{x.Imagem}"));
    }
  }
}