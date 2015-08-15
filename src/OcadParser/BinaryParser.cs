namespace OcadParser
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Reflection;

    public class BinaryParser<T> 
        where T : IBinaryParsable<T>
    {
        private Expression<Func<T, object>>[] propertyOrder;
        private Dictionary<string, Func<T, int>> arrayLengths = new Dictionary<string, Func<T, int>>();
        private Dictionary<string, Expression<Func<T, int>>> propertyStartIndexes = new Dictionary<string, Expression<Func<T, int>>>();

        private Dictionary<string, Expression<Func<T, IEnumerable<int>>>> propertyIndexes =
            new Dictionary<string, Expression<Func<T, IEnumerable<int>>>>();

        public BinaryParser<T> SetStartIndex(Expression<Func<T, object>> property, Expression<Func<T, int>> startIndexProperty)
        {
            this.propertyStartIndexes[GetPropertyName(property)] = startIndexProperty;
            return this;
        }

        public BinaryParser<T> SetIndexes(Expression<Func<T, object>> property, Expression<Func<T, IEnumerable<int>>> startIndexProperty)
        {
            this.propertyIndexes[GetPropertyName(property)] = startIndexProperty;
            return this;
        }

        public BinaryParser<T> SetPropertyOrder(params Expression<Func<T, object>>[] property)
        {
            this.propertyOrder = property;
            return this;
        }

        public T Read(OcadStreamReader reader) 
        {
            var returnValue = Activator.CreateInstance<T>();

            returnValue.SetupBinaryParser(this);
            this.ReadSimplePropertiesByOrder(reader, returnValue);
            ReadAllDynamicLists(reader, returnValue);
            this.ReadAllPropertiesWithStartIndex(reader, returnValue);

            this.ReadAllIndexBlocks(reader, returnValue);

            this.ReadAllLists(returnValue, reader);

            ReadSpecialStringLists(returnValue, reader);

            afterActions.ForEach(_ => _(returnValue));

            return returnValue;
        }

        private void ReadAllDynamicLists(OcadStreamReader reader, T value)
        {
            foreach (var listConfig in dynamicLists)
            {
                var listPropertyName = listConfig.Key;
                var property = typeof(T).GetProperty(listPropertyName);
                var itemType = property.PropertyType.GetGenericArguments()[0];
                var listType = typeof(List<>).MakeGenericType(itemType);
                var list = property.GetValue(value);

                while (listConfig.Value(value))
                {
                    var itemValue = this.ReadPropertyValue(reader, itemType, null, value);
                    listType.GetMethod("Add").Invoke(list, new[] {itemValue});
                }
            }
        }

        private void ReadSpecialStringLists(T returnValue, OcadStreamReader reader)
        {
            foreach (var listProperty in this.specialStringListMapping.Keys)
            {
                var property = typeof(T).GetProperty(GetPropertyName(listProperty));
                var list = new List<OcadFileSpecialString>();
                var getIndexes = this.specialStringListMapping[listProperty];
                var indexes = getIndexes.Compile()(returnValue);
                foreach (var index in indexes)
                {
                    reader.ReadUntil(index.Pos);
                    var bytes = reader.ReadBytes(index.Len);
                    list.Add(new OcadFileSpecialString(bytes, index.RecType, index.ObjIndex));
                }

                property.SetValue(returnValue, list);
            }
        }

        public BinaryParser<T> SetArrayLength(Expression<Func<T, object>> property, Func<T, int> function)
        {
            this.arrayLengths[GetPropertyName(property)] = function;
            return this;
        }

        private void ReadSimplePropertiesByOrder(OcadStreamReader reader, T returnValue)
        {
            if (this.propertyOrder != null)
            {
                foreach (var propertyExpression in this.propertyOrder)
                {
                    this.ReadAndSetPropertyValue(reader, GetPropertyName(propertyExpression), returnValue);
                }
            }
        }

        private void ReadAllPropertiesWithStartIndex(OcadStreamReader reader, T returnValue)
        {
            while (this.propertyStartIndexes.Any())
            {
                var nextProperty = this.propertyStartIndexes.OrderBy(_ => _.Value.Compile().Invoke(returnValue)).First();
                reader.ReadUntil(nextProperty.Value.Compile().Invoke(returnValue));
                this.ReadAndSetArrayValue(reader, nextProperty.Key, returnValue);
                this.propertyStartIndexes.Remove(nextProperty.Key);
            }
        }

        private void ReadAllIndexBlocks(OcadStreamReader reader, T returnValue)
        {
            var alreadyReadIndexes = new List<int>();
            while (
                this.propertyIndexes.SelectMany(_ => _.Value.Compile().Invoke(returnValue))
                    .Any(_ => _ != 0 && !alreadyReadIndexes.Contains(_)))
            {
                var property =
                    this.propertyIndexes.Select(_ => new { Property = _.Key, Value = _.Value.Compile().Invoke(returnValue) })
                        .First(_ => !_.Value.All(v => v == 0 || alreadyReadIndexes.Contains(v)));
                var index = property.Value.First(_ => _ != 0 && !alreadyReadIndexes.Contains(_));
                reader.ReadUntil(index);
                this.ReadAndSetArrayValue(reader, property.Property, returnValue);
                alreadyReadIndexes.Add(index);
            }
        }

        private void ReadAllLists(T returnValue, OcadStreamReader reader)
        {
            foreach (var listPropertyName in this.listIndexes.Keys)
            {
                var property = typeof(T).GetProperty(listPropertyName);
                var itemType = property.PropertyType.GetGenericArguments()[0];
                var listType = typeof(List<>).MakeGenericType(itemType);
                var list = Activator.CreateInstance(listType);
                var getIndexes = this.listIndexes[listPropertyName];
                var indexes = getIndexes(returnValue);
                foreach (var index in indexes)
                {
                    if (index == 0)
                    {
                        continue;
                    }
                    reader.ReadUntil(index);

                    var itemValue = this.ReadPropertyValue(reader, itemType, null, returnValue);
                    if (this.listTypeMappings.ContainsKey(listPropertyName))
                    {
                        var newType = this.listTypeMappings[listPropertyName](itemValue);
                        if (newType != null)
                        {
                            reader.ReadUntil(index);
                            itemValue = this.ReadPropertyValue(reader, newType, null, returnValue);
                        }

                    }

                    listType.GetMethod("Add").Invoke(list, new[] { itemValue });
                    
                }

                property.SetValue(returnValue, list);
            }
        }

        private void ReadAndSetArrayValue(OcadStreamReader reader, string key, T item)
        {
            var property = typeof(T).GetProperty(key);
            var type = property.PropertyType.GetElementType();
            var value = this.ReadPropertyValue(reader, type, key, item);
            var oldArray = (Array)property.GetValue(item);
            if (oldArray == null)
            {
                oldArray = Array.CreateInstance(type, 0);
            }

            var newArray = Array.CreateInstance(type, oldArray.Length + 1);
            for (var i = 0; i < oldArray.Length; i++)
            {
                newArray.SetValue(oldArray.GetValue(i), i);
            }
            newArray.SetValue(value, newArray.Length -1);
            property.SetValue(item, newArray);
        }

        protected virtual void ReadAndSetPropertyValue(OcadStreamReader reader, string propertyName, T value)
        {
            var property = typeof(T).GetProperty(propertyName);
            var type = property.PropertyType;
            var propertyValue = this.ReadPropertyValue(reader, type, propertyName, value);

            property.SetValue(value, propertyValue);
        }

        private object ReadPropertyValue(OcadStreamReader reader, Type type, string propertyName, T value)
        {
            object propertyValue = null;
            if (type == typeof(Int16))
            {
                propertyValue = reader.ReadSmallInt();
            }
            else if (type == typeof(Byte))
            {
                propertyValue = reader.ReadByte();
            }
            else if (type == typeof(double))
            {
                propertyValue = reader.ReadDouble();
            }
            else if (type == typeof(int))
            {
                propertyValue = reader.ReadInt();
            }
            else if (type == typeof(long))
            {
                propertyValue = reader.ReadLong();
            }
            else if (type == typeof(string))
            {
                propertyValue = reader.ReadString();
            }
            else if (type == typeof(TdPoly))
            {
                propertyValue = reader.ReadTdPoly();
            }
            else if (type == typeof(ushort))
            {
                propertyValue = reader.ReadWord();
            }
            else if (type == typeof (char))
            {
                propertyValue = reader.ReadChar();
            }
            else if (type == typeof(bool))
            {
                propertyValue = reader.ReadWordBool();
            }
            else if (type.IsArray)
            {
                var length = this.arrayLengths[propertyName](value);
                var array = Array.CreateInstance(type.GetElementType(), length);

                for (var i = 0; i < length; i ++)
                {
                    array.SetValue(this.ReadPropertyValue(reader, type.GetElementType(), null, value), i);
                }

                propertyValue = array;
            }
            else if (type.GetInterfaces().Any(x => x.IsGenericType && x.GetGenericTypeDefinition() == typeof(IBinaryParsable<>)))
            {
                var childParserType = typeof(BinaryParser<>).MakeGenericType(type);
                var childParser = Activator.CreateInstance(childParserType);
                var readMethod = childParserType.GetMethod("Read");
                propertyValue = readMethod.Invoke(childParser, new object[] { reader });
            }
            return propertyValue;
        }

        private static string GetPropertyName
            (Expression<Func<T, object>> expression)
        {
            var lambda = expression as LambdaExpression;
            MemberExpression memberExpression;
            if (lambda.Body is UnaryExpression)
            {
                var unaryExpression = lambda.Body as UnaryExpression;
                memberExpression = unaryExpression.Operand as MemberExpression;
            }
            else
            {
                memberExpression = lambda.Body as MemberExpression;
            }

            Debug.Assert(memberExpression != null, "Please provide a lambda expression like 'n => n.PropertyName'");
            var propertyInfo = memberExpression.Member as PropertyInfo;

            if (propertyInfo != null)
            {
                return propertyInfo.Name;
            }
            return null;
        }

        private static string GetPropertyName<T2>
            (Expression<Func<T, IList<T2>>> expression)
        {
            var lambda = expression as LambdaExpression;
            MemberExpression memberExpression;
            if (lambda.Body is UnaryExpression)
            {
                var unaryExpression = lambda.Body as UnaryExpression;
                memberExpression = unaryExpression.Operand as MemberExpression;
            }
            else
            {
                memberExpression = lambda.Body as MemberExpression;
            }

            Debug.Assert(memberExpression != null, "Please provide a lambda expression like 'n => n.PropertyName'");
            var propertyInfo = memberExpression.Member as PropertyInfo;

            if (propertyInfo != null)
            {
                return propertyInfo.Name;
            }
            return null;
        }

        public BinaryParser<T> SetArrayLength(Expression<Func<T, object>> property, int length)
        {
            var param = Expression.Parameter(typeof (T));
            var value = Expression.Constant(length);
            SetArrayLength(property, Expression.Lambda<Func<T, int>>(value, param).Compile()); 
            return this;
        }

        Dictionary<string, Func<T, IEnumerable<int>>> listIndexes = new Dictionary<string, Func<T, IEnumerable<int>>>();
        Dictionary<string, Func<object, Type>> listTypeMappings = new Dictionary<string, Func<object, Type>>();

        public BinaryParser<T> ConfigureList<T2>(Expression<Func<T, IList<T2>>> property, Func<T, IEnumerable<int>> getIndexes, Func<T2, Type> castMapping = null)
        {
            var name = GetPropertyName(property);
            this.listIndexes[name] = getIndexes;
            if (castMapping != null)
            {
                this.listTypeMappings[name] = BuildAccessor<object, Type, T2, Type>(castMapping);
            }
            return this;
        }

        private Func<T1, T2> BuildAccessor<T1, T2, T3, T4>(Func<T3, T4> method)
        {
            ParameterExpression obj = Expression.Parameter(typeof(T1), "obj");

            Expression<Func<T1, T2>> expr =
                Expression.Lambda<Func<T1, T2>>(
                    Expression.Convert(
                        Expression.Call(
                                Expression.Constant(method.Target),
                                method.Method,
                            Expression.Convert(obj, method.Method.GetParameters()[0].ParameterType)),
                        typeof(T2)),
                    obj);

            return expr.Compile();
        }

        private Dictionary<Expression<Func<T, object>>, Expression<Func<T, IEnumerable<OcadFileStringIndex>>>>
            specialStringListMapping = new Dictionary<Expression<Func<T, object>>, Expression<Func<T, IEnumerable<OcadFileStringIndex>>>>(); 

        public void ConfigureSpecialStringList(Expression<Func<T, object>>  stringList, Expression<Func<T, IEnumerable<OcadFileStringIndex>>> stringIndexes)
        {
            specialStringListMapping[stringList] = stringIndexes;
        }

        private readonly Dictionary<string, Func<T, bool>> dynamicLists = new Dictionary<string, Func<T, bool>>(); 

        public void SetDynamicList(Expression<Func<T, object>> listProperty, Func<T,bool> continueFunc)
        {
            dynamicLists[GetPropertyName(listProperty)] = continueFunc;
        }
        private readonly List<Action<T>> afterActions = new List<Action<T>>(); 
        public void AddAfterParseFunction(Action<T> action)
        {
            afterActions.Add(action);
        }
    }
}