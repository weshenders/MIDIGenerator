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
    public class ComboboxItem
    {
        public string Text { get; set; }
        public int Value { get; set; }

        public override string ToString()
        {
            return Text;
        }
    }
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        MIDIFile midiFile = new MIDIFile();
        public MainWindow()
        {
            InitializeComponent();
            ComboboxItem piano = new ComboboxItem();
            ComboboxItem organ = new ComboboxItem();
            ComboboxItem guitar = new ComboboxItem();
            ComboboxItem drums = new ComboboxItem();
            ComboboxItem none = new ComboboxItem();
            none.Text = "None";
            none.Value = 0x00;
            piano.Text = "Acoustic Grand Piano";
            piano.Value = 0x01;
            organ.Text = "Rock Organ";
            organ.Value = 0x13;
            guitar.Text = "Electric Guitar";
            guitar.Value = 0x1c;
            drums.Text = "Steel Drums";
            drums.Value = 0x73;
            
            ComboboxItem[] values = new[] {none, piano, organ, guitar, drums};
            foreach (ComboboxItem value in values)
            {
                this.cmb_Insturment1.Items.Add(value);
                this.cmb_Insturment2.Items.Add(value);
                this.cmb_Insturment3.Items.Add(value);
            }
        }

        private void btn_Generate_Click(object sender, RoutedEventArgs e)
        {
            midiFile.NewTrack(30, this.cmb_Key.Text, ((ComboboxItem)this.cmb_Insturment1.SelectedItem).Value, 0xC0, 0x90);
            midiFile.NewTrack(30, this.cmb_Key.Text, ((ComboboxItem)this.cmb_Insturment2.SelectedItem).Value, 0xC1, 0x91);
            midiFile.NewTrack(30, this.cmb_Key.Text, ((ComboboxItem)this.cmb_Insturment3.SelectedItem).Value, 0xC2, 0x92);

            midiFile.WriteMIDIToFile();
        }
    }
}
