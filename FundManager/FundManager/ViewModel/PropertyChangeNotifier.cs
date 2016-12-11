using System.ComponentModel;

namespace FundManager.ViewModel
{
    // Marked as abstract to prevent instantiation 
    public abstract class PropertyChangeNotifier : INotifyPropertyChanged , IDataErrorInfo
    {
        public void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Will be called for each and every property when ever its value is changed
        /// </summary>
        /// <param name="columnName">Name of the property whose value is changed</param>
        /// <returns></returns>
        public string this[string columnName]
        {
            get
            {
                return Validate(columnName);
            }
        }

        public string Error
        {
            get { return ""; }
        }

        public virtual string Validate(string columnName)
        {
            return string.Empty;
        }
    }
}
