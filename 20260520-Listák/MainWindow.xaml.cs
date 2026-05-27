using Microsoft.Win32;
using System.IO;
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
                this.Refresh();
            }
        }

        private void ButtonRemoveLeft_Click(object sender, RoutedEventArgs e)
        {
            int index = this.ListboxLeft.SelectedIndex;
            if (index == -1) return;
            this.ListboxLeft.Items.RemoveAt(index);
            this.Refresh();
        }

        private void TextboxLeft_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (this.ListboxLeft != null && this.TextboxLeft != null && this.ButtonAddLeft != null)
            {
                this.ButtonAddLeft.IsEnabled = this.TextboxLeft.Text != string.Empty &&
                                               !this.ListboxLeft.Items.Contains(this.TextboxLeft.Text);
                this.Refresh();
            }
        }

        private void ListboxLeft_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.ButtonRemoveLeft != null && this.ListboxLeft != null)
            {
                this.ButtonRemoveLeft.IsEnabled = this.ListboxLeft.SelectedIndex != -1;
                this.Refresh();
            }
        }

        private void MoveLeft_Click(object sender, RoutedEventArgs e)
        {
            if(this.ListboxRight.SelectedItem is string item)
            {
                if (!this.ListboxLeft.Items.Contains(item))
                {
                    this.ListboxRight.Items.Remove(item);
                    this.ListboxLeft.Items.Add(item);
                    this.Refresh();
                }
            }
        }

        private void MoveAllLeft_Click(object sender, RoutedEventArgs e)
        {
            if (this.ListboxRight.Items.Count == 0)
            {
                return;
            }
            this.ListboxRight.Items.OfType<string>().ToList().Where(
                    value => !this.ListboxLeft.Items.Contains(value)
                ).ToList().ForEach(
                    value => {
                        this.ListboxLeft.Items.Add(value);
                        this.ListboxRight.Items.Remove(value);
                    }
                );
            this.Refresh();
        }

        private void TextboxRight_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (this.ListboxRight != null && this.TextboxRight != null && this.ButtonAddRight != null)
            {
                this.ButtonAddRight.IsEnabled = this.TextboxRight.Text != string.Empty &&
                                               !this.ListboxRight.Items.Contains(this.TextboxRight.Text);
                this.Refresh();
            }
        }

        private void ListboxRight_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.ButtonRemoveRight != null && this.ListboxRight != null)
            {
                this.ButtonRemoveRight.IsEnabled = this.ListboxRight.SelectedIndex != -1;
                this.Refresh();
            }
        }

        private void ButtonAddRight_Click(object sender, RoutedEventArgs e)
        {
            if (!this.ListboxRight.Items.Contains(this.TextboxRight.Text))
            {
                this.ListboxRight.Items.Add(this.TextboxRight.Text);
                this.TextboxRight.Text = string.Empty;
                this.ButtonAddRight.IsEnabled = false;
                this.Refresh();
            }
        }

        private void ButtonRemoveRight_Click(object sender, RoutedEventArgs e)
        {
            int index = this.ListboxRight.SelectedIndex;
            if (index == -1) return;
            this.ListboxRight.Items.RemoveAt(index);
            this.Refresh();
        }

        private void MoveRight_Click(object sender, RoutedEventArgs e)
        {
            if(this.ListboxLeft.SelectedItem is string item)
            {
                if (!this.ListboxRight.Items.Contains(item))
                {
                    this.ListboxLeft.Items.Remove(item);
                    this.ListboxRight.Items.Add(item);
                    this.Refresh();
                }
            }
        }

        private void MoveAllRight_Click(object sender, RoutedEventArgs e)
        {
            if (this.ListboxLeft.Items.Count == 0)
            {
                return;
            }
            this.ListboxLeft.Items.OfType<string>().ToList().Where(
                    value => !this.ListboxRight.Items.Contains(value)
                ).ToList().ForEach(
                    value => {
                        this.ListboxLeft.Items.Remove(value);
                        this.ListboxRight.Items.Add(value);
                    }
                );
            this.Refresh();
        }

        private void Refresh()
        {
            this.MoveRight.IsEnabled = this.ListboxLeft.SelectedIndex > -1;
            this.MoveAllRight.IsEnabled = this.ListboxLeft.Items.Count > 0;
            this.MoveLeft.IsEnabled = this.ListboxRight.SelectedIndex > -1;
            this.MoveAllLeft.IsEnabled = this.ListboxRight.Items.Count > 0;
            this.ButtonSaveLeft.IsEnabled = this.ListboxLeft.Items.Count > 0;
        }

        private void ButtonSaveLeft_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Title = "Bal oldali lista mentése";
            saveFileDialog.FileName = "bal_lista.txt";
            saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            saveFileDialog.Filter = "Szöveges fájl (*.txt)|*.txt|Minden fájl (*.*)|*.*";
            saveFileDialog.DefaultExt = "txt";
            if (saveFileDialog.ShowDialog() == true)
            {
                File.WriteAllLines(saveFileDialog.FileName, this.ListboxLeft.Items.Cast<string>());
            }
        }

        private void ButtonLoadLeft_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "Bal oldali lista betöltése";
            openFileDialog.FileName = "bal_lista.txt";
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            openFileDialog.Filter = "Szöveges fájl (*.txt)|*.txt|Minden fájl (*.*)|*.*";
            openFileDialog.DefaultExt = "txt";
            if (openFileDialog.ShowDialog() == true)
            {
                this.ListboxLeft.Items.Clear();
                File.ReadAllLines(openFileDialog.FileName).ToList().ForEach(v => this.ListboxLeft.Items.Add(v));
                this.Refresh();
            }
        }
    }
}