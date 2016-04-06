using System;
using System.Collections.Generic;

namespace DoFactory.GangOfFour.Composite.Structural
{
    /// <summary>
    /// MainApp startup class for Structural 
    /// Composite Design Pattern.
    /// </summary>
    class MainApp
    {
        /// <summary>
        /// Entry point into console application.
        /// </summary>
        static void Main()
        {
            // Create a tree structure
            Composite root = new Composite("root");
            root.Add(new Leaf("Leaf A"));
            root.Add(new Leaf("Leaf B"));

            Composite comp = new Composite("Composite X");
            comp.Add(new Leaf("Leaf XA"));
            comp.Add(new Leaf("Leaf XB"));

            root.Add(comp);
            root.Add(new Leaf("Leaf C"));

            // Add and remove a leaf
            Leaf leaf = new Leaf("Leaf D");
            root.Add(leaf);
            root.Remove(leaf);

            // Recursively display tree
            root.Display(1);

            // Wait for user
            Console.ReadKey();
        }
    }

    /// <summary>
    /// The 'Component' abstract class
    /// </summary>
    abstract class Component // removed methods not used by component to conform with SOLID
    {
        protected string name;

        // Constructor
        public Component(string name)
        {
            this.name = name;
        }
        
        public abstract void Display(int depth);
    }

    /// <summary>
    /// The 'Composite' class
    /// </summary>
    class Composite : Component
    {
        private List<Component> _children = new List<Component>();

        // Constructor
        public Composite(string name)
          : base(name)
        {
        }

        public void Add(Component component)
        {
            _children.Add(component);
        }

        public void Remove(Component component)
        {
            _children.Remove(component);
        }

        public override void Display(int depth)
        {
            Console.WriteLine(new String('-', depth) + name);

            // Recursively display child nodes
            foreach (Component component in _children)
            {
                component.Display(depth + 2);
            }
        }
    }

    /// <summary>
    /// The 'Leaf' class
    /// </summary>
    class Leaf : Component
    {
        // Constructor
        public Leaf(string name)
          : base(name)
        {
        }
        
        public override void Display(int depth)
        {
            Console.WriteLine(new String('-', depth) + name);
        }
    }
}