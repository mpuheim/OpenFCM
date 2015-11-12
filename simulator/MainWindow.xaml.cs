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

namespace simulator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            
            /*TESTING CODE*/
            libfcm.Functions.PiecewiseLinear f = new libfcm.Functions.PiecewiseLinear();
            f.set("0;0 2;2 4;0");
            libfcm.Functions.PiecewiseLinear i = (libfcm.Functions.PiecewiseLinear) f.getInverse();
            MessageBox.Show(f.get() + " | " + i.get()
                            + System.Environment.NewLine +
                            f.piece.Count.ToString() + " | " + i.piece.Count.ToString());
            MessageBox.Show(f.evaluate(5).ToString());
            //terminate application
            Environment.Exit(-1);
            /*END OF TESTING CODE*/
        }
    }
}
