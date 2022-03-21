using DataAccessLibrary.Entities;
using System;
using System.Windows;
using System.Windows.Controls;

namespace WpfUI.Controls;

public partial class NotebookControl : UserControl
{
    public Notebook Notebook
    {
        get { return (Notebook)GetValue(NotebookProperty); }
        set { SetValue(NotebookProperty, value); }
    }

    public static readonly DependencyProperty NotebookProperty =
        DependencyProperty.Register("Notebook", typeof(Notebook), typeof(NotebookControl), new PropertyMetadata(null, SetValues));

    private static void SetValues(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        NotebookControl notebookControl = (NotebookControl)d;

        if (notebookControl != null)
        {
            notebookControl.DataContext = notebookControl.Notebook;
        }
    }

    public NotebookControl()
    {
        InitializeComponent();
    }
}
