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
        private readonly List<ComponentCategory> categorized = new List<ComponentCategory>();

        public ComponentCategoryCollection(ICollection<LibraryComponent> components)
        {
            BackingCollection = components;
            var cats =
                components
                .GroupBy(c => c.Category)
                .Select(g => new ComponentCategory(g, g.Key))
                .OrderBy(c => c.CategoryName);
            foreach (var c in cats) { AddCategory(c); }
        }

        public event EventHandler<CategoryChangedEventArgs> ChildCategoryModified;
        public event NotifyCollectionChangedEventHandler CollectionChanged;

        public ICollection<LibraryComponent> BackingCollection { get; }

        public void AddComponent(LibraryComponent component)
        {
            System.Diagnostics.Debug.Assert(!(component is LibraryComponentSet));
            var category = categorized.Find(cat => cat.CategoryName == component.Category);
            if (category == null)
            {
                category = new ComponentCategory(component.Category);
                AddCategory(category);
            }
            category.AddComponent(component);
            BackingCollection.Add(component);
        }

        public void AddComponent(LibraryComponentSet set)
        {
            foreach (var c in set.Components) { AddComponent(c); }
        }

        public void PurgeEmptyCategories()
        {
            var empty = categorized.Where(cat => cat.ComponentCount == 0).FirstOrDefault();
            while (empty != null)
            {
                RemoveCategory(empty);
                empty = categorized.Where(cat => cat.ComponentCount == 0).FirstOrDefault();
            }
        }

        public void RemoveComponent(LibraryComponent component)
        {
            var category = categorized.Find(cat => cat.CategoryName == component.Category);
            if (category == null)
            {
                throw new InvalidOperationException($"The component '{component.Name}' could not be found in the component collection");
            }
            category.RemoveComponent(component);
            BackingCollection.Remove(component);
        }

        public IEnumerator<ComponentCategory> GetEnumerator() => categorized.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => categorized.GetEnumerator();

        private void AddCategory(ComponentCategory c)
        {
            categorized.Add(c);
            c.CollectionChanged += CategoryChanged;
            CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, c));
        }

        private void CategoryChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            ChildCategoryModified?.Invoke(this, new CategoryChangedEventArgs(sender as ComponentCategory));
        }

        private void RemoveCategory(ComponentCategory c)
        {
            c.CollectionChanged -= CategoryChanged;
            var ix = categorized.IndexOf(c);
            categorized.RemoveAt(ix);
            CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, c, ix));
        }
    }
}
