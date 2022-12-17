using Data.FakeDataBase;
using ReactiveUI;

namespace SchoolTimetabler.ViewModels;

public class CreateTimetableViewModel : ViewModelBase, IRoutableViewModel, IScreen
{
    public CreateTimetableViewModel(IScreen hostScreen, FDataBaseTimetable storageTimetable)
    {
        HostScreen = hostScreen;
    }

    public string? UrlPathSegment { get; }
    public IScreen HostScreen { get; }
    public RoutingState Router { get; }
}