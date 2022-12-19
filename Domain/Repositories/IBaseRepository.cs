namespace Domain.Repositories;

public interface IBaseRepository<T> 
{
    void Delete(T delEntity);
    void Add(T newEntity);
    List<T> Read();   
}