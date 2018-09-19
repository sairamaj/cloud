﻿using System.Collections.ObjectModel;
using System.ComponentModel;

namespace Common.ViewModels
{
	// https://www.codeproject.com/Articles/26288/Simplifying-the-WPF-TreeView-by-Using-the-ViewMode
	public class TreeViewItemViewModel : INotifyPropertyChanged
	{
		#region Data

		static readonly TreeViewItemViewModel DummyChild = new TreeViewItemViewModel();

		readonly ObservableCollection<TreeViewItemViewModel> _children;
		readonly TreeViewItemViewModel _parent;

		bool _isExpanded;
		bool _isSelected;

		#endregion // Data

		#region Constructors

		protected TreeViewItemViewModel(TreeViewItemViewModel parent, bool lazyLoadChildren)
		{
			_parent = parent;

			_children = new ObservableCollection<TreeViewItemViewModel>();

			if (lazyLoadChildren)
				_children.Add(DummyChild);
		}

		// This is used to create the DummyChild instance.
		private TreeViewItemViewModel()
		{
		}

		#endregion // Constructors

		#region Presentation Members

		#region Children

		/// <summary>
		/// Returns the logical child items of this object.
		/// </summary>
		public ObservableCollection<TreeViewItemViewModel> Children
		{
			get { return _children; }
		}

		#endregion // Children

		#region HasLoadedChildren

		/// <summary>
		/// Returns true if this object's Children have not yet been populated.
		/// </summary>
		public bool HasDummyChild
		{
			get { return this.Children.Count == 1 && this.Children[0] == DummyChild; }
		}

		#endregion // HasLoadedChildren

		#region IsExpanded

		/// <summary>
		/// Gets/sets whether the TreeViewItem 
		/// associated with this object is expanded.
		/// </summary>
		public bool IsExpanded
		{
			get { return _isExpanded; }
			set
			{
				if (value != _isExpanded)
				{
					_isExpanded = value;
					this.OnPropertyChanged("IsExpanded");
				}

				// Expand all the way up to the root.
				if (_isExpanded && _parent != null)
					_parent.IsExpanded = true;

				// Lazy load the child items, if necessary.
				if (this.HasDummyChild)
				{
					this.Children.Remove(DummyChild);
					this.LoadChildren();
				}
			}
		}

		#endregion // IsExpanded

		#region IsSelected

		/// <summary>
		/// Gets/sets whether the TreeViewItem 
		/// associated with this object is selected.
		/// </summary>
		public bool IsSelected
		{
			get { return _isSelected; }
			set
			{
				if (value != _isSelected)
				{
					_isSelected = value;
					this.OnPropertyChanged("IsSelected");
				}
			}
		}

		#endregion // IsSelected

		#region LoadChildren

		/// <summary>
		/// Invoked when the child items need to be loaded on demand.
		/// Subclasses can override this to populate the Children collection.
		/// </summary>
		protected virtual void LoadChildren()
		{
		}

		#endregion // LoadChildren

		#region Parent

		public TreeViewItemViewModel Parent
		{
			get { return _parent; }
		}

		#endregion // Parent

		#endregion // Presentation Members

		#region INotifyPropertyChanged Members

		public event PropertyChangedEventHandler PropertyChanged;

		protected virtual void OnPropertyChanged(string propertyName)
		{
			if (this.PropertyChanged != null)
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
		}

		#endregion // INotifyPropertyChanged Members
	}

}
