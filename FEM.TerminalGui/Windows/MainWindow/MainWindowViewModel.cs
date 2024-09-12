using System.Reactive;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Runtime.Serialization;
using Client.Shared.HttpClientContext;
using Client.Shared.Services.TestingService;
using FEM.TerminalGui.Components.AdditionalParamsForm;
using FEM.TerminalGui.Components.CoordinatesForm;
using FEM.TerminalGui.Components.SplittingForm;
using NStack;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using AdditionParameters = Client.Shared.Data.AdditionParameters;
using MeshParameters = Client.Shared.Data.MeshParameters;
using SplittingParameters = Client.Shared.Data.SplittingParameters;
using TestSession = Client.Shared.Data.TestSession;

namespace FEM.TerminalGui.Windows.MainWindow;

[DataContract]
public class MainWindowViewModel : ViewModelBase
{
    #region Fields

    private readonly ITestingService _testingService;

    private readonly BehaviorSubject<FemResponse?> _response = new(null);

    #endregion

    #region LifeCycle

    public MainWindowViewModel(ITestingService testingService)
    {
        _testingService = testingService;

        SubmitCommand = ReactiveCommand.CreateFromTask(SubmitFieldsAsync);
        ClearCommand = ReactiveCommand.CreateFromTask(ClearFieldsAsync);

        _response.Subscribe(response => FemResponse = response);
    }

    #endregion

    #region Labels

    public string SubmitButtonLabel => "Submit";

    public string ClearButtonLabel => "Clear";

    #endregion

    #region Properties

    public CoordinateInputFormViewModel CoordinateInputFormViewModel { get; set; } = new();

    public SplittingInputFormViewModel SplittingInputFormViewModel { get; set; } = new();

    public AdditionalParamsFormViewModel AdditionalParamsFormViewModel { get; set; } = new();

    [Reactive, DataMember]
    public FemResponse? FemResponse { get; set; }

    #endregion

    #region Commands

    public ReactiveCommand<Unit, Unit> SubmitCommand { get; }
    public ReactiveCommand<Unit, Unit> ClearCommand { get; }

    #endregion

    #region Methods

    public async Task SubmitFieldsAsync()
    {
        var meshParameters = new MeshParameters()
        {
            XCenterCoordinate = await UStringToDouble(CoordinateInputFormViewModel.XCenterCoordinate),
            YCenterCoordinate = await UStringToDouble(CoordinateInputFormViewModel.YCenterCoordinate),
            ZCenterCoordinate = await UStringToDouble(CoordinateInputFormViewModel.ZCenterCoordinate),
            XStepToBounds = await UStringToDouble(CoordinateInputFormViewModel.XStepToBounds),
            YStepToBounds = await UStringToDouble(CoordinateInputFormViewModel.YStepToBounds),
            ZStepToBounds = await UStringToDouble(CoordinateInputFormViewModel.ZStepToBounds)
        };

        var additionParameters = new AdditionParameters()
        {
            MuCoefficient = await UStringToDouble(AdditionalParamsFormViewModel.MuCoefficient),
            GammaCoefficient = await UStringToDouble(AdditionalParamsFormViewModel.GammaCoefficient),
            BoundaryCondition = AdditionalParamsFormViewModel.BoundaryCondition
        };

        var splittingParameters = new SplittingParameters()
        {
            XSplittingCoefficient = await UStringToDouble(SplittingInputFormViewModel.XSplittingCoefficient),
            YSplittingCoefficient = await UStringToDouble(SplittingInputFormViewModel.YSplittingCoefficient),
            ZSplittingCoefficient = await UStringToDouble(SplittingInputFormViewModel.ZSplittingCoefficient),
            XMultiplyCoefficient = await UStringToDouble(SplittingInputFormViewModel.XMultiplyCoefficient),
            YMultiplyCoefficient = await UStringToDouble(SplittingInputFormViewModel.YMultiplyCoefficient),
            ZMultiplyCoefficient = await UStringToDouble(SplittingInputFormViewModel.ZMultiplyCoefficient)
        };

        var session = new TestSession()
        {
            Id = Guid.NewGuid(),
            MeshParameters = meshParameters,
            SplittingParameters = splittingParameters,
            AdditionParameters = additionParameters
        };

        var result = await _testingService.CreateSessionAsync(session);
        _response.OnNext(result);
    }

    public Task ClearFieldsAsync()
    {
        _response.OnNext(null);

        CoordinateInputFormViewModel.XCenterCoordinate = "0";
        CoordinateInputFormViewModel.YCenterCoordinate = "0";
        CoordinateInputFormViewModel.ZCenterCoordinate = "0";
        CoordinateInputFormViewModel.XStepToBounds = "1";
        CoordinateInputFormViewModel.YStepToBounds = "1";
        CoordinateInputFormViewModel.ZStepToBounds = "1";

        SplittingInputFormViewModel.XSplittingCoefficient = "1";
        SplittingInputFormViewModel.YSplittingCoefficient = "1";
        SplittingInputFormViewModel.ZSplittingCoefficient = "1";
        SplittingInputFormViewModel.XMultiplyCoefficient = "0";
        SplittingInputFormViewModel.YMultiplyCoefficient = "0";
        SplittingInputFormViewModel.ZMultiplyCoefficient = "0";

        AdditionalParamsFormViewModel.MuCoefficient = "1";
        AdditionalParamsFormViewModel.GammaCoefficient = "1";
        AdditionalParamsFormViewModel.BoundaryCondition = 0;

        return Task.CompletedTask;
    }

    private Task<double> UStringToDouble(ustring nonFormatedLine)
    {
        var line = nonFormatedLine.ToString();
        line = line?.Trim();
        line = line?.Replace('.', ',');

        double.TryParse(line, out var result);
        return Task.FromResult(result);
    }

    #endregion
}