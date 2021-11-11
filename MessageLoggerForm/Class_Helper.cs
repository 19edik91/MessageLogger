using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageLoggerForm
{
    public static class Class_Helper
    {
        public abstract class Enumeration : IComparable
        {
            public string Name { get; private set; }
            public int Id { get; private set; }

            protected Enumeration(string name, int id) => (Name, Id) = (name, id);

            public override string ToString() => Name;

            public static IEnumerable<T> GetAll<T>() where T : Enumeration, new()
            {
                var type = typeof(T);
                var fields = type.GetType().GetFields(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.DeclaredOnly);

                foreach(var info in fields)
                {
                    var instance = new T();
                    var locatedValue = info.GetValue(instance) as T;
                    if(locatedValue != null)
                    {
                        yield return locatedValue;
                    }
                }
            }

            public int CompareTo(object other) => Id.CompareTo(((Enumeration)other).Id);
        }

    }
}
