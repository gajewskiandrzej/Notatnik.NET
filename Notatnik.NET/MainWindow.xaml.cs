using System.Windows;
using Microsoft.Win32;
using System.IO;

namespace Notatnik.NET
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private OpenFileDialog openFileDialog;
        private SaveFileDialog saveFileDialog;

        private string traceFile = null;

        public MainWindow()
        {
            InitializeComponent();

            openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "Wybierz plik tekstowy";
            openFileDialog.DefaultExt = "txt";
            openFileDialog.Filter = "Pliki tekstowe (*.txt) | *.txt|Pliki XML" +
                "(*.xml)|*.xml|Pliki źródłowe (*.cs)|*.cs|Wszystkie pliki(*.*)|*.*";
            openFileDialog.FilterIndex = 1;

            saveFileDialog = new SaveFileDialog();
            saveFileDialog.Title = "Zapisz plik tekstowy";
            saveFileDialog.DefaultExt = "txt";
            saveFileDialog.Filter = openFileDialog.Filter;
            saveFileDialog.FilterIndex = 1;
        }
        private void MenuItem_Open_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(traceFile))
            {
                openFileDialog.InitialDirectory = Path.GetDirectoryName(traceFile);
                openFileDialog.FileName = Path.GetFileName(traceFile);
            }
            bool? result = openFileDialog.ShowDialog();
            if(result.HasValue && result.Value)
            {
                traceFile = openFileDialog.FileName;
                textBox.Text = File.ReadAllText(traceFile);
                statusBarText.Text = Path.GetFileName(traceFile);
            }
        }

        private void MenuItem_SaveAs_Click(object sender, RoutedEventArgs e)
        {
            if(!string.IsNullOrWhiteSpace(traceFile))
            {
                saveFileDialog.InitialDirectory = Path.GetDirectoryName(traceFile);
                saveFileDialog.FileName = Path.GetFileName(traceFile);
            }
            bool? result = saveFileDialog.ShowDialog();
            if(result.HasValue && result.Value)
            {
                traceFile = saveFileDialog.FileName;
                File.WriteAllText(traceFile, textBox.Text);
                statusBarText.Text = Path.GetFileName(traceFile);
            }
        }

        private void MenuItem_Save_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(traceFile))
                File.WriteAllText(traceFile, textBox.Text);
            else
                MenuItem_SaveAs_Click(sender, e);
        }
    }
}
