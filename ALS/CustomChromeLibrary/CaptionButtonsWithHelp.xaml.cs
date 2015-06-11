using System;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Windows.Shell;

namespace CustomChromeLibrary
{
	/// <summary>
	/// Interaction logic for CaptionButtonsWithHelp.xaml
	/// </summary>
	public partial class CaptionButtonsWithHelp : UserControl
	{
		public CaptionButtonsWithHelp()
		{
			InitializeComponent();
		}

        private void MinimizeButton_Click(object sender, RoutedEventArgs e)
        {
            SystemCommands.MinimizeWindow(Window.GetWindow(this));
        }

        private void MaximizeButton_Click(object sender, RoutedEventArgs e)
        {
            Window.GetWindow(this).MaxHeight = SystemParameters.MaximizedPrimaryScreenHeight;
            Window.GetWindow(this).MaxWidth = SystemParameters.MaximizedPrimaryScreenWidth;
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            SystemCommands.CloseWindow(Window.GetWindow(this));
        }
	}
}
