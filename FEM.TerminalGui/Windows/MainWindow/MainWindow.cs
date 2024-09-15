using System.Reactive.Disposables;
using System.Reactive.Linq;
using FEM.TerminalGui.Components.AdditionalParamsForm;
using FEM.TerminalGui.Components.CoordinatesForm;
using FEM.TerminalGui.Components.SplittingForm;
using ReactiveUI;
using Terminal.Gui;

namespace FEM.TerminalGui.Windows.MainWindow;

public sealed class MainWindow : ViewBase<MainWindowViewModel>
{
    #region Fields

    private const string TITLE = "Vector FEM (Ctrl+Q to quit)";

    #endregion

    #region LifeCycle

    public MainWindow(MainWindowViewModel viewModel) : base(viewModel, TITLE)
    {
        ViewModel = viewModel;

        var coordinatesForm = CoordinatesPanel();
        var splittingForm = SplittingPanel(coordinatesForm);
        var additionalParamsForm = AdditionalParamsFormPanel(coordinatesForm);
        var submitButton = SubmitButton(additionalParamsForm);
        var clearButton = ClearButton(submitButton);
        var resultFieldLabel = ResultFieldLabel(submitButton);
    }

    #endregion

    #region Methods

    private CoordinateInputForm CoordinatesPanel()
    {
        var coordinatesForm = new CoordinateInputForm(ViewModel?.CoordinateInputFormViewModel!)
        {
            AutoSize = true,
            Height = 10,
            Width = 59,
            ViewModel = ViewModel?.CoordinateInputFormViewModel
        };

        Add(coordinatesForm);
        return coordinatesForm;
    }

    private SplittingInputForm SplittingPanel(View previous)
    {
        var splittingForm = new SplittingInputForm(ViewModel?.SplittingInputFormViewModel!)
        {
            X = Pos.Right(previous),
            Y = Pos.Top(previous),
            AutoSize = true,
            Height = 10,
            Width = 59,
            ViewModel = ViewModel?.SplittingInputFormViewModel
        };

        Add(splittingForm);
        return splittingForm;
    }

    private AdditionalParamsForm AdditionalParamsFormPanel(View previous)
    {
        var splittingForm = new AdditionalParamsForm(ViewModel?.AdditionalParamsFormViewModel!)
        {
            X = Pos.Left(previous),
            Y = Pos.Bottom(previous),
            Height = 10,
            Width = 118,
            ViewModel = ViewModel?.AdditionalParamsFormViewModel
        };

        Add(splittingForm);
        return splittingForm;
    }

    private Button SubmitButton(View previous)
    {
        var submitButton = new Button(ViewModel?.SubmitButtonLabel)
        {
            X = Pos.Left(previous),
            Y = Pos.Bottom(previous) + 10,
            Width = 10
        };

        submitButton.Clicked += () =>
            ViewModel?.SubmitCommand.Execute();

        Add(submitButton);
        return submitButton;
    }

    private Button ClearButton(View previous)
    {
        var clearButton = new Button(ViewModel?.ClearButtonLabel)
        {
            X = Pos.Right(previous),
            Y = Pos.Top(previous),
            Width = 10
        };

        clearButton.Clicked += () =>
            ViewModel?.ClearCommand.Execute();

        Add(clearButton);
        return clearButton;
    }

    private Label ResultFieldLabel(View previous)
    {
        var resultField = new Label("THE RESULT IS FOUND")
        {
            X = Pos.Left(previous),
            Y = Pos.Bottom(previous) + 1,
            Width = 20,
            Visible = false
        };

        ViewModel?
            .WhenAnyValue(model => model.FemResponse)
            .Select(response => response is not null)
            .BindTo(resultField, label => label.Visible)
            .DisposeWith(_disposable);

        Add(resultField);
        return resultField;
    }

    #endregion
}