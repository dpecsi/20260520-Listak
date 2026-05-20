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

namespace _20260520_Listák
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void ButtonAddLeft_Click(object sender, RoutedEventArgs e)
        {
            if (!this.ListboxLeft.Items.Contains(this.TextboxLeft.Text))
            {
                this.ListboxLeft.Items.Add(this.TextboxLeft.Text);
                this.TextboxLeft.Text = string.Empty;
                this.ButtonAddLeft.IsEnabled = false;
            }
        }

        private void ButtonRemoveLeft_Click(object sender, RoutedEventArgs e)
        {
            int index = this.ListboxLeft.SelectedIndex;
            if (index == -1) return;
            this.ListboxLeft.Items.RemoveAt(index);

            //object elem = this.ListboxLeft.SelectedItem;
            //if(elem == null) return;
            //this.ListboxLeft.Items.Remove(elem);
        }

        private void TextboxLeft_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (this.ListboxLeft != null && this.TextboxLeft != null && this.ButtonAddLeft != null)
            {
                this.ButtonAddLeft.IsEnabled = this.TextboxLeft.Text != string.Empty &&
                                               !this.ListboxLeft.Items.Contains(this.TextboxLeft.Text);
            }
        }

        private void ListboxLeft_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.ButtonRemoveLeft != null && this.ListboxLeft != null)
            {
                this.ButtonRemoveLeft.IsEnabled = this.ListboxLeft.SelectedIndex != -1;
            }
        }
    }
}