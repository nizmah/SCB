using System;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Windows.Shell;

namespace CustomChromeLibrary
{
	/// <summary>
	/// Interaction logic for CaptionButtons.xaml
	/// </summary>
	public partial class CaptionButtons : UserControl
	{
		public CaptionButtons()
		{
			InitializeComponent();
		}

		private void SpecialMinimizeButton_Click(object sender, RoutedEventArgs e)
		{
            //Window.GetWindow(this).WindowState = WindowState.Minimized;
            SystemCommands.MinimizeWindow(Window.GetWindow(this));
		}

		private void SpecialCloseButton_Click(object sender, RoutedEventArgs e)
		{
			SystemCommands.CloseWindow(Window.GetWindow(this));
		}

        private void MaximizeButton_Click(object sender, RoutedEventArgs e)
        {
            Window.GetWindow(this).MaxHeight = SystemParameters.MaximizedPrimaryScreenHeight;
            Window.GetWindow(this).MaxWidth = SystemParameters.MaximizedPrimaryScreenWidth;
            
            
            //Window.GetWindow(this).WindowState = WindowState.Maximized;
            //SystemCommands.MaximizeWindow(Window.GetWindow(this));
        }
	}
}
