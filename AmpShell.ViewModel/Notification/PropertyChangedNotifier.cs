using System;
using System.ComponentModel;
using System.Linq.Expressions;

namespace AmpShell.ViewModel.Notification
{
    public class PropertyChangedNotifier : INotifyPropertyChanged
    {
        protected void SetNotifyingProperty<T>(Expression<Func<T>> expression, ref T field, T value)
        {
            if (field == null || !field.Equals(value))
            {
                T oldValue = field;
                field = value;
                OnPropertyChanged(this, new PropertyChangedExtendedEventArgs<T>(GetPropertyName(expression), oldValue, value));
            }
        }
        protected string GetPropertyName<T>(Expression<Func<T>> expression)
        {
            MemberExpression memberExpression = (MemberExpression)expression.Body;
            return memberExpression.Member.Name;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public virtual void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            PropertyChanged?.Invoke(sender, e);
        }
    }
}
