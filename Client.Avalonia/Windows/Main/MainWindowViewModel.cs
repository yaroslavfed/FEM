using System;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Threading.Tasks;
using Client.Avalonia.Components.Tabs.AdditionalParametersTab;
using Client.Avalonia.Components.Tabs.CoordinatesParametersTab;
using Client.Avalonia.Components.Tabs.SplittingParametersTab;
using Client.Avalonia.Extensions;
using Client.Avalonia.ViewModels;
using Client.Shared.Data;
using Client.Shared.HttpClientContext;
using Client.Shared.Services.ReportService;
using Client.Shared.Services.TestingService;
using ReactiveUI;
using AdditionParameters = Client.Shared.Data.AdditionParameters;
using MeshParameters = Client.Shared.Data.MeshParameters;
using SplittingParameters = Client.Shared.Data.SplittingParameters;
using TestSession = Client.Shared.Data.TestSession;

namespace Client.Avalonia.Windows.Main;

public class MainWindowViewModel : ViewModelBase, IScreen
{

    #region MyRegion

    private readonly ITestingService _testingService;
    private readonly IReportService  _reportService;

    private readonly BehaviorSubject<FemResponse?> _response = new(null);

    #endregion

    #region LifeCycle

    public MainWindowViewModel(
        ITestingService testingService,
        IReportService reportService,
        CoordinatesParametersTabViewModel coordinatesParametersTabViewModel,
        SplittingParametersTabViewModel splittingParametersTabViewModel,
        AdditionalParametersTabViewModel additionalParametersTabViewModel
    )
    {
        _testingService = testingService;
        CoordinatesParametersTabViewModel = coordinatesParametersTabViewModel;
        SplittingParametersTabViewModel = splittingParametersTabViewModel;
        AdditionalParametersTabViewModel = additionalParametersTabViewModel;
        _reportService = reportService;

        SubmitCommand = ReactiveCommand.CreateFromTask(SubmitFieldsAsync);
        ClearCommand = ReactiveCommand.CreateFromTask(ClearFieldsAsync);
        DownloadResultCommand = ReactiveCommand.CreateFromTask(DownloadResultAsync);

        Result = _response
                 .Where(response => response is not null)
                 .DistinctUntilChanged(response => response?.Id)
                 .Select(response => _testingService.GetSessionResultAsync(response!.Id))
                 .Switch()!;

        IsResultReceived = _response.Select(response => response is not null);
    }

    #endregion

    #region Labels

    public string CoordinatesParametersTabLabel { get; set; } = "Координаты фигуры";

    public string SplittingParametersTabLabel { get; set; } = "Дробление";

    public string AdditionalParametersTabLabel { get; set; } = "Доп. параметры";

    #endregion

    #region Properties

    public RoutingState Router { get; set; } = new();

    public IObservable<TestResult> Result { get; }

    public IObservable<bool> IsResultReceived { get; }

    public CoordinatesParametersTabViewModel CoordinatesParametersTabViewModel { get; }

    public SplittingParametersTabViewModel SplittingParametersTabViewModel { get; }

    public AdditionalParametersTabViewModel AdditionalParametersTabViewModel { get; }

    #endregion

    #region Commands

    public ReactiveCommand<Unit, Unit> SubmitCommand { get; }

    public ReactiveCommand<Unit, Unit> ClearCommand { get; }

    public ReactiveCommand<Unit, Unit> DownloadResultCommand { get; }

    #endregion

    #region Methods

    private async Task SubmitFieldsAsync()
    {
        _response.OnNext(null);

        var meshParameters = new MeshParameters
        {
            XCenterCoordinate = CoordinatesParametersTabViewModel.XCenterCoordinate,
            YCenterCoordinate = CoordinatesParametersTabViewModel.YCenterCoordinate,
            ZCenterCoordinate = CoordinatesParametersTabViewModel.ZCenterCoordinate,
            XStepToBounds = CoordinatesParametersTabViewModel.XStepToBounds,
            YStepToBounds = CoordinatesParametersTabViewModel.YStepToBounds,
            ZStepToBounds = CoordinatesParametersTabViewModel.ZStepToBounds
        };

        var additionParameters = new AdditionParameters
        {
            MuCoefficient = AdditionalParametersTabViewModel.MuCoefficient,
            GammaCoefficient = AdditionalParametersTabViewModel.GammaCoefficient,
            BoundaryCondition = AdditionalParametersTabViewModel.BoundaryConditions
                                                                .First(condition => condition.IsBoundarySelected)
                                                                .BoundaryNumber
        };

        var splittingParameters = new SplittingParameters
        {
            XSplittingCoefficient = SplittingParametersTabViewModel.XSplittingCoefficient,
            YSplittingCoefficient = SplittingParametersTabViewModel.YSplittingCoefficient,
            ZSplittingCoefficient = SplittingParametersTabViewModel.ZSplittingCoefficient,
            XMultiplyCoefficient = SplittingParametersTabViewModel.XMultiplyCoefficient,
            YMultiplyCoefficient = SplittingParametersTabViewModel.YMultiplyCoefficient,
            ZMultiplyCoefficient = SplittingParametersTabViewModel.ZMultiplyCoefficient
        };

        var session = new TestSession
        {
            Id = Guid.NewGuid(),
            MeshParameters = meshParameters,
            SplittingParameters = splittingParameters,
            AdditionParameters = additionParameters
        };

        var result = await _testingService.CreateSessionAsync(session);
        _response.OnNext(result);
    }

    private Task ClearFieldsAsync()
    {
        CoordinatesParametersTabViewModel.XCenterCoordinate = 0;
        CoordinatesParametersTabViewModel.YCenterCoordinate = 0;
        CoordinatesParametersTabViewModel.ZCenterCoordinate = 0;
        CoordinatesParametersTabViewModel.XStepToBounds = 1;
        CoordinatesParametersTabViewModel.YStepToBounds = 1;
        CoordinatesParametersTabViewModel.ZStepToBounds = 1;

        SplittingParametersTabViewModel.XSplittingCoefficient = 1;
        SplittingParametersTabViewModel.YSplittingCoefficient = 1;
        SplittingParametersTabViewModel.ZSplittingCoefficient = 1;
        SplittingParametersTabViewModel.XMultiplyCoefficient = 0;
        SplittingParametersTabViewModel.YMultiplyCoefficient = 0;
        SplittingParametersTabViewModel.ZMultiplyCoefficient = 0;

        AdditionalParametersTabViewModel.MuCoefficient = 1;
        AdditionalParametersTabViewModel.GammaCoefficient = 1;

        foreach (var condition in AdditionalParametersTabViewModel.BoundaryConditions)
            condition.IsBoundarySelected = false;

        return Task.CompletedTask;
    }

    private async Task DownloadResultAsync()
    {
        if (_response.Value is null)
            return;

        _reportService.GenerateReportAsync(await Result.Value());
    }

    #endregion

}