using AutoMapper;
using TechChallengeFiap.Application.DTOs;
using TechChallengeFiap.Domain.Entities;

namespace TechChallengeFiap.Application.Helpers
{
    public class UsuarioProfile: Profile
    {
        public UsuarioProfile()
        {
            CreateMap<Usuario, CadastrarUsuarioDTO>()
            .ReverseMap();

            CreateMap<Usuario, AlterarUsuarioDTO>()
                .ReverseMap();
        }
        
    }
}
