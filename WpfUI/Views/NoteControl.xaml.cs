using DataAccessLibrary.Entities;
using System.Windows;
using System.Windows.Controls;

namespace WpfUI.Views;

public partial class NoteControl : UserControl
{
    public Note Note
    {
        get { return (Note)GetValue(NoteProperty); }
        set { SetValue(NoteProperty, value); }
    }

    public static readonly DependencyProperty NoteProperty =
        DependencyProperty.Register("Note", typeof(Note), typeof(NoteControl), new PropertyMetadata(null, SetValues));

    private static void SetValues(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        NoteControl noteControl = (NoteControl)d;

        if (noteControl != null)
        {
            noteControl.DataContext = noteControl.Note;
        }
    }

    public NoteControl()
    {
        InitializeComponent();
    }
}
