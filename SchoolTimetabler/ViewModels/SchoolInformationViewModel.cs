using System.Reactive;using Data.Repositories;using Domain.UseCases;using ReactiveUI;namespace SchoolTimetabler.ViewModels;public class SchoolInformationViewModel : ViewModelBase, IRoutableViewModel, IScreen{    private string _countClasses;    private string _countTeachers;    private string _fullNameDirector;    private string _schoolNumber;        public ReactiveCommand<Unit, Unit> ConfirmSchoolSettings { get; }        public SchoolInformationViewModel()    {        var schoolInfoInteractor = new SchoolInfoInteractor(SchoolRepository.GetInstance());                ConfirmSchoolSettings = ReactiveCommand.Create(() =>        {            schoolInfoInteractor.SchoolInfoSet(_fullNameDirector, _countClasses, _countTeachers, _schoolNumber);        });    }        public string SchoolNumber    {        set => this.RaiseAndSetIfChanged(ref _schoolNumber, value);        get => _schoolNumber;    }    public string FullNameDirector    {        set => this.RaiseAndSetIfChanged(ref _fullNameDirector, value);        get => _fullNameDirector;    }    public string CountClasses    {        set => this.RaiseAndSetIfChanged(ref _countClasses, value);        get => _countClasses;    }    public string CountTeachers    {        set => this.RaiseAndSetIfChanged(ref _countTeachers, value);        get => _countTeachers;    }    public string? UrlPathSegment { get; }    public IScreen HostScreen { get; }    public RoutingState Router { get; }}