using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace CompositePattern
{
    // The Interface
    public interface IComponent<T>
    {
        void Add(IComponent<T> c);
        IComponent<T> Remove(T s);
        string Display(int depth);
        IComponent<T> Find(T s);
        T Name { get; set; }
    }
    // The Composite
    public class Composite<T> : IComponent<T>
    {
        List<IComponent<T>> list = null;

        public T Name { get; set; }

        public Composite(T name)
        {
            Name = name;
            list = new List<IComponent<T>>();
        }

        public void Add(IComponent<T> c)
        {
            list.Add(c);
        }

        // Finds the item from a particular point in the structure
        // and returns the composite from which it was removed
        // If not found, return the point as given
        public IComponent<T> Remove(T s)
        {
            holder = this;
            IComponent<T> p = holder.Find(s);
            if (holder != null)
            {
                (holder as Composite<T>).list.Remove(p);
                return holder;
            }
            else
                return this;
        }

        IComponent<T> holder = null;

        // Recursively looks for an item
        // Returns its reference or else null
        public IComponent<T> Find(T s)
        {
            holder = this;
            if (Name.Equals(s)) return this;
            IComponent<T> found = null;
            foreach (IComponent<T> c in list)
            {
                found = c.Find(s);
                if (found != null)
                    break;
            }
            return found;
        }

        // Displays items in a format indicating their level in the composite structure
        public string Display(int depth)
        {
            StringBuilder s = new StringBuilder(new String('-', depth));
            s.Append("Set " + Name + " length :" + list.Count + "\n");
            foreach (IComponent<T> component in list)
            {
                s.Append(component.Display(depth + 2));
            }
            return s.ToString();
        }
    }

    // The Component
    public class Component<T> : IComponent<T>
    {
        public T Name { get; set; }

        public Component(T name)
        {
            Name = name;
        }

        public void Add(IComponent<T> c)
        {
            Console.WriteLine("Cannot add to an item");
        }

        public IComponent<T> Remove(T s)
        {
            Console.WriteLine("Cannot remove directly");
            return this;
        }

        public string Display(int depth)
        {
            return new String('-', depth) + Name + "\n";
        }

        public IComponent<T> Find(T s)
        {
            if (s.Equals(Name))
                return this;
            else
                return null;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            IComponent<string> album = new Composite<string>("Album");
            IComponent<string> point = album;
            string[] s;
            string command, parameter;
            // Create and manipulate a structure 
            StreamReader instream = new StreamReader("composite.dat");
            do
            {
                string t = instream.ReadLine();
                Console.WriteLine("\t\t\t\t" + t);
                s = t.Split();
                command = s[0];
                if (s.Length > 1) parameter = s[1]; else parameter = null;
                switch (command)
                {
                    case "AddSet":
                        IComponent<string> c = new Composite<string>(parameter);
                        point.Add(c);
                        point = c;
                        break;
                    case "AddPhoto": point.Add(new Component<string>(parameter)); break;
                    case "Remove": point = point.Remove(parameter); break;
                    case "Find": point = album.Find(parameter); break;
                    case "Display": Console.WriteLine(album.Display(0)); break;
                    case "Quit": break;
                }

            } while (!command.Equals("Quit"));

            Console.ReadKey();
        }
    }
}
