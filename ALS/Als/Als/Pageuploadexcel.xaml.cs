using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using Excel = Microsoft.Office.Interop.Excel ;


namespace Als
{
    /// <summary>
    /// Interaction logic for Pageuploadexcel.xaml
    /// </summary>
    public partial class Pageuploadexcel : System.Windows.Controls.Page
    {
        public Pageuploadexcel()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            /*
                microsoft.win32.openfiledialog dlg = new ..

                dlg.defaultext = ".txt";

                dlg.filter = "Text document (.txt)|*.txt";


                Nullable<bool> result = dlg.ShowDialog();


                fi (result == true)

                {
                    string filename = dlg.filename;
                    filenametextbox.text = filename;
                }
             */

            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();

            
            dlg.DefaultExt = ".xls|.xlsx";
            dlg.Filter = "Excel File (.xls)|*.xls|Excel File 2007 (.xlsx)|*.xlsx";

            Nullable<bool> result = dlg.ShowDialog();
            if (result == true)
            {
                string filename = dlg.FileName;
                txtupload.Text = filename;
            }
        }

        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            string scoba = "";
            Excel.Application xlapp = new Excel.Application();
            Excel.Workbook xlworkbook = null;
            Excel.Worksheet xlworksheet = null;
            Excel.Range range = null;
            xlworkbook = xlapp.Workbooks.Open(txtupload.Text);
            try
            {
                xlworksheet = (Excel.Worksheet)xlworkbook.Worksheets.get_Item(1);
                range = xlworksheet.UsedRange;
                //for (int ccnt = 1; ccnt <= range.Columns.Count; ccnt++)
                //{
                //}

                for (int ccnt = 1; ccnt <= range.Rows.Count; ccnt++)
                {
                    scoba += ((range.Cells[ccnt,1] as Excel.Range).Value).ToString();
                    //scoba += range.Cells[ccnt,1]
                }

            }
            catch (Exception ex)
            {}
            finally { 
                xlworkbook.Close(); 
            }

            xlapp.Quit();

            MessageBox.Show(scoba);
        }
    }
}
