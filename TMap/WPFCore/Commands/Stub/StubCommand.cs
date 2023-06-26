using System.Diagnostics;

using TMap.WPFCore.Commands.Base;

namespace TMap.WPFCore.Commands.Stub;

public class StubCommand : CommandBase
{
    private readonly ViewModelBase _viewModel;

    public StubCommand(ViewModelBase viewModel)
    {
        _viewModel = viewModel;
    }

    protected override void Execute()
    {
        Debug.WriteLine($"This is a stub command for {_viewModel.GetType().Name}.");
    }
}
