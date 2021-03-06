using System;
using System.ComponentModel.Composition;
using System.Linq;
using System.Windows.Input;
using Gemini.Framework.Services;

namespace Gemini.Framework
{
	public abstract class Tool : LayoutItemBase, ITool
	{
		private ICommand _closeCommand;
		public ICommand CloseCommand
		{
			get { return _closeCommand ?? (_closeCommand = new RelayCommand(p => IsVisible = false, p => true)); }
		}

	    public virtual string ContentId
	    {
            get { return ExportType.AssemblyQualifiedName; }
	    }

	    private Type ExportType
	    {
	        get
	        {
	            var exportAttribute = GetType().GetCustomAttributes(typeof(ExportAttribute), false)
                    .Cast<ExportAttribute>().FirstOrDefault();
                if (exportAttribute != null)
	                return exportAttribute.ContractType;
	            throw new Exception("Tool must have an [Export] attribute.");
	        }
	    }

	    public abstract PaneLocation PreferredLocation { get; }

	    public virtual double PreferredWidth
	    {
            get { return 200; }
	    }

        public virtual double PreferredHeight
        {
            get { return 200; }
        }

		public virtual Uri IconSource
		{
			get { return null; }
		}

        private bool _isSelected;
        public bool IsSelected
        {
            get { return _isSelected; }
            set
            {
                _isSelected = value;
                NotifyOfPropertyChange(() => IsSelected);
            }
        }

		private bool _isVisible;
		public bool IsVisible
		{
			get { return _isVisible; }
			set
			{
				_isVisible = value;
				NotifyOfPropertyChange(() => IsVisible);
			}
		}

		protected Tool()
		{
			IsVisible = true;
		}
	}
}