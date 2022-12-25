using Domain.Entities;using Domain.Repositories;using MessageBox.Avalonia;namespace Domain.UseCases;public class ClassInteractor{    private readonly IClassesRepository<Class> _classesRepository;    public ClassInteractor(IClassesRepository<Class> classesRepository)    {        _classesRepository = classesRepository;    }    public void AddClass(string classNumber, string classSymbol)    {        var detected = false;        var msg = new List<string>();        foreach (var Class in _classesRepository.Read().ToArray())            if (Class.Symbol == classSymbol && Class.Number == classNumber)            {                var message = MessageBoxManager                    .GetMessageBoxStandardWindow("Неправильные данные",                        "Вы неправильно заполинили поля: Такой класс уже существует").Show();                detected = true;                return;            }        if ((string.IsNullOrWhiteSpace(classNumber) || string.IsNullOrWhiteSpace(classSymbol)) && detected == false)        {            var t = new List<string> { "Номер класса", "Буква класса" };            for (var index = 0; index < 2; index++)            {                if (string.IsNullOrWhiteSpace(classNumber) && index == 0) msg.Add(t[index]);                if (string.IsNullOrWhiteSpace(classSymbol) && index == 1) msg.Add(t[index]);            }            var message = MessageBoxManager                .GetMessageBoxStandardWindow("Неправильные данные",                    "Вы не заполинили одно или несколько полей:" + string.Join(", ", msg)).Show();        }        else        {            var newClass = new Class(classNumber, classSymbol);            _classesRepository.Add(newClass);        }    }    public void DelClass(Class delClass)    {        _classesRepository.Delete(delClass);    }    public List<Class> GetClasses()    {        var t = _classesRepository.Read();        return t;    }}