using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automat.DAL.Interfaces
{
    /// <summary>
    /// Интерфейс базового репозитория для операций с сущностями.
    /// </summary>
    /// <typeparam name="T">Тип сущности, с которой работает репозиторий.</typeparam>
    public interface IBaseRepository<T>
    {
        /// <summary>
        /// Создает новую сущность в репозитории.
        /// </summary>
        /// <param name="entity">Сущность для создания.</param>
        /// <returns>True, если операция создания успешно выполнена, иначе - false.</returns>
        Task<bool> Create(T entity);

        /// <summary>
        /// Получает сущность по её идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор сущности.</param>
        /// <returns>Сущность, если найдена, иначе - null.</returns>
        Task<T> Get(int id);

        /// <summary>
        /// Возвращает список всех сущностей в репозитории.
        /// </summary>
        /// <returns>Список сущностей.</returns>
        Task<List<T>> Select();

        /// <summary>
        /// Удаляет сущность из репозитория.
        /// </summary>
        /// <param name="entity">Сущность для удаления.</param>
        /// <returns>True, если операция удаления успешно выполнена, иначе - false.</returns>
        Task<bool> Delete(T entity);

        /// <summary>
        /// Обновляет существующую сущность в репозитории.
        /// </summary>
        /// <param name="entity">Сущность для обновления.</param>
        /// <returns>Обновленная сущность, если операция обновления успешно выполнена, иначе - null.</returns>
        Task<T> Update(T entity);
    }

}
