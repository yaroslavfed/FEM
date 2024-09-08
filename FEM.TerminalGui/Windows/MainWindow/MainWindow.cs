using FEM.TerminalGui.Components.CoordinatesForm;
using Terminal.Gui;
using Attribute = Terminal.Gui.Attribute;

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
        var submitButton = SubmitButton(coordinatesForm);
        var clearButton = ClearButton(submitButton);
    }

    #endregion

    #region Methods

    private CoordinateInputForm CoordinatesPanel()
    {
        var coordinatesForm = new CoordinateInputForm(ViewModel?.CoordinateInputFormViewModel!)
        {
            AutoSize = true,
            Height = 12,
            ViewModel = ViewModel?.CoordinateInputFormViewModel
        };

        Add(coordinatesForm);
        return coordinatesForm;
    }

    private Button SubmitButton(View previous)
    {
        var submitButton = new Button(ViewModel?.SubmitButtonLabel)
        {
            X = Pos.Left(previous) + 1,
            Y = Pos.Bottom(previous),
            Width = 12
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
            Width = 12
        };

        clearButton.Clicked += () =>
            ViewModel?.ClearCommand.Execute();

        Add(clearButton);
        return clearButton;
    }

    #endregion
}