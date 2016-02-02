using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Basilisk.Controls.Attributes;
using Basilisk.Controls.InterfaceModels;

namespace Basilisk.Controls
{
    public class ComponentCategoryCollection : IEnumerable<ComponentCategory>, INotifyCollectionChanged
    {
        private readonly List<ComponentCategory> categorized;

        public ComponentCategoryCollection(ICollection<LibraryComponent> components)
        {
            BackingCollection = components;
            categorized =
                components
                .GroupBy(c => c.Category)
                .Select(g => new ComponentCategory(g, g.Key))
                .ToList();
        }

        public event NotifyCollectionChangedEventHandler CollectionChanged;

        public ICollection<LibraryComponent> BackingCollection { get; }

        public void AddComponent(LibraryComponent component)
        {
            System.Diagnostics.Debug.Assert(!(component is LibraryComponentSet));
            var category = categorized.Find(cat => cat.CategoryName == component.Category);
            if (category == null)
            {
                category = new ComponentCategory(component.Category);
                categorized.Add(category);
                CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, category));
            }
            category.AddComponent(component);
            BackingCollection.Add(component);
        }

        public void AddComponent(LibraryComponentSet set)
        {
            foreach (var c in set.Components) { AddComponent(c); }
        }

        public void RemoveComponent(LibraryComponent component)
        {
            var category = categorized.Find(cat => cat.CategoryName == component.Category);
            if (category == null)
            {
                throw new InvalidOperationException($"The component '{component.Name}' could not be found in the component collection");
            }
            category.RemoveComponent(component);
            if (category.ComponentCount == 0)
            {
                var ix = categorized.IndexOf(category);
                categorized.RemoveAt(ix);
                CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, category, ix));
            }
            BackingCollection.Remove(component);
        }

        public IEnumerator<ComponentCategory> GetEnumerator() => categorized.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => categorized.GetEnumerator();
    }
}
