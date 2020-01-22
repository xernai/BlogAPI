using System;
using System.IO;
using BlogAPI.Utilities;

namespace BlogAPI.Data
{
    public  class Repository<D, T> : IRepository<T> where T : class where D : BlogPostsContext, new()
    {
        private readonly IFileLogger _fileLogger;
        public Repository( IFileLogger fileLogger)
        {
            _fileLogger = fileLogger;
        }

        D entities = new D();

        public  void Create(T entity)
        {
            try
            {

            entities.Set<T>().Add(entity);
            }
            catch(Exception ex)
            {
                _fileLogger.Log(ex.ToString());
            }
        }

        public  void Save()
        {
            try
            {
                entities.SaveChanges();
            }
            catch(Exception ex)
            {
                _fileLogger.Log(ex.ToString());
            }
        }
    }
}