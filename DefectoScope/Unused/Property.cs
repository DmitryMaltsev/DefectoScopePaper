namespace DefectoScope
{
    /*public interface IProperty
    {
        string Name { get; set; }
        //Type TypeValue { get; set; }
        object DefValue { get; set; }
        object Value { get; set; }
    }*/

    /// <summary>
    ///     Свойство
    /// </summary>
    /// <typeparam name="T">Тип данных</typeparam>
    public class Property<T>
    {
        /*private Type _typeValue;
        public Type TypeValue
        {
            get
            {
                if (_typeValue != null)
                    throw new ApplicationException($"Property [TypeValue] in class [Property<{typeof(T)}>] is null");

                return _typeValue;
            }
            set => _typeValue = value;
        }*/

        /// <summary>
        ///     Значение свойства по умолчанию
        /// </summary>
        private T _defValue;

        /// <summary>
        ///     Имя свойства
        /// </summary>
        private string _name;

        /// <summary>
        ///     Текущее значение свойства
        /// </summary>
        private T _value;

        /// <summary>
        ///     Создает свойство
        /// </summary>
        /// <param name="defValue">Значение свойства по умолчанию</param>
        /// <param name="name">Имя свойства</param>
        /// <param name="value">Значение свойства</param>
        public Property(T defValue, string name = default, T value = default)
        {
            _defValue = defValue;
            Name = name;
            _value = value;
        }

        /// <summary>
        ///     Возвращает или задает имя свойства
        /// </summary>
        public string Name
        {
            get
            {
                //if (string.IsNullOrEmpty(_name))
                //    throw new ApplicationException("Property [Name] in class [Property] is null or empty");

                return _name;
            }
            set => _name = value;
        }

        /// <summary>
        ///     Возвращает или задает значение свойства по умолчанию
        /// </summary>
        public T DefValue
        {
            get
            {
                //if (_defValue != null)
                //    throw new ApplicationException($"Property [DefValue] in class [Property<{typeof(T)}>] is null");

                return _defValue;
            }
            set => _defValue = value;
        }

        /// <summary>
        ///     Возвращает или задает текущее значение свойства
        /// </summary>
        public T Value
        {
            get
            {
                //if (_value != null)
                //    throw new ApplicationException($"Property [Value] in class [Property<{typeof(T)}>] is null");

                return _value;
            }
            set => _value = value;
        }
    }
}