using System;
using System.Collections.Generic;
using System.ComponentModel;
using GalaSoft.MvvmLight;

namespace MyCompany.Visitors.Client.ViewModel.Base
{
	/// <summary>
	///     Custom viewmodel base.
	/// </summary>
	public class VMBase : ViewModelBase, IDisposable
	{
		readonly Dictionary<string, List<Action>> actions = new Dictionary<string, List<Action>>();
		bool isBusy;

		public VMBase()
		{
			PropertyChanged += OnPropertyChanged;
		}

		/// <summary>
		///     Set or get if the viewmodel is in busy mode (performing server petitions...)
		/// </summary>
		public bool IsBusy
		{
			get { return isBusy; }
			set
			{
				isBusy = value;
				RaisePropertyChanged(() => IsBusy);
			}
		}

		/// <summary>
		///     Dispose method.
		/// </summary>
		public void Dispose()
		{
			actions.Clear();
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		void OnPropertyChanged(object sender, PropertyChangedEventArgs propertyChangedEventArgs)
		{
			List<Action> actionList;
			if (!actions.TryGetValue(propertyChangedEventArgs.PropertyName, out actionList)) return;
			foreach (Action action in actionList)
			{
				action();
			}
		}

		/// <summary>
		///     Overrides Dispose method.
		/// </summary>
		/// <param name="dispose"></param>
		protected virtual void Dispose(bool dispose)
		{
		}

		public void SubscribeToProperty(string property, Action action)
		{
			List<Action> actionList;
			if (!actions.TryGetValue(property, out actionList))
				actionList = new List<Action>();
			actionList.Add(action);
			actions[property] = actionList;
		}
	}
}