using System.ComponentModel;
using System.Reflection;

using Basilisk.Controls;
using Basilisk.Controls.InterfaceModels;

namespace Basilisk.LibraryEditor.ViewModels
{
    public class MergeCollisionViewModel : INotifyPropertyChanged
    {
        private readonly MergeCollision collision;

        private MergeCollisionAction action = MergeCollisionAction.Overwrite;

        public MergeCollisionViewModel(MergeCollision collision)
        {
            this.collision = collision;
        }

        public MergeCollisionViewModel(LibraryComponent oldComponent, LibraryComponent newComponent)
        {
            this.collision = new MergeCollision(oldComponent, newComponent);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public string ComponentTypeName => OriginalComponent.GetType().GetCustomAttribute<DisplayNameAttribute>()?.DisplayName ?? OriginalComponent.GetType().Name;
        public LibraryComponent NewComponent => collision.NewComponent;
        public LibraryComponent OriginalComponent => collision.OriginalComponent;

        public bool Overwrite
        {
            get { return action == MergeCollisionAction.Overwrite; }
            set
            {
                var newAction = value ? MergeCollisionAction.Overwrite : MergeCollisionAction.Ignore;
                ChangeAction(newAction);
            }
        }

        public bool Ignore
        {
            get { return action == MergeCollisionAction.Ignore; }
            set
            {
                var newAction = value ? MergeCollisionAction.Ignore : MergeCollisionAction.Overwrite;
                ChangeAction(newAction);
            }
        }

        private void ChangeAction(MergeCollisionAction newAction)
        {
            if (action == newAction) { return; }
            action = newAction;
            PropertyChanged(this, new PropertyChangedEventArgs(nameof(Overwrite)));
            PropertyChanged(this, new PropertyChangedEventArgs(nameof(Ignore)));
        }
    }
}
