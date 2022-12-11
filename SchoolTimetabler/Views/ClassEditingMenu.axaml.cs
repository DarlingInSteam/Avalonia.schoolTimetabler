using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using SchoolTimetabler.ViewModels;

namespace SchoolTimetabler.Views;

public partial class ClassEditingMenu : ReactiveUserControl<ClassEditingMenuViewModel>
{
    public ClassEditingMenu()
    {
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}