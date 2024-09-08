using System.ComponentModel;
using System.Reactive.Disposables;
using ReactiveUI;

namespace FEM.TerminalGui;

public abstract class ViewModelBase : ReactiveObject, IActivatableViewModel
{
    #region LifeCycle

    protected ViewModelBase()
    {
        Activator = new ViewModelActivator();
        this.WhenActivated(disposables =>
        {
            OnActivation(disposables);
            SetupPropertyChangedHandler();
            Disposable.Create(OnDeactivation).DisposeWith(disposables);
        });
    }

    protected virtual void OnActivation(CompositeDisposable disposables)
    {
    }

    protected virtual void OnDeactivation()
    {
        DisposePropertyChangedHandler();
    }

    #endregion

    #region Properties

    public string Title { get; protected set; } = string.Empty;

    public ViewModelActivator Activator { get; }

    #endregion

    #region Methods

    private void SetupPropertyChangedHandler()
    {
        PropertyChanged += OnPropertyChanged;
    }

    private void DisposePropertyChangedHandler()
    {
        PropertyChanged -= OnPropertyChanged;
    }

    protected virtual void OnPropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
    }

    #endregion
}