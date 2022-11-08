using ReactiveUI;

namespace Avalonia.schoolTimetabler.ViewModels;

public class DisciplineEditingMenuViewModel : ViewModelBase, IRoutableViewModel, IScreen
{
    public DisciplineEditingMenuViewModel(CreateSchoolProfileViewModel createSchoolProfileViewModel)
    {
       
    }
    

    public string? UrlPathSegment { get; }
    public IScreen HostScreen { get; }
    public RoutingState Router { get; }
}