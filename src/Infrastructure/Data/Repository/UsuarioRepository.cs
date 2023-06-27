
using System.Collections.Generic;
using Domain.Entity;
using Domain.Repositoty;
using Infrastructure.Data.Context;

namespace Infrastructure.Data.Repositoty;

public class UsuarioRepository : AbstractRepository<Usuario>, UsuarioRepositoryInterface
{
    public UsuarioRepository(CleanContext context) : base(context)
    {
    }

    public IEnumerable<Usuario> findUsuariosQueMaisCompram(int limit)
    {
        return GetAll();
    }
}