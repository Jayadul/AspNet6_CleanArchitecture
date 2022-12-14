using Dapper;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;

public class ProductRepository : IProductRepository
{
    private readonly IConfiguration configuration;
    public ProductRepository(IConfiguration configuration)
    {
        this.configuration = configuration;
    }
    public async Task<int> AddAsync(Product entity)
    {
        entity.AddedOn = DateTime.Now;
        var sql = "Insert into Products (Name,Category,Date,Condition,Price,Comment,AddedOn) VALUES (@Name,@Category,@Date,@Condition,@Price,@Comment,@AddedOn)";
        using (var connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection")))
        {
            connection.Open();
            var result = await connection.ExecuteAsync(sql, entity);
            return result;
        }
    }
    public async Task<int> DeleteAsync(int id)
    {
        var sql = "DELETE FROM Products WHERE Id = @Id";
        using (var connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection")))
        {
            connection.Open();
            var result = await connection.ExecuteAsync(sql, new { Id = id });
            return result;
        }
    }
    public async Task<IReadOnlyList<Product>> GetAllAsync()
    {
        var sql = "SELECT * FROM Products";
        using (var connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection")))
        {
            connection.Open();
            var result = await connection.QueryAsync<Product>(sql);
            return result.ToList();
        }
    }
    public async Task<Product> GetByIdAsync(int id)
    {
        var sql = "SELECT * FROM Products WHERE Id = @Id";
        using (var connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection")))
        {
            connection.Open();
            var result = await connection.QuerySingleOrDefaultAsync<Product>(sql, new { Id = id });
            return result;
        }
    }
    public async Task<int> UpdateAsync(Product entity)
    {
        entity.ModifiedOn = DateTime.Now;
        var sql = "UPDATE Products SET Name = @Name, Category = @Category, Date = @Date, Condition = @Condition,Price=@Price,Comment=@Comment, ModifiedOn = @ModifiedOn  WHERE Id = @Id";
        using (var connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection")))
        {
            connection.Open();
            var result = await connection.ExecuteAsync(sql, entity);
            return result;
        }
    }
}