using DataAccessLibrary.Entities;
using System.Windows;
using System.Windows.Controls;

namespace WpfUI.Controls;

public partial class NotebookControl : UserControl
{
    public NotebookControl()
    {
        InitializeComponent();
    }

    public Notebook Notebook
    {
        get { return (Notebook)GetValue(NotebookProperty); }
        set { SetValue(NotebookProperty, value); }
    }

    public static readonly DependencyProperty NotebookProperty =
        DependencyProperty.Register("Notebook", typeof(Notebook), typeof(NotebookControl), new PropertyMetadata(null));
}
