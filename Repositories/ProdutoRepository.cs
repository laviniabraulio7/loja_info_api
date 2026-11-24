// Importa o namespace que contém a classe ConexaoBanco.
using LojaApi.Data;

// Importa o namespace que contém a entidade Produto.
using LojaApi.Entities;

// Importa as classes da biblioteca Npgsql,
// utilizada para acessar o PostgreSQL.
using Npgsql;

// Define que a classe pertence ao namespace
// LojaApi.Repositories.
namespace LojaApi.Repositories;

// Declara o Repository responsável por executar
// comandos SQL relacionados à entidade Produto.
public class ProdutoRepository
{
    // Armazena uma referência para a classe de conexão.
    //
    // private impede o acesso direto por outras classes.
    //
    // readonly impede que o atributo receba outro objeto
    // depois da execução do construtor.
    private readonly ConexaoBanco _conexaoBanco;

    // Construtor da classe ProdutoRepository.
    //
    // O objeto ConexaoBanco será fornecido automaticamente
    // pelo sistema de injeção de dependência do ASP.NET Core.
    public ProdutoRepository(ConexaoBanco conexaoBanco)
    {
        // Armazena no atributo privado o objeto
        // recebido pelo parâmetro do construtor.
        _conexaoBanco = conexaoBanco;
    }

    // Declara um método público e assíncrono
    // responsável por listar os produtos cadastrados.
    //
    // Task<List<Produto>> representa uma operação que,
    // ao terminar, disponibiliza uma lista de produtos.
    public async Task<List<Produto>> ListarTodosAsync()
    {
        // Cria a lista que será devolvida ao Service.
        List<Produto> produtos = new();

        // Solicita uma conexão aberta com o PostgreSQL.
        //
        // await aguarda a conclusão da abertura da conexão.
        //
        // await using garante o descarte da conexão
        // ao sair do escopo, inclusive se ocorrer uma exceção.
        await using NpgsqlConnection conexao =
            await _conexaoBanco.CriarConexaoAsync();

        // As três aspas permitem escrever um texto
        // utilizando várias linhas.
        string sql = """
            SELECT
                id_produto,
                nome,
                preco,
                quantidade_estoque
            FROM produto
            ORDER BY nome;
            """;

        // Cria o comando que será executado no PostgreSQL.
        //
        // O primeiro argumento contém o SQL.
        // O segundo informa a conexão utilizada.
        await using NpgsqlCommand comando =
            new NpgsqlCommand(sql, conexao);

        // Executa o SELECT e obtém um DataReader,
        // que permite ler as linhas retornadas pelo banco.
        await using NpgsqlDataReader leitor =
            await comando.ExecuteReaderAsync();

        // Obtém a posição de cada coluna pelo nome.
        //
        // Assim, a leitura dos valores não depende de números
        // escritos diretamente em cada chamada de GetInt32,
        // GetString ou nos demais métodos de leitura.
        int colunaIdProduto = leitor.GetOrdinal("id_produto");
        int colunaNome = leitor.GetOrdinal("nome");
        int colunaPreco = leitor.GetOrdinal("preco");
        int colunaQuantidadeEstoque = leitor.GetOrdinal("quantidade_estoque");

        // Percorre as linhas retornadas pelo SELECT.
        //
        // ReadAsync avança para a próxima linha.
        // Enquanto existir uma linha, o bloco será executado.
        while (await leitor.ReadAsync())
        {
            // Utiliza o construtor que recebe os dados
            // de um produto já cadastrado no banco.
            Produto produto = new Produto(
                leitor.GetInt32(colunaIdProduto),
                leitor.GetString(colunaNome),
                leitor.GetDecimal(colunaPreco),
                leitor.GetInt32(colunaQuantidadeEstoque)
            );

            // Adiciona o produto à lista de retorno.
            produtos.Add(produto);
        }

        // Devolve os produtos cadastrados.
        return produtos;
    }
}