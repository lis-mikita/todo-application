using System.Collections.Generic;

namespace todo_domain_entities.Interfaces
{
    /// <summary>
    /// Provide mechanism for read/create/update/delete objects of T type.
    /// </summary>
    /// <typeparam name="T">Type of object.</typeparam>
    public interface IRepository<T>
        where T : class
    {
        /// <summary>
        /// Return all available objects.
        /// </summary>
        /// <returns>Set of object.</returns>
        IEnumerable<T> ReadAll();

        /// <summary>
        /// Return object.
        /// </summary>
        /// <param name="id">Object number.</param>
        /// <returns>Object or null.</returns>
        T Read(int id);

        /// <summary>
        /// Create object in set.
        /// </summary>
        /// <param name="item">Object for add in set.</param>
        void Create(T item);

        /// <summary>
        /// Update object in set.
        /// </summary>
        /// <param name="item">Object for update in set.</param>
        void Update(T item);

        /// <summary>
        /// Delete object in set.
        /// </summary>
        /// <param name="id">Object number.</param>
        void Delete(int id);
    }
}
