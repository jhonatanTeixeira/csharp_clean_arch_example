using System.Net.Mime;
using Domain.Entity;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace RestApi.Controllers;

[ApiController]
[Route("[controller]")]
public class UsuarioController : ControllerBase
{

    private readonly ILogger<UsuarioController> _logger;
    private readonly UsuarioServiceInterface UsuarioService;

    public UsuarioController(ILogger<UsuarioController> logger, UsuarioServiceInterface usuarioService)
    {
        _logger = logger;
        UsuarioService = usuarioService;
    }

    [HttpGet(Name = "usuarios_mais_compram")]
    public IEnumerable<Usuario> Get()
    {
        return UsuarioService.usuariosQueMaisCompram(10);
    }
}
