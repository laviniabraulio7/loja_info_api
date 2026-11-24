namespace LojaApi.Entities;

/* 
    Representa um produto cadastrado na aplicação.

    A entidade Produto concentra os dados principais
    do item comercializado e protege o estado do objeto
    por meio de propriedades com validação.

    Essa classe não executa comandos SQL
    e não acessa diretamente o banco de dados.
*/
public class Produto
{
/* 
    Atributo privado responsável por armazenar
    o identificador do produto.

    O acesso acontece por meio da propriedade IdProduto.
*/
    private int _idProduto;

/* 
    Armazena o nome do produto.

    A string é inicializada com ""
    para evitar que o atributo
    comece com valor nulo.
*/
    private string _nome = "";

/* 
    Armazena o preço unitário do produto.
*/
    private decimal _preco;

/* 
    Armazena a quantidade disponível
    do produto em estoque.
*/
    private int _quantidadeEstoque;

/* 
    Construtor utilizado para criar
    um novo produto dentro da aplicação.

    Nesse momento, o identificador ainda
    não é informado porque será gerado
    automaticamente pelo PostgreSQL.
*/
    public Produto(
        string nome,
        decimal preco,
        int quantidadeEstoque)
    {
        this.Nome = nome;
        this.Preco = preco;
        this.QuantidadeEstoque = quantidadeEstoque;
    }

/* 
    Construtor utilizado para criar
    um objeto Produto com dados
    que já existem no banco.

    Esse construtor é usado principalmente
    pelo Repository durante a leitura
    dos registros do PostgreSQL.

    Nesse caso, todos os dados já existem:
- identificador;
- nome;
- preço;
- quantidade em estoque.
*/
    public Produto(
        int idProduto,
        string nome,
        decimal preco,
        int quantidadeEstoque)
    {
        this.IdProduto = idProduto;
        this.Nome = nome;
        this.Preco = preco;
        this.QuantidadeEstoque = quantidadeEstoque;
    }

/* 
    Propriedade responsável pelo identificador
    do produto.

    O get permite consultar o valor.

    O private set impede que outras classes
    alterem o identificador diretamente.

    A própria entidade controla
    quando esse valor pode ser definido.
*/
    public int IdProduto
    {
        get
        {
            return _idProduto;
        }

        private set
        {
/* 
    Impede que o objeto receba
    um identificador negativo.

    O valor 0 é permitido porque pode representar
    um produto criado em memória
    que ainda não foi persistido no banco.

    Valores negativos não são considerados válidos
    para o identificador do produto.
*/
            if (value < 0)
            {
                throw new ArgumentException(
                    "O identificador do produto não pode ser negativo."
                );
            }

            _idProduto = value;
        }
    }

/* 
    Propriedade responsável pelo nome
    do produto.

    O private set impede alterações diretas
    por outras classes.
*/
    public string Nome
    {
        get
        {
            return _nome;
        }

        private set
        {
/* 
    Impede que o produto possua
    um nome vazio, nulo
    ou formado apenas por espaços.

    IsNullOrWhiteSpace verifica
    exatamente essas situações.
*/
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException(
                    "O nome do produto não pode ficar vazio."
                );
            }

/* 
    Trim remove espaços em branco
    do início e do final do texto
    antes de armazená-lo.
*/
            _nome = value.Trim();
        }
    }

/* 
    Propriedade responsável por armazenar
    o preço do produto.

    O valor somente pode ser alterado
    internamente pela própria entidade.
*/
    public decimal Preco
    {
        get
        {
            return _preco;
        }

        private set
        {
/* 
    Impede que um produto possua
    preço igual ou menor que zero.

    Essa validação protege o estado
    do objeto Produto.
*/
            if (value <= 0)
            {
                throw new ArgumentException(
                    "O preço deve ser maior que zero."
                );
            }

            _preco = value;
        }
    }

/* 
    Propriedade responsável por armazenar
    a quantidade em estoque do produto.

    O private set impede que outras classes
    alterem diretamente esse valor.
*/
    public int QuantidadeEstoque
    {
        get
        {
            return _quantidadeEstoque;
        }

        private set
        {
/* 
    Impede que o produto possua
    estoque negativo.

    O valor 0 é permitido para representar
    um produto esgotado.
*/
            if (value < 0)
            {
                throw new ArgumentException(
                    "A quantidade em estoque não pode ser negativa."
                );
            }

            _quantidadeEstoque = value;
        }
    }
}