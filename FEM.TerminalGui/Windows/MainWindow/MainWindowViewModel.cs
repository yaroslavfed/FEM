using System.Reactive;
using System.Runtime.Serialization;
using FEM.TerminalGui.Components.AdditionalParamsForm;
using FEM.TerminalGui.Components.CoordinatesForm;
using FEM.TerminalGui.Components.SplittingForm;
using FEM.TerminalGui.Data;
using NStack;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace FEM.TerminalGui.Windows.MainWindow;

[DataContract]
public class MainWindowViewModel : ViewModelBase
{
    #region LifeCycle

    public MainWindowViewModel()
    {
        SubmitCommand = ReactiveCommand.CreateFromTask(SubmitFieldsAsync);
        ClearCommand = ReactiveCommand.CreateFromTask(ClearFieldsAsync);
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

    #endregion

    #region Commands

    public ReactiveCommand<Unit, Unit> SubmitCommand { get; }
    public ReactiveCommand<Unit, Unit> ClearCommand { get; }

    #endregion

    #region Methods

    public async Task SubmitFieldsAsync()
    {
        var mesh = new Mesh
        {
            XCenterCoordinate = await UStringToDouble(CoordinateInputFormViewModel.XCenterCoordinate),
            YCenterCoordinate = await UStringToDouble(CoordinateInputFormViewModel.YCenterCoordinate),
            ZCenterCoordinate = await UStringToDouble(CoordinateInputFormViewModel.ZCenterCoordinate),
            XStepToBounds = await UStringToDouble(CoordinateInputFormViewModel.XStepToBounds),
            YStepToBounds = await UStringToDouble(CoordinateInputFormViewModel.YStepToBounds),
            ZStepToBounds = await UStringToDouble(CoordinateInputFormViewModel.ZStepToBounds)
        };
    }

    public Task ClearFieldsAsync()
    {
        CoordinateInputFormViewModel.XCenterCoordinate = "0";
        CoordinateInputFormViewModel.YCenterCoordinate = "0";
        CoordinateInputFormViewModel.ZCenterCoordinate = "0";
        CoordinateInputFormViewModel.XStepToBounds = "0";
        CoordinateInputFormViewModel.YStepToBounds = "0";
        CoordinateInputFormViewModel.ZStepToBounds = "0";

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