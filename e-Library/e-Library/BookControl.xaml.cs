using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace e_Library
{
    /// <summary>
    /// Interaction logic for BookControl.xaml
    /// </summary>
    public partial class BookControl : UserControl
    {
        public BookControl()
        {
            InitializeComponent();
         
        }
        public void groupBoxText(String name)
        {
            headerGB.Content = name;
        }
        public void setLabelText(String avtor,String red,String year, String pages, String category, String limit)
        {
            labelAvtor.Content = avtor;
            LabelRed.Content = red;
            labelYear.Content = year;
            labelPages.Content = pages;
            labelCategory.Content = category;
            labelLimit.Content = limit;
         
        }
        public void Image_MouseEnter(object sender, MouseEventArgs e)
        {
            MessageBox.Show("This message");
        }

        private void ImageBook_TouchDown(object sender, TouchEventArgs e)
        {
            MessageBox.Show("This message");
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("This message");
        }
    }
}
