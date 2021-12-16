using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Management;
using Microsoft.Win32;

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

                foreach (var info in fields)
                {
                    var instance = new T();
                    var locatedValue = info.GetValue(instance) as T;
                    if (locatedValue != null)
                    {
                        yield return locatedValue;
                    }
                }
            }

            public int CompareTo(object other) => Id.CompareTo(((Enumeration)other).Id);
        }

        public static class ComPorts
        {
            /// <summary>
            /// Checks the Win32 properties for available SERIAL-COM-Ports
            /// </summary>
            /// <returns>String-List with the available COM ports </returns>
            public static List<string> GetAvailablePorts()
            {
                List<string> lstComPorts = new List<string>();

                using (ManagementClass i_Entity = new ManagementClass("Win32_PnPEntity"))
                {
                    foreach (ManagementObject i_Inst in i_Entity.GetInstances())
                    {
                        Object o_Guid = i_Inst.GetPropertyValue("ClassGuid");
                        if (o_Guid == null || o_Guid.ToString().ToUpper() != "{4D36E978-E325-11CE-BFC1-08002BE10318}")
                            continue; // Skip all devices except device class "PORTS"

                        String s_Caption = i_Inst.GetPropertyValue("Caption").ToString();
                        String s_Manufact = i_Inst.GetPropertyValue("Manufacturer").ToString();
                        String s_DeviceID = i_Inst.GetPropertyValue("PnpDeviceID").ToString();
                        String s_RegPath = "HKEY_LOCAL_MACHINE\\System\\CurrentControlSet\\Enum\\" + s_DeviceID + "\\Device Parameters";
                        String s_PortName = Registry.GetValue(s_RegPath, "PortName", "").ToString();

                        int s32_Pos = s_Caption.IndexOf(" (COM");
                        if (s32_Pos > 0) // remove COM port from description
                            s_Caption = s_Caption.Substring(0, s32_Pos);

                        Console.WriteLine("Port Name:    " + s_PortName);
                        Console.WriteLine("Description:  " + s_Caption);
                        Console.WriteLine("Manufacturer: " + s_Manufact);
                        Console.WriteLine("Device ID:    " + s_DeviceID);
                        Console.WriteLine("-----------------------------------");


                        if (s_Caption.Contains("SERIAL") == true)
                        {
                            Console.WriteLine("Used Port: " + s_PortName);

                            lstComPorts.Add(s_PortName);
                        }
                    }
                }

                return lstComPorts;
            }
        }


        /// <summary>
        /// Base class for enumeration classes 
        /// </summary>
        public abstract class EnumBase<T>
        {
            /// <summary>
            /// Checks if the used type is a enum base type
            /// </summary>
            private void CheckBaseType()
            {
                //Check if base type system enum class
                if (typeof(T).BaseType != typeof(Enum))
                {
                    throw new ArgumentException("T must be of type System.Enum");
                }
            }

            /// <summary>
            /// Returns the whole enumeration as a list.
            /// </summary>
            /// <returns>List of the enumeration </returns>
            public List<T> GetEnumList()
            {
                //Check if base type system enum class
                CheckBaseType();
                return Enum.GetValues(typeof(T)).Cast<T>().ToList();
            }

            /// <summary>
            /// Returns the enum name as a string. Checks previously if
            /// the selected type is an enum type
            /// </summary>
            /// <typeparam name="T">  </typeparam>
            /// <param name="obj"></param>
            /// <returns>The selected enum is returned as a string </returns>
            public string GetEnumAsString(object obj)
            {
                //Check if base type system enum class
                CheckBaseType();
                return Enum.GetName(typeof(T), obj);
            }

            /// <summary>
            /// Returns the enum as a string-list
            /// </summary>
            /// <returns>Returns the whole enumeration as a string-list </returns>
            public List<string> GetEnumListAsString()
            {
                //Check if base type system enum class
                CheckBaseType();

                return Enum.GetNames(typeof(T)).ToList();
            }

            /// <summary>
            /// Converts the string value (not case sensitive) into an enum-value
            /// </summary>
            /// <param name="value">The string which is parsed into an enum type</param>
            /// <returns>the enum value</returns>
            public T ToEnum(string value)
            {
                CheckBaseType();
                return (T)Enum.Parse(typeof(T), value, true);
            }

            /// <summary>
            /// Converts the int value into an enum-value
            /// </summary>
            /// <param name="value">The int value which is used to parse into an enum</param>
            /// <returns>the enum value </returns>
            public T ToEnum(int value)
            {
                CheckBaseType();
                var name = Enum.GetName(typeof(T), value);
                return ToEnum(name);
            }

        }

    }

}
