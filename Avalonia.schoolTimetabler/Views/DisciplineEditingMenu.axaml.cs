using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace Avalonia.schoolTimetabler.Views;

public partial class DisciplineEditingMenu : UserControl
{
    public DisciplineEditingMenu()
    {
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}