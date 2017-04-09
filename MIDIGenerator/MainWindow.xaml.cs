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

namespace MIDIGenerator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        MIDIFile midiFile = new MIDIFile();
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btn_Generate_Click(object sender, RoutedEventArgs e)
        {
            midiFile.NewTrack(30, this.cmb_Key.Text);
        }
    }
}
