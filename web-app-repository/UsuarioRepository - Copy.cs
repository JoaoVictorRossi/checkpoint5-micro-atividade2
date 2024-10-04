using MySqlConnector;
using web_app_domain;
using Dapper;
using web_app_performance.Model;

namespace web_app_repository
{
    public class ProdutoRepository : IProdutoRepository
    {
        private readonly MySqlConnection mySqlConnection;
        public ProdutoRepository()
        {
            string connectionString = "Server=localhost;Database=sys;User=root;Password=123;";
            mySqlConnection = new MySqlConnection(connectionString);
        }

        public async Task<IEnumerable<Produto>> ListarProdutos()
        {
            await mySqlConnection.OpenAsync();
            string query = "select Id, Nome, Email from produtos;";
            var produtos = await mySqlConnection.QueryAsync<Produto>(query);
            await mySqlConnection.CloseAsync();

            return produtos;
        }

        public async Task SalvarProduto(Produto produto)
        {
            await mySqlConnection.OpenAsync();
            string sql = @"insert into produtos(nome, email) 
                            values(@nome, @email);";
            await mySqlConnection.ExecuteAsync(sql, produto);
            await mySqlConnection.CloseAsync();
        }

        public async Task AtualizarProduto(Produto produto)
        {
            await mySqlConnection.OpenAsync();
            string sql = @"update produtos 
                            set Nome = @nome, 
	                            Email = @email
                            where Id = @id;";

            await mySqlConnection.ExecuteAsync(sql, produto);
            await mySqlConnection.CloseAsync();
        }

        public async Task RemoverProduto(int id)
        {
            await mySqlConnection.OpenAsync();
            string sql = @"delete from produtos  where Id = @id;";
            await mySqlConnection.ExecuteAsync(sql, new { id });
            await mySqlConnection.CloseAsync();
        }
    }
}
