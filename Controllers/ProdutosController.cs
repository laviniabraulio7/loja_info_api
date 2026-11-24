using LojaApi.Entities;
using LojaApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace LojaApi.Controllers;

[ApiController]
[Route("api/produtos")]
public class ProdutosController : ControllerBase
{
    private readonly ProdutoService _produtoService;

    public ProdutosController(ProdutoService produtoService)
    {
        _produtoService = produtoService;
    }

    [HttpGet]
    public async Task<ActionResult<List<Produto>>> Listar()
    {
        List<Produto> produtos =
            await _produtoService.ListarTodosAsync();

        return Ok(produtos);
    }
}