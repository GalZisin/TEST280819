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
using System.Windows.Shapes;

namespace Targil3
{
    /// <summary>
    /// Interaction logic for Trgil3_Dispatcher.xaml
    /// </summary>
    public partial class Trgil3_Dispatcher : Window
    {
        ViewModelDispatcher vm;
        public Trgil3_Dispatcher()
        {
            InitializeComponent();
            vm = new ViewModelDispatcher();
            this.DataContext = vm;
        }

      
    }
}
