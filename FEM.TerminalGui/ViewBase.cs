using System.Reactive.Disposables;
using ReactiveUI;
using Terminal.Gui;

namespace FEM.TerminalGui;

public abstract class ViewBase<TViewModel> : Window, IViewFor<TViewModel> where TViewModel : ViewModelBase
{
    protected readonly CompositeDisposable _disposable = new();

    protected ViewBase(TViewModel viewModel, string? title = null)
    {
        ViewModel = viewModel;
        Title = title;
    }

    public TViewModel? ViewModel { get; set; }

    protected override void Dispose(bool disposing)
    {
        _disposable.Dispose();
        base.Dispose(disposing);
    }

    object? IViewFor.ViewModel
    {
        get => ViewModel;
        set => ViewModel = value as TViewModel;
    }
}