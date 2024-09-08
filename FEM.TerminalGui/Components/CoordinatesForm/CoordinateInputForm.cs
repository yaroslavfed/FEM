using System.Reactive.Disposables;
using System.Reactive.Linq;
using ReactiveMarbles.ObservableEvents;
using ReactiveUI;
using Terminal.Gui;

namespace FEM.TerminalGui.Components.CoordinatesForm;

public class CoordinateInputForm : ViewBase<CoordinateInputFormViewModel>
{
    #region LifeCycle

    public CoordinateInputForm(CoordinateInputFormViewModel viewModel) : base(viewModel)
    {
        ViewModel = viewModel;

        var centerCoordinatesLabel = CenterCoordinatesLabel();
        var stepToBoundsLabel = StepToBoundsLabel(centerCoordinatesLabel);

        var xCoordinatesLabel = XCoordinatesLabel(centerCoordinatesLabel);
        var yCoordinatesLabel = YCoordinatesLabel(xCoordinatesLabel);
        var zCoordinatesLabel = ZCoordinatesLabel(yCoordinatesLabel);

        var xCenterInput = XCenterInput(xCoordinatesLabel, centerCoordinatesLabel);
        var yCenterInput = YCenterInput(yCoordinatesLabel, centerCoordinatesLabel);
        var zCenterInput = ZCenterInput(zCoordinatesLabel, centerCoordinatesLabel);


        var xStepToBoundsInput = XStepToBoundsInput(xCoordinatesLabel, stepToBoundsLabel);
        var yStepToBoundsInput = YStepToBoundsInput(yCoordinatesLabel, stepToBoundsLabel);
        var zStepToBoundsInput = ZStepToBoundsInput(zCoordinatesLabel, stepToBoundsLabel);
    }

    #endregion

    #region Methods

    #region HeaderLabels

    private Label CenterCoordinatesLabel()
    {
        var label = new Label(ViewModel?.CenterCoordinatesLabel)
        {
            X = 5,
            TextAlignment = TextAlignment.Left,
            Width = 4
        };
        Add(label);
        return label;
    }

    private Label StepToBoundsLabel(View previous)
    {
        var label = new Label(ViewModel?.StepToBoundsLabel)
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

    private TextField XCenterInput(View previousVertical, View previousHorizontal)
    {
        var fieldValue = new TextField(ViewModel?.XCenterCoordinate)
        {
            X = Pos.Left(previousHorizontal),
            Y = Pos.Top(previousVertical),
            Width = 10
        };

        ViewModel
            .WhenAnyValue(x => x.XCenterCoordinate)
            .BindTo(fieldValue, x => x.Text)
            .DisposeWith(_disposable);

        fieldValue
            .Events()
            .TextChanged
            .Select(old => fieldValue.Text)
            .DistinctUntilChanged()
            .BindTo(ViewModel, x => x.XCenterCoordinate);


        Add(fieldValue);
        return fieldValue;
    }

    private TextField YCenterInput(View previousVertical, View previousHorizontal)
    {
        var fieldValue = new TextField(ViewModel?.YCenterCoordinate)
        {
            X = Pos.Left(previousHorizontal),
            Y = Pos.Top(previousVertical),
            Width = 10
        };

        ViewModel
            .WhenAnyValue(x => x.YCenterCoordinate)
            .BindTo(fieldValue, x => x.Text)
            .DisposeWith(_disposable);

        fieldValue
            .Events()
            .TextChanged
            .Select(old => fieldValue.Text)
            .DistinctUntilChanged()
            .BindTo(ViewModel, x => x.YCenterCoordinate);


        Add(fieldValue);
        return fieldValue;
    }

    private TextField ZCenterInput(View previousVertical, View previousHorizontal)
    {
        var fieldValue = new TextField(ViewModel?.ZCenterCoordinate)
        {
            X = Pos.Left(previousHorizontal),
            Y = Pos.Top(previousVertical),
            Width = 10
        };

        ViewModel
            .WhenAnyValue(x => x.ZCenterCoordinate)
            .BindTo(fieldValue, x => x.Text)
            .DisposeWith(_disposable);

        fieldValue
            .Events()
            .TextChanged
            .Select(old => fieldValue.Text)
            .DistinctUntilChanged()
            .BindTo(ViewModel, x => x.ZCenterCoordinate);


        Add(fieldValue);
        return fieldValue;
    }

    #endregion

    #region StepToBoundsCoordinates

    private TextField XStepToBoundsInput(View previousVertical, View previousHorizontal)
    {
        var fieldValue = new TextField(ViewModel?.XStepToBounds)
        {
            X = Pos.Left(previousHorizontal),
            Y = Pos.Top(previousVertical),
            Width = 10
        };

        ViewModel
            .WhenAnyValue(x => x.XStepToBounds)
            .BindTo(fieldValue, x => x.Text)
            .DisposeWith(_disposable);

        fieldValue
            .Events()
            .TextChanged
            .Select(old => fieldValue.Text)
            .DistinctUntilChanged()
            .BindTo(ViewModel, x => x.XStepToBounds);


        Add(fieldValue);
        return fieldValue;
    }

    private TextField YStepToBoundsInput(View previousVertical, View previousHorizontal)
    {
        var fieldValue = new TextField(ViewModel?.YStepToBounds)
        {
            X = Pos.Left(previousHorizontal),
            Y = Pos.Top(previousVertical),
            Width = 10
        };

        ViewModel
            .WhenAnyValue(x => x.YStepToBounds)
            .BindTo(fieldValue, x => x.Text)
            .DisposeWith(_disposable);

        fieldValue
            .Events()
            .TextChanged
            .Select(old => fieldValue.Text)
            .DistinctUntilChanged()
            .BindTo(ViewModel, x => x.YStepToBounds);


        Add(fieldValue);
        return fieldValue;
    }

    private TextField ZStepToBoundsInput(View previousVertical, View previousHorizontal)
    {
        var fieldValue = new TextField(ViewModel?.ZStepToBounds)
        {
            X = Pos.Left(previousHorizontal),
            Y = Pos.Top(previousVertical),
            Width = 10
        };

        ViewModel
            .WhenAnyValue(x => x.ZStepToBounds)
            .BindTo(fieldValue, x => x.Text)
            .DisposeWith(_disposable);

        fieldValue
            .Events()
            .TextChanged
            .Select(old => fieldValue.Text)
            .DistinctUntilChanged()
            .BindTo(ViewModel, x => x.ZStepToBounds);


        Add(fieldValue);
        return fieldValue;
    }

    #endregion

    #endregion
}