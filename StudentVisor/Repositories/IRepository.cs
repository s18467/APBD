using StudentVisor.Models;

namespace StudentVisor.Repositories
{
    /// <summary>
    /// Interface for repository pattern
    /// </summary>
    /// <typeparam name="T">Type of the Entity in the repository</typeparam>
    public interface IRepository<T> where T : IEntity
    {
        /// <summary>
        /// Retrieve an entity by its index
        /// </summary>
        /// <param name="index">Id of the entity</param>
        /// <returns>The entity with the matching index</returns>
        T? GetById(string index);

        /// <summary>
        /// Retrieve all entities from the repository
        /// </summary>
        /// <returns>List of the entities</returns>
        List<T> GetAll();

        /// <summary>
        /// Create a new entity in the repository
        /// </summary>
        /// <param name="entity">The entity to be created</param>
        /// <returns>true jeżeli dodano encję pomyślnie, inaczej false</returns>
        bool TryAdd(T entity);

        /// <summary>
        /// Update an existing entity in the repository
        /// </summary>
        /// <param name="entity">The entity to be updated</param>
        /// <returns>Zapisany obiekt</returns>
        T Update(T entity);

        /// <summary>
        /// TryDelete an existing entity from the repository by specified object
        /// </summary>
        /// <param name="entity">The entity to be deleted</param>
        bool TryDelete(T entity);
        /// <summary>
        /// TryDelete an existing entity from the repository by index
        /// </summary>
        /// <param name="index"></param>
        bool TryDelete(string index);
    }
}