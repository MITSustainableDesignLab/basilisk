using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AutoMapper;

namespace Basilisk.Controls.InterfaceModels
{
    public abstract class LibraryComponent : INotifyPropertyChanged
    {
        private string name;

        static LibraryComponent()
        {
            Mapper.CreateMap<Core.LibraryComponent, LibraryComponent>();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public virtual string Category { get; set; }
        public string Comments { get; set; }
        public string DataSource { get; set; }

        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Name)));
            }
        }

        public void RaisePropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void RaisePropertyChanged(object sender, string propertyName)
        {
            PropertyChanged?.Invoke(sender, new PropertyChangedEventArgs(propertyName));
        }

        public abstract LibraryComponent Duplicate();

        protected void CopyBasePropertiesFrom(LibraryComponent source)
        {
            Category = source.Category;
            Comments = source.Comments;
            DataSource = source.DataSource;
            Name = source.Name;
        }
    }
}
