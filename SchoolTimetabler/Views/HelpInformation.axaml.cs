using Avalonia.Markup.Xaml;using Avalonia.ReactiveUI;using SchoolTimetabler.ViewModels;namespace SchoolTimetabler.Views;public partial class HelpInformation : ReactiveUserControl<HelpInformationViewModel>{    public HelpInformation()    {        InitializeComponent();    }    private void InitializeComponent()    {        AvaloniaXamlLoader.Load(this);    }}