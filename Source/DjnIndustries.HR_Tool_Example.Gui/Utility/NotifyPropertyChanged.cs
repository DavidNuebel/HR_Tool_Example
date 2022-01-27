using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace DjnIndustries.HR_Tool_Example.Gui.Util
{
    // Hint: This class is a helper class and can be done better, worse or different :D

    /// <summary>
    /// Base class which implemented <see cref="System.ComponentModel.INotifyPropertyChanged"/>
    /// </summary>
    public abstract class NotifyPropertyChanged : INotifyPropertyChanged
    {
        /// <summary>
        /// Raised when property has changed its value
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Raises <see cref="PropertyChanged"/>
        /// </summary>
        /// <param name="propertyName">Name of the property</param>
        protected void RaisePropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}