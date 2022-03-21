using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Markup;

namespace WpfUI.Helpers;

public class RichTextBoxHelper : DependencyObject
{
    private static HashSet<Thread> _recursionProtection = new();

    public static string GetDocumentXaml(DependencyObject obj)
    {
        return (string)obj.GetValue(DocumentXamlProperty);
    }

    public static void SetDocumentXaml(DependencyObject obj, string value)
    {
        _recursionProtection.Add(Thread.CurrentThread);
        obj.SetValue(DocumentXamlProperty, value);
        _recursionProtection.Remove(Thread.CurrentThread);
    }

    public static readonly DependencyProperty DocumentXamlProperty = DependencyProperty.RegisterAttached(
        "DocumentXaml",
        typeof(string),
        typeof(RichTextBoxHelper),
        new FrameworkPropertyMetadata(
            "",
            FrameworkPropertyMetadataOptions.AffectsRender | FrameworkPropertyMetadataOptions.BindsTwoWayByDefault,
            (obj, e) => {
                if (_recursionProtection.Contains(Thread.CurrentThread))
                    return;

                RichTextBox richTextBox = (RichTextBox)obj;

                try
                {
                    MemoryStream stream = new(Encoding.UTF8.GetBytes(GetDocumentXaml(richTextBox)));
                    FlowDocument doc = (FlowDocument)XamlReader.Load(stream);
                    richTextBox.Document = doc;
                }
                catch (Exception)
                {
                    richTextBox.Document = new FlowDocument();
                }

                richTextBox.TextChanged += (obj2, e2) =>
                {
                    RichTextBox richTextBox2 = obj2 as RichTextBox;
                    if (richTextBox2 != null)
                    {
                        SetDocumentXaml(richTextBox, XamlWriter.Save(richTextBox2.Document));
                    }
                };
            }
        )
    );
}
