using System.Reactive.Disposables;
using System.Reactive.Linq;
using ReactiveMarbles.ObservableEvents;
using ReactiveUI;
using Terminal.Gui;

namespace FEM.TerminalGui.Components.SplittingForm;

public class SplittingInputForm : ViewBase<SplittingInputFormViewModel>
{
    #region Fields

    private const string TITLE = "Дробление";

    #endregion

    #region LifeCycle

    public SplittingInputForm(SplittingInputFormViewModel viewModel) : base(viewModel, TITLE)
    {
        ViewModel = viewModel;

        var centerCoordinatesLabel = SplittingCoefficientLabel();
        var stepToBoundsLabel = MultiplyCoefficientLabel(centerCoordinatesLabel);

        var xCoordinatesLabel = XCoordinatesLabel(centerCoordinatesLabel);
        var yCoordinatesLabel = YCoordinatesLabel(xCoordinatesLabel);
        var zCoordinatesLabel = ZCoordinatesLabel(yCoordinatesLabel);

        var xCenterInput = XSplittingCoefficient(xCoordinatesLabel, centerCoordinatesLabel);
        var yCenterInput = YSplittingCoefficient(yCoordinatesLabel, centerCoordinatesLabel);
        var zCenterInput = ZSplittingCoefficient(zCoordinatesLabel, centerCoordinatesLabel);


        var xStepToBoundsInput = XMultiplyCoefficient(xCoordinatesLabel, stepToBoundsLabel);
        var yStepToBoundsInput = YMultiplyCoefficient(yCoordinatesLabel, stepToBoundsLabel);
        var zStepToBoundsInput = ZMultiplyCoefficient(zCoordinatesLabel, stepToBoundsLabel);
    }

    #endregion

    #region Methods

    #region HeaderLabels

    private Label SplittingCoefficientLabel()
    {
        var label = new Label(ViewModel?.SplittingCoefficientLabel)
        {
            X = 5,
            TextAlignment = TextAlignment.Left,
            Width = 4
        };
        Add(label);
        return label;
    }

    private Label MultiplyCoefficientLabel(View previous)
    {
        var label = new Label(ViewModel?.MultiplyCoefficientLabel)
        {
            X = Pos.Right(previous) + 3,
            Y = Pos.Top(previous),
            TextAlignment = TextAlignment.Left,
        };
        Add(label);
        return label;
    }

    #endregion

    #region CoordinatesLabels

    private Label XCoordinatesLabel(View previous)
    {
        var label = new Label("OX:")
        {
            Y = Pos.Top(previous) + 2,
            TextAlignment = TextAlignment.Left,
            Width = 4
        };
        Add(label);
        return label;
    }

    private Label YCoordinatesLabel(View previous)
    {
        var label = new Label("OY:")
        {
            Y = Pos.Top(previous) + 2,
            TextAlignment = TextAlignment.Left,
            Width = 4
        };
        Add(label);
        return label;
    }

    private Label ZCoordinatesLabel(View previous)
    {
        var label = new Label("OZ:")
        {
            Y = Pos.Top(previous) + 2,
            TextAlignment = TextAlignment.Left,
            Width = 4
        };
        Add(label);
        return label;
    }

    #endregion

    #region CenterCoordinates

    private TextField XSplittingCoefficient(View previousVertical, View previousHorizontal)
    {
        var fieldValue = new TextField(ViewModel?.XSplittingCoefficient)
        {
            X = Pos.Left(previousHorizontal),
            Y = Pos.Top(previousVertical),
            Width = 10
        };

        ViewModel
            .WhenAnyValue(x => x.XSplittingCoefficient)
            .BindTo(fieldValue, x => x.Text)
            .DisposeWith(_disposable);

        fieldValue
            .Events()
            .TextChanged
            .Select(old => fieldValue.Text)
            .DistinctUntilChanged()
            .BindTo(ViewModel, x => x.XSplittingCoefficient);


        Add(fieldValue);
        return fieldValue;
    }

    private TextField YSplittingCoefficient(View previousVertical, View previousHorizontal)
    {
        var fieldValue = new TextField(ViewModel?.YSplittingCoefficient)
        {
            X = Pos.Left(previousHorizontal),
            Y = Pos.Top(previousVertical),
            Width = 10
        };

        ViewModel
            .WhenAnyValue(x => x.YSplittingCoefficient)
            .BindTo(fieldValue, x => x.Text)
            .DisposeWith(_disposable);

        fieldValue
            .Events()
            .TextChanged
            .Select(old => fieldValue.Text)
            .DistinctUntilChanged()
            .BindTo(ViewModel, x => x.YSplittingCoefficient);


        Add(fieldValue);
        return fieldValue;
    }

    private TextField ZSplittingCoefficient(View previousVertical, View previousHorizontal)
    {
        var fieldValue = new TextField(ViewModel?.ZSplittingCoefficient)
        {
            X = Pos.Left(previousHorizontal),
            Y = Pos.Top(previousVertical),
            Width = 10
        };

        ViewModel
            .WhenAnyValue(x => x.ZSplittingCoefficient)
            .BindTo(fieldValue, x => x.Text)
            .DisposeWith(_disposable);

        fieldValue
            .Events()
            .TextChanged
            .Select(old => fieldValue.Text)
            .DistinctUntilChanged()
            .BindTo(ViewModel, x => x.ZSplittingCoefficient);


        Add(fieldValue);
        return fieldValue;
    }

    #endregion

    #region StepToBoundsCoordinates

    private TextField XMultiplyCoefficient(View previousVertical, View previousHorizontal)
    {
        var fieldValue = new TextField(ViewModel?.XMultiplyCoefficient)
        {
            X = Pos.Left(previousHorizontal),
            Y = Pos.Top(previousVertical),
            Width = 10
        };

        ViewModel
            .WhenAnyValue(x => x.XMultiplyCoefficient)
            .BindTo(fieldValue, x => x.Text)
            .DisposeWith(_disposable);

        fieldValue
            .Events()
            .TextChanged
            .Select(old => fieldValue.Text)
            .DistinctUntilChanged()
            .BindTo(ViewModel, x => x.XMultiplyCoefficient);


        Add(fieldValue);
        return fieldValue;
    }

    private TextField YMultiplyCoefficient(View previousVertical, View previousHorizontal)
    {
        var fieldValue = new TextField(ViewModel?.YMultiplyCoefficient)
        {
            X = Pos.Left(previousHorizontal),
            Y = Pos.Top(previousVertical),
            Width = 10
        };

        ViewModel
            .WhenAnyValue(x => x.YMultiplyCoefficient)
            .BindTo(fieldValue, x => x.Text)
            .DisposeWith(_disposable);

        fieldValue
            .Events()
            .TextChanged
            .Select(old => fieldValue.Text)
            .DistinctUntilChanged()
            .BindTo(ViewModel, x => x.YMultiplyCoefficient);


        Add(fieldValue);
        return fieldValue;
    }

    private TextField ZMultiplyCoefficient(View previousVertical, View previousHorizontal)
    {
        var fieldValue = new TextField(ViewModel?.ZMultiplyCoefficient)
        {
            X = Pos.Left(previousHorizontal),
            Y = Pos.Top(previousVertical),
            Width = 10
        };

        ViewModel
            .WhenAnyValue(x => x.ZMultiplyCoefficient)
            .BindTo(fieldValue, x => x.Text)
            .DisposeWith(_disposable);

        fieldValue
            .Events()
            .TextChanged
            .Select(old => fieldValue.Text)
            .DistinctUntilChanged()
            .BindTo(ViewModel, x => x.ZMultiplyCoefficient);


        Add(fieldValue);
        return fieldValue;
    }

    #endregion

    #endregion
}