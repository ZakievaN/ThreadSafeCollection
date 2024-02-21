using System.Collections;


namespace ThreadSafeCollection
{
    /// <summary>
    /// Потокобезопасная коллекция
    /// </summary>
    /// <typeparam name="TId">Тип для идентификатора в составном ключе коллекции</typeparam>
    /// <typeparam name="TName">Тип для имени в составном ключе коллекции</typeparam>
    /// <typeparam name="TValue">Тип значения коллекции</typeparam>
    public class Collection<TId, TName, TValue> : IEnumerable
        where TId : notnull, IComparable
        where TName : notnull, IComparable
    {
        private object _locker = new();

        private SortedSet<CollectionItem<TId, TName, TValue>> _collection;

        public int Count => _collection.Count;

        public Collection()
        {
            _collection = new SortedSet<CollectionItem<TId, TName, TValue>>(
                Comparer<CollectionItem<TId, TName, TValue>>
                    .Create((a, b) => a.CompareTo(b))
                );
        }

        /// <summary>
        /// Метод, возврающий перечислитель коллекции
        /// </summary>
        /// <returns>Перечислитель коллекции</returns>
        public IEnumerator GetEnumerator()
        {
            lock (_locker)
            {
                return _collection.GetEnumerator();
            }
        }

        /// <summary>
        /// Добавление в коллекцию значения по ключу
        /// </summary>
        /// <param name="id">Идентификатор в составном ключе</param>
        /// <param name="name">Имя в составном ключе</param>
        /// <param name="value">Значение</param>
        /// <returns>True - если элемент добавлен в коллекцию, иначе False</returns>
        public bool Add(TId id, TName name, TValue value)
        {
            lock (_locker)
            {
                var item = new CollectionItem<TId, TName, TValue>(id, name, value);
                return _collection.Add(item);
            }
        }

        #region Методы Remove 

        public void Clear() 
        {
            _collection.Clear();
        }

        /// <summary>
        /// Удаление элемента по составному ключу из коллекции
        /// </summary>
        /// <param name="id">Идентификатор в составном ключе</param>
        /// <param name="name">Имя в составном ключе</param>
        /// <returns>Число элементов, удаленных из коллекции</returns>
        public int Remove(TId id, TName name)
        {
            lock (_locker)
            {
                return _collection.RemoveWhere(x => x.Id.Equals(id) && x.Name.Equals(name));
            }
        }

        /// <summary>
        /// Удаление элемента по идентификатору ключа из коллекции
        /// </summary>
        /// <param name="id">Идентификатор в составном ключе</param>
        /// <returns>Число элементов, удаленных из коллекции</returns>
        public int RemoveById(TId id)
        {
            lock (_locker)
            {
                return _collection.RemoveWhere(x => x.Id.Equals(id));
            }
        }

        /// <summary>
        /// Удаление элемента по имени ключа из коллекции
        /// </summary>
        /// <param name="id">Имя в составном ключе</param>
        /// <returns>Число элементов, удаленных из коллекции</returns>
        public int RemoveByName(TName name)
        {
            lock (_locker)
            {
                return _collection.RemoveWhere(x => x.Name.Equals(name));
            }
        }

        #endregion

        #region Методы Get

        /// <summary>
        /// Индексатор для составного ключа
        /// </summary>
        /// <param name="key">Составной ключ</param>
        /// <returns>Найденное значение или значение по умолчанию для возвращаемого типа</returns>
        public TValue this[TId id, TName name]
        {
            get 
            {
                if (_collection.TryGetValue(new CollectionItem<TId, TName, TValue>(id, name, default), out var result))
                {
                    return result.Value;
                }

                return default;
            }
            set
            {
                Add(id, name, value);
            }
        }

        /// <summary>
        /// Метод, возврщающий коллекцию значений по идентификатору составного ключа
        /// </summary>
        /// <param name="id">Идентификатор в составном ключе</param>
        /// <returns>Найденное значение или значение по умолчанию для возвращаемого типа</returns>
        public IEnumerable<TValue> GetByKeyId(TId id)
        {
            var flag = false;
            foreach (var item in _collection)
            {
                if (item.Id.Equals(id))
                {
                    flag = true;
                    yield return item.Value;
                }
                else
                {
                    if (flag)
                    {
                        yield break;
                    }
                }
            }
        }

        /// <summary>
        /// Метод, возврщающий коллекцию значений по имени составного ключа
        /// </summary>
        /// <param name="name">Имя в составном ключе</param>
        /// <returns>Найденное значение или значение по умолчанию для возвращаемого типа</returns>
        public IEnumerable<TValue> GetByKeyName(TName name)
        {
            foreach (var item in _collection)
            {
                if (item.Name.Equals(name))
                {
                    yield return item.Value;
                }
            }
        }

        #endregion
    }
}
