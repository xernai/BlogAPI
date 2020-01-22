namespace BlogAPI.Data
{
    public interface IRepository<T> where T : class
    {
        void Create(T entity);
        void Save();
    }
}