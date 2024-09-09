using System.Diagnostics;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using NStack;
using ReactiveMarbles.ObservableEvents;
using ReactiveUI;
using Terminal.Gui;

namespace FEM.TerminalGui.Components.AdditionalParamsForm;

public class AdditionalParamsForm : ViewBase<AdditionalParamsFormViewModel>
{
    #region Fields

    private const string TITLE = "Дополнительные параметры";

    #endregion

    #region LifeCycle

    public AdditionalParamsForm(AdditionalParamsFormViewModel viewModel) : base(viewModel, TITLE)
    {
        ViewModel = viewModel;

        var muLabel = MuLabel();
        var gammaLabel = GammaLabel(muLabel);
        var boundaryLabelLabel = BoundaryLabel(gammaLabel);

        var xCenterInput = MuCoefficient(muLabel);
        var yCenterInput = GammaCoefficient(gammaLabel);
        var zCenterInput = BoundaryCondition(boundaryLabelLabel);
    }

    #endregion

    #region Methods

    private Label MuLabel()
    {
        var label = new Label("Mu:")
        {
            TextAlignment = TextAlignment.Left,
            Width = 4
        };
        Add(label);
        return label;
    }

    private Label GammaLabel(View previous)
    {
        var label = new Label("Gamma:")
        {
            Y = Pos.Top(previous) + 2,
            TextAlignment = TextAlignment.Left,
            Width = 4
        };
        Add(label);
        return label;
    }

    private Label BoundaryLabel(View previous)
    {
        var label = new Label("Краевое условие:")
        {
            Y = Pos.Top(previous) + 2,
            TextAlignment = TextAlignment.Left,
            Width = 4
        };
        Add(label);
        return label;
    }

    private TextField MuCoefficient(View previous)
    {
        var fieldValue = new TextField(ViewModel?.MuCoefficient)
        {
            X = Pos.Right(previous) + 1,
            Y = Pos.Top(previous),
            Width = 10
        };

        ViewModel
            .WhenAnyValue(x => x.MuCoefficient)
            .BindTo(fieldValue, x => x.Text)
            .DisposeWith(_disposable);

        fieldValue
            .Events()
            .TextChanged
            .Select(old => fieldValue.Text)
            .DistinctUntilChanged()
            .BindTo(ViewModel, x => x.MuCoefficient);


        Add(fieldValue);
        return fieldValue;
    }

    private TextField GammaCoefficient(View previous)
    {
        var fieldValue = new TextField(ViewModel?.GammaCoefficient)
        {
            X = Pos.Right(previous) + 1,
            Y = Pos.Top(previous),
            Width = 10
        };

        ViewModel
            .WhenAnyValue(x => x.GammaCoefficient)
            .BindTo(fieldValue, x => x.Text)
            .DisposeWith(_disposable);

        fieldValue
            .Events()
            .TextChanged
            .Select(old => fieldValue.Text)
            .DistinctUntilChanged()
            .BindTo(ViewModel, x => x.GammaCoefficient);


        Add(fieldValue);
        return fieldValue;
    }

    private RadioGroup BoundaryCondition(View previous)
    {
        var fieldValue = new RadioGroup(new[]
        {
            ustring.Make($"Первое краевое"),
            ustring.Make($"Второе краевое"),
            ustring.Make($"Третье краевое"),
        })
        {
            X = Pos.Left(previous),
            Y = Pos.Bottom(previous) + 1,
            SelectedItem = 0,
            DisplayMode = DisplayModeLayout.Horizontal,
        };

        fieldValue.SelectedItemChanged += (obj) =>
        {
            if (ViewModel != null)
                ViewModel.BoundaryCondition = obj.SelectedItem;
        };

        Add(fieldValue);
        return fieldValue;
    }

    #endregion
}