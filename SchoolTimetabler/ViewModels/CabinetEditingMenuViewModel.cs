using System;using System.Collections.Generic;using System.Collections.ObjectModel;using System.Reactive;using Data.Repositories;using Domain.Entities;using Domain.UseCases;using ReactiveUI;namespace SchoolTimetabler.ViewModels;public class CabinetEditingMenuViewModel : ViewModelBase, IRoutableViewModel, IScreen{    private int _dataGridSelectedIndex;    private readonly CabinetInteractor _cabinetInteractor;    private string _cabinetNumber;    public CabinetEditingMenuViewModel()    {        _cabinetInteractor = new CabinetInteractor(CabinetsRepository.GetInstance());        Cabinets = new ObservableCollection<Cabinet>(_cabinetInteractor.GetCabinets());        AddNewCabinet = ReactiveCommand.Create(() =>        {            _cabinetInteractor.AddCabinet(CabinetNumber);            Cabinets.Clear();            foreach (var t in _cabinetInteractor.GetCabinets())            {                Cabinets.Add(t);            }        });        DeleteCabinet = ReactiveCommand.Create(() =>        {            _cabinetInteractor.DelCabinet(Cabinets[_dataGridSelectedIndex]);            Cabinets.Remove(Cabinets[_dataGridSelectedIndex]);        });    }    public ObservableCollection<Cabinet> Cabinets { get; set; }    public ReactiveCommand<Unit, Unit> AddNewCabinet { get; }    public ReactiveCommand<Unit, Unit> DeleteCabinet { get; }    public int DataGridSelectedIndex    {        set => this.RaiseAndSetIfChanged(ref _dataGridSelectedIndex, value);        get => _dataGridSelectedIndex;    }    public string CabinetNumber    {        set => this.RaiseAndSetIfChanged(ref _cabinetNumber, value);        get => _cabinetNumber;    }    public string? UrlPathSegment { get; }    public IScreen HostScreen { get; }    public RoutingState Router { get; }}