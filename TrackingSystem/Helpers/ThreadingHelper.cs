using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace TrackingSystem.Helpers
{
	public static class ThreadingHelper
	{
		/// <summary>
		/// Выполняет функциию или делегат компонента в другом потоке
		/// </summary>
		/// <param name="control">Компонент</param>
		/// <param name="action">Функция или делегат исполняемая в другом потоке</param>
		public static void InvokeEx(this Control control, Action action)
		{
			if (control.InvokeRequired)
					control.Invoke(action);
			else
				action();
		}

		/// <summary>
		/// Выполняет функциию или делегат компонента в другом потоке
		/// </summary>
		/// <param name="control">Компонент</param>
		/// <param name="action">Функция или делегат исполняемая в другом потоке</param>
		public static void InvokeEx(this Control control, Action<int> action, object[] methonds)
		{
			if (control.InvokeRequired)
					control.Invoke(action, methonds);
			else
				action(methonds[0].exToInt());
		}

	}
}
