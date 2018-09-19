using Common.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelpApp.ViewModel
{
	public class RootTreeViewModel : TreeViewItemViewModel
	{
		public RootTreeViewModel(string name) : base(null, true)
		{
			this.Name = name;
		}

		public string Name { get; set; }
	}
}
