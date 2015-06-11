using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Windows.Controls;
using System.Windows;

namespace Als
{   

    public class Visuals
    {
        
        public static bool FreezeGridAndShowErrorToGrid(ref Grid targetGrid, string errorText,
            System.Windows.Media.Brush backGroundBrush, string messageDetails)
        {
            if (IsFrozen(ref targetGrid))
            {
                return ReplaceErrorText(ref targetGrid, errorText,backGroundBrush,messageDetails);
            }
            else
            {
                try
                {
                    Grid grdWait = new Grid();

                    TextBlock txt = new TextBlock();
                    txt.Text = errorText;
                    txt.FontSize = 28;
                    txt.Foreground = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Colors.White);
                    txt.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                    txt.VerticalAlignment = System.Windows.VerticalAlignment.Top;
                    txt.Margin = new Thickness(10, 50, 0, 0);

                    TextBlock messageDet = new TextBlock();
                    messageDet.Text = messageDetails;
                    messageDet.FontSize = 12;
                    messageDet.Width = 350;
                    messageDet.TextWrapping = TextWrapping.WrapWithOverflow;
                    messageDet.FontWeight = FontWeights.UltraBold;
                    messageDet.Foreground = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Colors.White);
                    messageDet.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                    messageDet.VerticalAlignment = System.Windows.VerticalAlignment.Top;
                    messageDet.Margin = new Thickness(5, 90, 0, 0);

                    Border brdWait = new Border();
                    brdWait.Child = grdWait;
                    brdWait.Name = "brdFreeze";
                    grdWait.Children.Add(txt);
                    grdWait.Children.Add(messageDet);
                    if (backGroundBrush == null)
                    {
                        System.Windows.Media.Color clr = new System.Windows.Media.Color();
                        clr.R = 160;
                        clr.G = 68;
                        clr.B = 68;
                        clr.A = 162;
                        brdWait.Background = new System.Windows.Media.SolidColorBrush(clr);
                    }
                    else
                    {
                        brdWait.Background = backGroundBrush;
                    }
                    targetGrid.Children.Add(brdWait);

                    return true;
                }
                catch { return false; }
            }
        }

        public static bool UnFreezeGrid(ref Grid targetGrid)
        {
            try
            {
                System.Windows.Media.Color clr = new System.Windows.Media.Color();
                clr.R = 160;
                clr.G = 68;
                clr.B = 68;
                clr.A = 162;

                if ((targetGrid.Children[targetGrid.Children.Count - 1].GetType() == typeof(Border)) &&
                    (((Border)targetGrid.Children[targetGrid.Children.Count - 1]).Name ==
                    "brdFreeze"))
                {
                    targetGrid.Children.RemoveAt(targetGrid.Children.Count - 1);
                    return true;
                }
                else { return false; }
            }
            catch { return false; }
        }

        private static bool IsFrozen(ref Grid targetGrid)
        {
            try
            {
                if ((targetGrid.Children[targetGrid.Children.Count - 1].GetType() == typeof(Border)) &&
                        (((Border)targetGrid.Children[targetGrid.Children.Count - 1]).Name ==
                        "brdFreeze"))
                {
                    return true;
                }
                else return false;
            }
            catch { return false; }
        }

        private static bool ReplaceErrorText(ref Grid targetGrid, string newText, 
            System.Windows.Media.Brush backGroundBrush, string newMessageDetails)
        {
            try
            {
                if (IsFrozen(ref targetGrid))
                {
                    ((TextBlock)((Grid)((Border)targetGrid.Children[targetGrid.Children.Count - 1]).Child).Children[
                    ((Grid)((Border)targetGrid.Children[targetGrid.Children.Count - 1]).Child).Children.Count - 1]).Text = newMessageDetails;

                    ((TextBlock)((Grid)((Border)targetGrid.Children[targetGrid.Children.Count - 1]).Child).Children[
                    ((Grid)((Border)targetGrid.Children[targetGrid.Children.Count - 1]).Child).Children.Count - 2]).Text = newText;

                    return true;
                }
                else
                    return false;
            }
            catch { return false; }
        }

    }
}
