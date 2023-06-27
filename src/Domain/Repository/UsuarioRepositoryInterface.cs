using System.Collections.Generic;
using Domain.Entity;

namespace Domain.Repositoty;

public interface UsuarioRepositoryInterface : RepositoryInterface<Usuario>
{
    IEnumerable<Usuario> findUsuariosQueMaisCompram(int limit);
}