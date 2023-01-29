using StudentVisor.Models;

namespace StudentVisor;

/// <summary>
///  Interface for handling data files.
/// The generic type T must implement the IEntity interface.
/// </summary>
/// <typeparam name="T">Extension of IEntity class</typeparam>
public interface IDataFileHandler<T> where T : class, IEntity
{
    /// <summary>
    /// Gets a list of entities and writes them to a data file.
    /// </summary>
    /// <param name="entities"></param>
    public void Write(List<T> entities);

    /// <summary>
    /// Takes a string as an argument and writes it to the data file.
    /// </summary>
    /// <param name="content"></param>
    public void Write(string content);

    /// <summary>
    /// Reads all the data from the data file and returns them as a list of entities.
    /// </summary>
    /// <returns></returns>
    public List<T> ReadAll();

    /// <summary>
    ///  Takes an id as an argument and returns a single entity with the specified id from the data file.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public T ReadLine(string id);

    /// <summary>
    /// Appends entity to the end of the data file.
    /// </summary>
    /// <param name="entity">Entity to append</param>
    public void Append(T entity);

    /// <summary>
    /// Returns a boolean indicating whether an entity with the specified id exists in the data file.
    /// </summary>
    /// <param name="id"></param>
    /// <returns>true if entity exists, false otherwise</returns>
    public bool Exists(string id);

    /// <summary>
    /// Removes the entity with the specified id from the data file.
    /// </summary>
    /// <param name="id">Id of entity to be removed</param>
    void RemoveLine(string id);
}