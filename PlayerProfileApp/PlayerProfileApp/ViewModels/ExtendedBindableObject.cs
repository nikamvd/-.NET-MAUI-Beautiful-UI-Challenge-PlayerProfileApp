using System;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace PlayerProfileApp.ViewModels
{
    public abstract class ExtendedBindableObject : BindableObject
    {
        /// <summary>
        /// Sets a property's value and triggers the PropertyChanged event.
        /// </summary>
        /// <returns><c>true</c>, if property was changed, <c>false</c> otherwise.</returns>
        /// <param name="storage">The private backing variable for the property.</param>
        /// <param name="value">The desired value for the property.</param>
        /// <param name="propertyName">The property's name.</param>
        /// <typeparam name="T">The type of the parameter.</typeparam>
        protected virtual bool SetProperty<T>(ref T storage, T value, [CallerMemberName] string propertyName = null)
        {
            if (Equals(storage, value))
                return false;

            OnPropertyChanging(propertyName);

            storage = value;

            RaisePropertyChanged(propertyName);

            return true;
        }

        /// <summary>
        /// Raises the PropertyChanged event.
        /// </summary>
        /// <param name="propertyName">The property's name. When called from a property setter, this defaults to the name of the property.</param>
        protected void RaisePropertyChanged([CallerMemberName] string propertyName = null)
        {
            OnPropertyChanged(propertyName);
        }

        public void RaisePropertyChanged<T>(Expression<Func<T>> property)
        {
            var name = GetMemberInfo(property).Name;
            RaisePropertyChanged(name);
        }

        private MemberInfo GetMemberInfo(Expression expression)
        {
            MemberExpression operand;
            LambdaExpression lambdaExpression = (LambdaExpression)expression;
            if (lambdaExpression.Body as UnaryExpression != null)
            {
                UnaryExpression body = (UnaryExpression)lambdaExpression.Body;
                operand = (MemberExpression)body.Operand;
            }
            else
            {
                operand = (MemberExpression)lambdaExpression.Body;
            }
            return operand.Member;
        }
    }
}

