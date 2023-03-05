namespace Backend.TechChallenge.Domain.Repositories.Contracts;

public interface IRepository<TEntity>
    where TEntity : class
{
    Task InsertAsync(TEntity entity);
    Task<List<TEntity>> GetAllAsync();
}