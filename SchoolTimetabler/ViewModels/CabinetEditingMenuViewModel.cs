using System.Collections.ObjectModel;using System.Reactive;using Data.FakeDataBase;using Data.Models;using ReactiveUI;namespace SchoolTimetabler.ViewModels;public class CabinetEditingMenuViewModel : ViewModelBase, IRoutableViewModel, IScreen{    private int _dataGridSelectedIndex;    private readonly FDataBaseCabinets _storage;    public CabinetEditingMenuViewModel()    {        _storage = FDataBaseCabinets.GetInstance();        Cabinets = new ObservableCollection<SchoolCabinet>(_storage.SchoolCabinets);        AddNewCabinet = ReactiveCommand.Create(() =>        {            var schoolCabinet = new SchoolCabinet("Новый кабинет");            _storage.AddCabinet(schoolCabinet);            Cabinets.Add(schoolCabinet);        });        DeleteCabinet = ReactiveCommand.Create(() =>        {            _storage.DeleteCabinet(_dataGridSelectedIndex);            Cabinets.Remove(Cabinets[_dataGridSelectedIndex]);        });    }    public ObservableCollection<SchoolCabinet> Cabinets { get; set; }    public ReactiveCommand<Unit, Unit> AddNewCabinet { get; }    public ReactiveCommand<Unit, Unit> DeleteCabinet { get; }    public int DataGridSelectedIndex    {        set => this.RaiseAndSetIfChanged(ref _dataGridSelectedIndex, value);        get => _dataGridSelectedIndex;    }    public string? UrlPathSegment { get; }    public IScreen HostScreen { get; }    public RoutingState Router { get; }}