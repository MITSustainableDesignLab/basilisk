using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Basilisk.Controls.InterfaceModels;

namespace Basilisk.Controls
{
    public class ComponentCategory : IEnumerable<LibraryComponent>, INotifyCollectionChanged
    {
        private string category;
        private List<LibraryComponent> components;

        public ComponentCategory(string categoryName)
        {
            category = categoryName;
            components = new List<LibraryComponent>();
        }

        public ComponentCategory(IEnumerable<LibraryComponent> components, string categoryName)
        {
            this.category = categoryName;
            this.components = components.ToList();
        }

        public event NotifyCollectionChangedEventHandler CollectionChanged;

        public string CategoryName => category;
        public int ComponentCount => components.Count;

        public void AddComponent(LibraryComponent component)
        {
            components.Add(component);
            CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, component));
        }

        public void RemoveComponent(LibraryComponent component)
        {
            var ix = components.IndexOf(component);
            if (ix >= 0)
            {
                components.RemoveAt(ix);
                CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, component, ix));
            }
            else
            {
                throw new InvalidOperationException($"The component '{component.Name}' does not exist in the category '{category}'");
            }
        }

        public IEnumerator<LibraryComponent> GetEnumerator() => components.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => components.GetEnumerator();
    }
}
