using ConsistencyCalculator.Desktop.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ConsistencyCalculator.Desktop.Views
{
    /// <summary>
    /// Interação lógica para DeckEditorView.xam
    /// </summary>
    public partial class DeckEditorView : UserControl
    {
        public DeckEditorView()
        {
            InitializeComponent();
        }
        private static readonly Regex _validTagRegex = new Regex("^[a-zA-Z0-9 ,_-]+$");

        private void DataGrid_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            if (e.Row.Item is CardEntryViewModel vm &&
                e.Column.Header?.ToString() == "Tags")
            {
                vm.CommitTags();
            }
        }

        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !_validTagRegex.IsMatch(e.Text);
        }

        private void TextBox_Pasting(object sender, DataObjectPastingEventArgs e)
        {
            if (!e.DataObject.GetDataPresent(typeof(string)))
            {
                e.CancelCommand();
                return;
            }

            var text = (string)e.DataObject.GetData(typeof(string))!;
            if (!_validTagRegex.IsMatch(text))
                e.CancelCommand();
        }
    }
}
