using Avalonia.Markup.Xaml;using Avalonia.ReactiveUI;using SchoolTimetabler.ViewModels;namespace SchoolTimetabler.Views;public partial class UserInformation : ReactiveUserControl<UserInformationViewModel>{    public UserInformation()    {        InitializeComponent();    }    private void InitializeComponent()    {        AvaloniaXamlLoader.Load(this);    }}