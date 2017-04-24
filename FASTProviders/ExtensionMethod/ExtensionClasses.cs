using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace Provider.ExtensionMethod
{
    public static class ExtensionClasses
    {
        public static int ToInteger(this String str)
        {
            int val = 0;

            if (Int32.TryParse(str, out val))
            {
                return val;
            }
            //Lets return negative 1 to signify error
            return -1;
        }

        public static bool ToBoolean(this String str)
        {
            if ( str.ToUpper().CompareTo("TRUE") == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static string ListToString<T>(this List<T> list)
        {
            if (list != null)
            {
                StringBuilder tempList = new StringBuilder();
                foreach(var item in list)
                {
                    tempList.Append(item.ToString() + " ; ");
                }

                return tempList.ToString();
            }
            else
            {
                return string.Empty;
            }
        }

        public static List<T> ToList<T>(this DataTable table) where T : class, new()
        {
            try
            {
                List<T> list = new List<T>();

                foreach (var row in table.AsEnumerable())
                {
                    T obj = new T();
                    DataRow data = (DataRow)row;

                    //Skip if the primary key is empty
                    if (!String.IsNullOrEmpty(data.ItemArray[0].ToString()))
                    {
                        foreach (var prop in obj.GetType().GetProperties())
                        {
                            try
                            {
                                if (!Convert.IsDBNull(row[prop.Name]))
                                {
                                    System.Reflection.PropertyInfo propertyInfo = obj.GetType().GetProperty(prop.Name);
                                    propertyInfo.SetValue(obj, Convert.ChangeType(row[prop.Name], propertyInfo.PropertyType), null);
                                }
                            }
                            catch
                            {
                                Type type = obj.GetType();
                                System.Reflection.PropertyInfo propertyInfo = type.GetProperty(prop.Name);
                                Type propertyType = propertyInfo.PropertyType;

                                var targetType = IsNullableType(propertyInfo.PropertyType) ? Nullable.GetUnderlyingType(propertyInfo.PropertyType) : propertyInfo.PropertyType;

                                if (!Convert.IsDBNull(row[prop.Name]))
                                {
                                    row[prop.Name] = Convert.ChangeType(row[prop.Name], targetType);

                                    if (row[prop.Name].GetType() == targetType)
                                    {
                                        propertyInfo.SetValue(obj, row[prop.Name], null);
                                    }
                                }
                                continue;
                            }
                        }
                        list.Add(obj);
                    }
                }
                return list;
            }
            catch
            {
                return null;
            }

        }

        private static bool IsNullableType(Type type)
        {
            return type.IsGenericType && type.GetGenericTypeDefinition().Equals(typeof(Nullable<>));
        }

    }

    
}
