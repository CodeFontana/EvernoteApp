using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;

namespace WpfUI.Controls;
public class RichTextBoxEx : RichTextBox
{
    public static readonly DependencyProperty CurrentFontFamilyProperty =
                DependencyProperty.Register("CurrentFontFamily", 
                    typeof(FontFamily), 
                    typeof(RichTextBoxEx), 
                    new FrameworkPropertyMetadata(new FontFamily("Segoe UI"), 
                        FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, 
                        new PropertyChangedCallback(OnCurrentFontChanged)));

    public FontFamily CurrentFontFamily
    {
        get
        {
            return (FontFamily)GetValue(CurrentFontFamilyProperty);
        }
        set
        {
            SetValue(CurrentFontFamilyProperty, value);
        }
    }

    private static bool FontChanged = false;

    private static void OnCurrentFontChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
    { 
        FontChanged = true;
    }

    protected override void OnTextInput(TextCompositionEventArgs e)
    {
        if (FontChanged)
        {
            TextPointer textPointer = this.CaretPosition.GetInsertionPosition(LogicalDirection.Forward);
            Run run = new Run(e.Text, textPointer);
            run.FontFamily = this.CurrentFontFamily;
            this.CaretPosition = run.ElementEnd;
            FontChanged = false;
        }
        else
        {
            base.OnTextInput(e);
        }
    }
}
