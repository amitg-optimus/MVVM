using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Game.Utilities
{
    public class ObservableBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string memberName = "")
        {
            if (PropertyChanged != null)            
                PropertyChanged(this, new PropertyChangedEventArgs(memberName));
        }
    }
}
