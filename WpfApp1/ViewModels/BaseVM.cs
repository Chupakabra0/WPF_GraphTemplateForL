using System.ComponentModel;
using System.Runtime.CompilerServices;
using PropertyChanged;

namespace WpfApp1 {
    [AddINotifyPropertyChangedInterface]
    public abstract class BaseVM : INotifyPropertyChanged {
        public event PropertyChangedEventHandler PropertyChanged;
        
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null) {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}