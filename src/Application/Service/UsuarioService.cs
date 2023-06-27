using Domain.Entity;
using Infrastructure.Data.Repositoty;
using Application.Interfaces;

namespace Application.Service;

public class UsuarioService : UsuarioServiceInterface
{
    private UsuarioRepository UsuarioRepository;
    
    public UsuarioService(UsuarioRepository usuarioRepository) 
    {
        UsuarioRepository = usuarioRepository;
    }

    public IEnumerable<Usuario> usuariosQueMaisCompram(int limit)
    {
        return UsuarioRepository.findUsuariosQueMaisCompram(limit);
    }
}