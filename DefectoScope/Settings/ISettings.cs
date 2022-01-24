#region

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Xml.Linq;
using Kogerent;

#endregion

namespace DefectoScope
{
    /// <summary>
    ///     Настройки XML
    /// </summary>
    public interface ISettings
    {
        /// <summary>
        ///     Свойства по умолчанию. Ключ - имя свойства, значение - значение по умолчанию для данного свойства
        /// </summary>
        ConcurrentDictionary<string, object> Default { get; }

        /// <summary>
        ///     Допустимые границы числовых свойств. Ключ - имя свойства, значение - массив из двух чисел (минимума и максимума)
        /// </summary>
        ConcurrentDictionary<string, object[]> Limit { get; }

        /// <summary>
        ///     Список публичных нестатических свойств (параметров настроек)
        /// </summary>
        PropertyInfo[] PropertiesInfo { get; }

        /// <summary>
        ///     Идентификатор конфигурации настроек
        /// </summary>
        string Id { get; set; }

        /// <summary>
        ///     Ответ на вопрос "Настройки чего?" (в родительном падеже)
        /// </summary>
        string TypeSettingsMessage { get; }

        /// <summary>
        ///     Имя корневого элемента в документе настроек
        /// </summary>
        string RootName { get; }

        /// <summary>
        ///     Имя главного элемента конфигурации настроек
        /// </summary>
        string MainElementName { get; }

        /// <summary>
        ///     Имя атрибута конфигурации настроек
        /// </summary>
        string AttributeName { get; }

        /// <summary>
        ///     Путь до документа настроек
        /// </summary>
        string PathToDoc { get; }

        /// <summary>
        ///     Все параметры в порядке? (логическая проверка)
        /// </summary>
        /// <returns></returns>
        bool AllIsOk();
    }

    public static class SettingExtensions
    {
        /// <summary>
        ///     Возвращает тег пункта массива строк
        /// </summary>
        /// <param name="i">Индекс строки</param>
        /// <param name="name">Имя</param>
        /// <returns></returns>
        private static string GetTagStringsItem(int i, string name = null) => $"{name ?? "item"}_{i + 1}";

        /// <summary>
        ///     Загрузка настроек из файла xml
        /// </summary>
        /// <param name="s">Настройки</param>
        /// <param name="id">Идентификатор конфигурации. Null = <see cref="Constants.DefIdSettings" /></param>
        /// <returns>Успех?</returns>
        public static bool LoadSettingsFromFile(this ISettings s, string id = null)
        {
            //Меняем текущий идентификатор конфигурации
            s.Id = id ?? Constants.DefIdSettings;

            //Загружаем документ из файла
            var doc = UtilsSettings.TryLoadXDoc(s.PathToDoc);

            //Если файл пуст или корень, то создаем новый корень
            if (doc?.Root == null) doc = new XDocument(new XElement(s.RootName));

            //Невозможная ситуация, но ReSharper чтобы не ругался)
            if (doc.Root == null) throw new NullReferenceException();

            //Ищем главный элемент конфигурации с указанным идентификатором
            var main = doc.Root.Elements(s.MainElementName)
                .FirstOrDefault(element => element.Attribute(s.AttributeName)?.Value == s.Id);

            //Если элемент не был найден, то создаем его в корне
            if (main == null)
            {
                G.Logger.Info($"{nameof(ISettings)}: В файле настроек {s.TypeSettingsMessage} была создана конфигурация {s.Id}.");

                main = new XElement(s.MainElementName, new XAttribute(s.AttributeName, s.Id));
                doc.Root?.Add(main);
            }

            //Массивы строк (сложные параметры-свойства)
            var lists = new List<PropertyInfo>();

            //Перебираем параметры-свойства
            foreach (var info in s.PropertiesInfo)
            {
                //Отбрасываем свойства, которые не имеют геттеров или сеттеров
                if (!info.CanRead || !info.CanWrite) continue;

                var propertyType = info.PropertyType;
                var code = Type.GetTypeCode(propertyType);

                //Отбираем массивы строк на потом
                if (propertyType == typeof(string[]))
                {
                    lists.Add(info);
                    continue;
                }

                //Отбрасываем сложные объекты
                if (code == TypeCode.Object) continue;

                var name = info.Name;

                //Если для свойтва нет значения по умолчанию, то свойство отбрасываем
                if (!s.Default.TryGetValue(name, out var defValue)) continue;

                if (!UtilsSettings.ReadValueElement(main, s.PathToDoc, out var value, propertyType, name, defValue))
                {
                    G.Logger.Fatal(
                        $"В файле настроек {s.TypeSettingsMessage} параметр {name} ({s.Id}) имеет недопустимое значение {value}."
                    );
                    return false;
                }

                info.SetValue(s, value);
            }

            //Перебираем массивы строк
            foreach (var info in lists)
            {
                var name = info.Name;
                var element = UtilsSettings.GetElement(main, name);
                var strings = (string[]) info.GetValue(s);

                for (var i = 0; i < strings.Length; i++)
                {
                    var subName = GetTagStringsItem(i, name[0].ToString());

                    if (!UtilsSettings.ReadValueElement(
                        element,
                        s.PathToDoc,
                        out var value,
                        typeof(string),
                        subName,
                        Constants.DefIdSettings
                    ))
                    {
                        G.Logger.Fatal(
                            $"В файле настроек {s.TypeSettingsMessage} параметр {name}_{subName} ({s.Id}) имеет недопустимое значение {value}."
                        );
                        return false;
                    }

                    strings[i] = value.ToString();
                }
            }

            G.Logger.Info($"{nameof(ISettings)}: Загрузка настроек {s.TypeSettingsMessage} для {s.Id} прошла успешно.");
            return true;
        }

        /// <summary>
        ///     Сохраняет настройки в файл xml
        /// </summary>
        /// <param name="s">Настройки</param>
        /// <param name="id">Идентификатор конфигурации. Null = <see cref="ISettings.Id" /></param>
        /// <param name="recreate">Пересоздать конфигурацию? (затереть ненужные строки?)</param>
        /// <param name="trimStrings">Удалять лишние строки из списков?</param>
        /// <returns>Успех?</returns>
        public static bool SaveSettingsInFile(
            this ISettings s,
            string id = null,
            bool recreate = false,
            bool trimStrings = false
        )
        {
            //Если null, то берем текущую конфигурацию
            if (id == null) id = s.Id;

            //Загружаем документ из файла
            var doc = UtilsSettings.TryLoadXDoc(s.PathToDoc);

            //Если файл пуст или корень, то создаем новый корень
            if (doc?.Root == null) doc = new XDocument(new XElement(s.RootName));

            //Невозможная ситуация, но ReSharper чтобы не ругался)
            if (doc.Root == null) throw new NullReferenceException();

            //Ищем главный элемент конфигурации с указанным идентификатором
            var main = doc.Root.Elements(s.MainElementName)
                .FirstOrDefault(e => e.Attribute(s.AttributeName)?.Value == id);

            //Если элемент не был найден, то создаем его в корне
            if (main == null)
            {
                G.Logger.Info($"{nameof(ISettings)}: В файле настроек {s.TypeSettingsMessage} была создана конфигурация {id}.");

                main = new XElement(s.MainElementName, new XAttribute(s.AttributeName, id));
                doc.Root?.Add(main);
            }

            //Массивы строк (сложные параметры-свойства)
            var lists = new List<PropertyInfo>();
            var typeStrings = typeof(string[]);

            //Нужно сохранить файл при обновлении данных
            var needSaveFile = false;

            //Если необходимо пересоздать, то очищаем все элементы и заполняем своими
            if (recreate)
            {
                main.RemoveNodes();
                needSaveFile = true;
            }

            //Перебираем параметры-свойства
            foreach (var info in s.PropertiesInfo)
            {
                //Отбрасываем свойства, которые не имеют геттеров или сеттеров
                if (!info.CanRead || !info.CanWrite) continue;

                var propertyType = info.PropertyType;
                var code = Type.GetTypeCode(propertyType);

                //Отбираем массивы строк на потом
                if (propertyType == typeStrings)
                {
                    lists.Add(info);
                    continue;
                }

                //Отбрасываем сложные объекты
                if (code == TypeCode.Object) continue;

                var name = info.Name;

                //Если для свойтва нет значения по умолчанию, то свойство отбрасываем
                if (!s.Default.TryGetValue(name, out var _)) continue;

                var value = info.GetValue(s);

                needSaveFile |= UtilsSettings.WriteValueElement(main, value, name);
            }

            //Перебираем массивы строк
            foreach (var info in lists)
            {
                var name = info.Name;
                var strings = (string[]) info.GetValue(s);

                //Не нужно сохранять пустые массивы
                if (strings.Length == 0) continue;

                var element = UtilsSettings.GetElement(main, name);

                //Если необходимо обрезать ненужное, то очищаем все элементы и заполняем своими
                if (trimStrings)
                {
                    element.RemoveNodes();
                    needSaveFile = true;
                }

                for (var i = 0; i < strings.Length; i++)
                {
                    var value = strings[i];
                    if (value == null) continue;

                    var subName = GetTagStringsItem(i, name[0].ToString());

                    needSaveFile |= UtilsSettings.WriteValueElement(element, value, subName);
                }
            }

            if (needSaveFile)
            {
                //Сохраняем в файл
                doc.Save(s.PathToDoc);

                //Подождем какое то время, пока данные будут сохранены
                Thread.Sleep(500);

                G.Logger.Info($"{nameof(ISettings)}: Сохранение настроек {s.TypeSettingsMessage} для {id} в файл прошло успешно.");
                return true;
            }

            G.Logger.Info($"{nameof(ISettings)}: Сохранение настроек {s.TypeSettingsMessage} для {id} в файл не требуется.");
            return true;
        }

        /// <summary>
        ///     Удаляет конфигурацию настроек из файла xml
        /// </summary>
        /// <param name="s">Настройки</param>
        /// <param name="id">Идентификатор конфигурации</param>
        /// <returns>Файл настроек изменялся?</returns>
        public static bool DeleteSettingsInFile(this ISettings s, string id)
        {
            //Загружаем документ из файла
            var doc = UtilsSettings.TryLoadXDoc(s.PathToDoc);

            //Если файл пуст или корень, то удалять нечего
            if (doc?.Root == null)
            {
                G.Logger.Info($"{nameof(ISettings)}: Удаление настроек {s.TypeSettingsMessage} из файла для {id} не требуется.");
                return false;
            }

            //Невозможная ситуация, но ReSharper чтобы не ругался)
            if (doc.Root == null) throw new NullReferenceException();

            ////Запрет удаления конфигурации по умолчанию
            //if (id == Constants.DefIdSettings)
            //{
            //    G.Logger.Info($"Удаление настроек {s.TypeSettingsMessage} по умолчанию из файла для {id} запрещено.");
            //    return false;
            //}

            //Ищем главный элемент конфигурации с указанным идентификатором
            var main = doc.Root.Elements(s.MainElementName)
                .FirstOrDefault(element => element.Attribute(s.AttributeName)?.Value == id);

            //Если элемент не был найден, то удалять нечего
            if (main == null)
            {
                G.Logger.Info($"{nameof(ISettings)}: Удаление настроек {s.TypeSettingsMessage} из файла для {id} не требуется.");
                return false;
            }

            //Перебираем главный элементы
            foreach (var element in doc.Root.Elements())
            {
                var attribute = element.Attribute(s.AttributeName);
                if (attribute == null || attribute.Value != id) continue;

                element.Remove(); //Удаляем элемент

                doc.Save(s.PathToDoc); //Сохраняем в файл

                G.Logger.Info($"{nameof(ISettings)}: Удаление настроек {s.TypeSettingsMessage} из файла для {id} завершено.");

                return true;
            }

            G.Logger.Info($"{nameof(ISettings)}: Удаление настроек {s.TypeSettingsMessage} из файла для {id} не требуется.");
            return false;
        }

        /// <summary>
        ///     Удаляет xml файл настроек
        /// </summary>
        /// <returns>Успех?</returns>
        public static bool DeleteSettingsFile(this ISettings settings)
        {
            try
            {
                File.Delete(settings.PathToDoc);
                G.Logger.Info($"{nameof(ISettings)}: Удаление файла настроек {settings.TypeSettingsMessage} завершено.");
                return true;
            }
            catch (Exception e)
            {
                G.Logger.Error(e.ToString());
                return false;
            }
        }

        /// <summary>
        ///     Считывает имеющиеся названия конфигураций из файла настроек
        /// </summary>
        /// <returns>Массив названий имеющихся в файле конфигураций</returns>
        public static object[] GetNames(this ISettings s)
        {
            //Загружаем документ из файла
            var doc = UtilsSettings.TryLoadXDoc(s.PathToDoc);

            //Если файл пуст или корень, то выводить нечего
            if (doc?.Root == null) return new object[0];

            //Невозможная ситуация, но ReSharper чтобы не ругался)
            if (doc.Root == null) throw new NullReferenceException();

            // ReSharper disable once CoVariantArrayConversion
            return doc.Root.Elements()
                .Select(setting => setting.Attribute(s.AttributeName))
                .Where(attribute => attribute != null)
                .Select(attribute => attribute.Value)
                .ToArray();
        }

        /// <summary>
        ///     Значения числовых свойств лежат в допустимых границах?
        /// </summary>
        /// <returns></returns>
        public static bool NumbersIsOk(this ISettings s)
        {
            //Перебираем параметры-свойства
            foreach (var info in s.PropertiesInfo)
            {
                //Отбрасываем свойства, которые не имеют геттеров или сеттеров
                if (!info.CanRead || !info.CanWrite) continue;

                var propertyType = info.PropertyType;

                var name = info.Name;

                //Если для свойтва нет граничных значений
                if (!s.Limit.TryGetValue(name, out var range)) continue;

                var code = Type.GetTypeCode(propertyType);

                //Отбрасываем все не числовые типы
                if (code == TypeCode.Empty || code == TypeCode.Object || code == TypeCode.DBNull ||
                    code == TypeCode.Boolean || code == TypeCode.DateTime || code == TypeCode.String)
                    continue;

                var objValue = info.GetValue(s);
                var objBegin = range[0];
                var objEnd = range[1];

                if (!KUtils.ValueInRange(objValue, objBegin, objEnd, code))
                {
                    G.Logger.Fatal(
                        $"В файле настроек {s.TypeSettingsMessage} параметр {name} ({s.Id}) имеет недопустимое по логике значение {objValue} [{objBegin},{objEnd}]."
                    );
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        ///     Значения числовых свойств лежат в допустимых границах?
        /// </summary>
        /// <returns></returns>
        public static bool KnownColorsIsOk(this ISettings s)
        {
            //Перебираем параметры-свойства
            foreach (var info in s.PropertiesInfo)
            {
                //Отбрасываем свойства, которые не имеют геттеров или сеттеров
                if (!info.CanRead || !info.CanWrite) continue;

                var propertyType = info.PropertyType;

                //Если свойство не является известным цветом, то отбрасываем
                if (propertyType.FullName != typeof(KnownColor).FullName) continue;

                //Это не работает с другими перечислениями
                //if (propertyType == typeof(KnownColor)) continue;

                var name = info.Name;

                //Если для свойтва нет значения по умолчанию, то отбрасываем
                if (!s.Default.TryGetValue(name, out var _)) continue;

                var objValue = info.GetValue(s);
                var success = KUtils.ChangeType(objValue, out KnownColor value);
                if (!success || !value.IsKnownColor())
                {
                    G.Logger.Fatal(
                        $"В файле настроек {s.TypeSettingsMessage} параметр {name} ({s.Id}) имеет недопустимое по логике значение {objValue}."
                    );
                    return false;
                }
            }

            return true;
        }
    }
}