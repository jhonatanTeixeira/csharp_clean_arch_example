using System.Collections.Generic;
using Domain.Entity;

namespace Application.Interfaces;

public interface UsuarioServiceInterface {
    IEnumerable<Usuario> usuariosQueMaisCompram(int limit);
}