using System;
using System.ComponentModel;
using System.Linq.Expressions;

namespace Common.ViewModels
{
	public class CoreViewModel : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;

		protected void OnPropertyChanged<T>(Expression<Func<T>> selector)
		{
			if( selector == null)
			{
				throw new ArgumentNullException("selector");
			}

			var body = selector.Body as MemberExpression;
			if( body == null)
			{
				throw new ArgumentException("Given expression is not of type" + typeof(MemberExpression));
			}

			var propertyName = body.Member.Name;
			if( this.PropertyChanged != null)
			{ 
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
	}
}
