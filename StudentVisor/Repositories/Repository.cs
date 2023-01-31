using StudentVisor.Models;

namespace StudentVisor.Repositories;

/// <summary>
/// Repository class that implements IRepository interface
/// </summary>
/// <typeparam name="T"></typeparam>
public class Repository<T> : IRepository<T> where T : class, IEntity
{
    protected IDataFileHandler<T> FileHandler { get; }

    public Repository(IDataFileHandler<T> fileHandler)
    {
        FileHandler = fileHandler;
    }

    /// <inheritdoc />
    public List<T> GetAll()
    {
        var list = FileHandler.ReadAll();
        return list;
    }

    /// <inheritdoc />
    public T GetById(string index)
    {
        var entity = FileHandler.ReadLine(index);
        return entity;
    }

    /// <inheritdoc />
    public bool TryAdd(T entity)
    {
        if (FileHandler.Exists(entity.Id))
        {
            return false;
        }
        FileHandler.Append(entity);
        return true;
    }

    /// <inheritdoc />
    public T Update(T entity)
    {
        if (entity == null)
        {
            throw new ArgumentException($"Entity <{nameof(T)}>[{entity}] does not exist");
        }
        if (!FileHandler.Exists(entity.Id))
        {
            throw new ArgumentException($"Entity <{nameof(IEntity)}>[{entity}] does not exist");
        }

        var data = FileHandler.ReadAll();
        data[data.IndexOf(entity)] = entity;
        FileHandler.Write(data);
        return FileHandler.ReadLine(entity.Id);
    }

    /// <inheritdoc />
    public bool TryDelete(T entity)
    {
        return TryDelete(entity.Id);
    }

    /// <inheritdoc />
    public bool TryDelete(string index)
    {
        if (!FileHandler.Exists(index))
        {
            return false;
        }
        FileHandler.RemoveLine(index);
        return true;
    }
}