using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Basilisk.Controls.Attributes;
using Basilisk.Controls.InterfaceModels;

namespace Basilisk.LibraryEditor.ViewModels
{
    public class DeletionDenialViewModel
    {
        public DeletionDenialViewModel(LibraryComponent component, IEnumerable<LibraryComponent> referencers)
        {
            Component = component;
            Referencers = referencers;
        }

        public LibraryComponent Component { get; }
        public IEnumerable<LibraryComponent> Referencers { get; }

        public IEnumerable<string> ReferencerStrings => Referencers.Select(r => $"{r.Name} ({r.TypeDisplayName})");
        public string Text => $"The {Component.TypeDisplayName} '{Component.Name}' could not be deleted because it is referenced by the following other components:";
    }
}
