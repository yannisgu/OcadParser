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
        private Dictionary<string, int> arrayLengths = new Dictionary<string, int>();
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

            this.ReadAllPropertiesWithStartIndex(reader, returnValue);

            this.ReadAllIndexBlocks(reader, returnValue);

            this.ReadAllLists(returnValue, reader);
            
            return returnValue;
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
                    reader.ReadUntil(index);

                    var itemValue = this.ReadPropertyValue(reader, itemType, null);
                    if (this.listTypeMappings.ContainsKey(listPropertyName))
                    {
                        var newType = this.listTypeMappings[listPropertyName](itemValue);
                        if (newType != null)
                        {
                            reader.ReadUntil(index);
                            itemValue = this.ReadPropertyValue(reader, newType, null);
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
            var value = this.ReadPropertyValue(reader, type, key);
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
            var propertyValue = this.ReadPropertyValue(reader, type, propertyName);

            property.SetValue(value, propertyValue);
        }

        private object ReadPropertyValue(OcadStreamReader reader, Type type, string propertyName)
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
            else if (type == typeof(bool))
            {
                propertyValue = reader.ReadWordBool();
            }
            else if (type.IsArray)
            {
                var length = this.arrayLengths[propertyName];
                var array = Array.CreateInstance(type.GetElementType(), length);

                for (var i = 0; i < length; i ++)
                {
                    array.SetValue(this.ReadPropertyValue(reader, type.GetElementType(), null), i);
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
            this.arrayLengths[GetPropertyName(property)] = length;
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
                this.listTypeMappings[name] = BuildAccessor<object, Type>(castMapping.Method);
            }
            return this;
        }

        private static Func<T1, T2> BuildAccessor<T1, T2>(MethodInfo method)
        {
            ParameterExpression obj = Expression.Parameter(typeof(T1), "obj");

            Expression<Func<T1, T2>> expr =
                Expression.Lambda<Func<T1, T2>>(
                    Expression.Convert(
                        Expression.Call(null,
                            method,
                            Expression.Convert(obj, method.GetParameters()[0].ParameterType)),
                        typeof(T2)),
                    obj);

            return expr.Compile();
        }
    }
}