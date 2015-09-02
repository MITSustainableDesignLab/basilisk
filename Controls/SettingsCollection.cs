using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basilisk.Controls
{
    public class SettingsCollection : KeyedCollection<string, SimulationSetting>, IEditableCollectionView
    {
        private SimulationSetting currentlyBeingEdited = null;

        public bool TryGetValue(string propertyName, out SimulationSetting value)
        {
            return this.Dictionary.TryGetValue(propertyName, out value);
        }

        #region KeyedCollection
        protected override string GetKeyForItem(SimulationSetting item)
        {
            return item.PropertyName;
        }
        #endregion

        #region IEditableCollectionView
        public object AddNew()
        {
            throw new InvalidOperationException("SettingsCollections cannot have settings added after construction");
        }

        public bool CanAddNew
        {
            get { return false; }
        }

        public bool CanCancelEdit
        {
            get { return true; }
        }

        public bool CanRemove
        {
            get { return false; }
        }

        public void CancelEdit()
        {
            if (currentlyBeingEdited != null) { currentlyBeingEdited.CancelEdit(); }
        }

        public void CancelNew()
        {
            throw new InvalidOperationException("SettingsCollections cannot have settings added after construction");
        }

        public void CommitEdit()
        {
            if (currentlyBeingEdited != null)
            {
                currentlyBeingEdited.EndEdit();
                currentlyBeingEdited = null;
            }
        }

        public void CommitNew()
        {
            throw new InvalidOperationException("SettingsCollections cannot have settings added after construction");
        }

        public object CurrentAddItem
        {
            get { throw new InvalidOperationException("SettingsCollections cannot have settings added after construction"); }
        }

        public object CurrentEditItem
        {
            get { return currentlyBeingEdited; }
        }

        public void EditItem(object item)
        {
            if (currentlyBeingEdited != null) { throw new InvalidOperationException("An item is already being edited"); }
            if (!this.Contains(item)) { throw new InvalidOperationException("A SettingsCollection cannot edit an item it does not contain"); }
            var setting = (SimulationSetting)item;
            setting.BeginEdit();
            currentlyBeingEdited = setting;
        }

        public bool IsAddingNew
        {
            get { return false; }
        }

        public bool IsEditingItem
        {
            get { return currentlyBeingEdited != null; }
        }

        public NewItemPlaceholderPosition NewItemPlaceholderPosition
        {
            get
            {
                throw new InvalidOperationException("SettingsCollections cannot have settings added after construction");
            }
            set
            {
                throw new InvalidOperationException("SettingsCollections cannot have settings added after construction");
            }
        }

        public void Remove(object item)
        {
            throw new InvalidOperationException("SettingsCollections cannot have settings removed after construction");
        }
        #endregion
    }
}
