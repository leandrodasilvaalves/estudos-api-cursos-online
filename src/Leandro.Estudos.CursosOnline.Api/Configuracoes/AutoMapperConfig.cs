using AutoMapper;
using Leandro.Estudos.CursosOnline.Api.Entidades;
using Leandro.Estudos.CursosOnline.Api.Models;

namespace Leandro.Estudos.CursosOnline.Api.Configuracoes
{
  public class AutoMapperConfig : Profile
  {
    public AutoMapperConfig()
    {
      CreateMap<AlunoComImagemModel, Aluno>()
        .ForMember(a => a.Imagem, src => src.MapFrom(x => x.Imagem))
        .ReverseMap();

      CreateMap<AlunoSemImagemModel, Aluno>().ReverseMap();
    }
  }
}