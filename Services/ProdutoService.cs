// Importa o namespace que contém a entidade Produto.
//
// A entidade Produto será utilizada como tipo dos objetos
// que serão devolvidos pelo Service.
using LojaApi.Entities;

// Importa o namespace que contém o ProdutoRepository.
//
// O Service utilizará o Repository para acessar
// os dados armazenados no PostgreSQL.
using LojaApi.Repositories;

// Define que esta classe pertence ao namespace
// LojaApi.Services.
//
// Esse namespace organiza as classes responsáveis
// pelos casos de uso e pelas regras de negócio.
namespace LojaApi.Services;

// Declara a classe pública ProdutoService.
//
// O Service fica entre o Controller e o Repository.
public class ProdutoService
{
    // Declara um atributo privado que armazenará
    // uma referência para o ProdutoRepository.
    //
    // private impede que o atributo seja acessado
    // diretamente por outras classes.
    //
    // readonly impede que o atributo receba outro objeto
    // depois da execução do construtor.
    private readonly ProdutoRepository _produtoRepository;

    // Construtor da classe ProdutoService.
    //
    // O ASP.NET Core fornecerá automaticamente
    // um objeto ProdutoRepository por meio
    // da injeção de dependência.
    public ProdutoService(ProdutoRepository produtoRepository)
    {
        // Armazena no atributo privado o Repository
        // recebido pelo parâmetro do construtor.
        _produtoRepository = produtoRepository;
    }

    // Declara um método público e assíncrono
    // responsável pelo caso de uso de listar
    // os produtos cadastrados.
    //
    // Task indica que o método executará
    // uma operação assíncrona.
    //
    // List<Produto> indica que, quando terminar,
    // o método devolverá uma lista de objetos Produto.
    public async Task<List<Produto>> ListarTodosAsync()
    {
        // Chama o método ListarTodosAsync()
        // existente no ProdutoRepository.
        //
        // O Repository acessará o PostgreSQL,
        // executará o SELECT e montará a lista.
        //
        // await aguarda o término da consulta
        // sem bloquear a aplicação.
        //
        // return devolve a lista recebida
        // para a classe que chamou o Service.
        return await _produtoRepository.ListarTodosAsync();
    }
}