using System.Collections.ObjectModel;using System.Reactive;using Data.FakeDataBase;using ReactiveUI;namespace SchoolTimetabler.ViewModels;public class CabinetEditingMenuViewModel : ViewModelBase, IRoutableViewModel, IScreen{    public ObservableCollection<Data.Models.SchoolCabinet> Cabinets { get; set; }    public ReactiveCommand<Unit, Unit> AddNewCabinet { get; }    public ReactiveCommand<Unit, Unit> DeleteCabinet { get; }    private FDataBaseCabinets _storage;    private int _dataGridSelectedIndex;        public CabinetEditingMenuViewModel()    {        _storage = FDataBaseCabinets.GetInstance();        Cabinets = new ObservableCollection<Data.Models.SchoolCabinet>(_storage.SchoolCabinets);                AddNewCabinet = ReactiveCommand.Create(() =>        {            var schoolCabinet = new Data.Models.SchoolCabinet("Новый кабинет");            _storage.AddCabinet(schoolCabinet);            Cabinets.Add(schoolCabinet);        });        DeleteCabinet = ReactiveCommand.Create(() =>        {            _storage.DeleteCabinet(_dataGridSelectedIndex);            Cabinets.Remove(Cabinets[_dataGridSelectedIndex]);        });    }        public int DataGridSelectedIndex    {        set => this.RaiseAndSetIfChanged(ref _dataGridSelectedIndex, value);        get => _dataGridSelectedIndex;    }        public string? UrlPathSegment { get; }    public IScreen HostScreen { get; }    public RoutingState Router { get; }}