using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace XF_Nodejs
{
   public class ModelPropertyChanged : INotifyPropertyChanged
    {


        protected virtual bool SetProperty<T>(ref T storage, T value, Action action,
           [CallerMemberName] string propertyName = null)
        {
            var r = SetProperty(ref storage, value, propertyName);
            if (r) action?.Invoke();
            return r;
        }

        protected virtual bool SetProperty<T>(
              ref T backingStore, T value,
              [CallerMemberName]string propertyName = "",
              Action onChanged = null,
              Func<T, T, bool> validateValue = null)
        {
            //if value didn't change
            if (EqualityComparer<T>.Default.Equals(backingStore, value))
                return false;

            //if value changed but didn't validate
            if (validateValue != null && !validateValue(backingStore, value))
                return false;

            backingStore = value;
            onChanged?.Invoke();
            OnPropertyChanged(propertyName);
            return true;
        }


        /// <summary>
        /// Occurs when property changed.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Raises the property changed event.
        /// </summary>
        /// <param name="propertyName">Property name.</param>
        protected virtual void OnPropertyChanged([CallerMemberName]string propertyName = "") =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

    }
}
