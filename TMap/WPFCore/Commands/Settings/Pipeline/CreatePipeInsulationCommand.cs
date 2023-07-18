namespace TMap.WPFCore.Commands.Settings.Pipeline;

public class CreatePipeInsulationCommand : CommandBase
{
    private readonly CreatePipeInsulationViewModel _viewModel;

    public CreatePipeInsulationCommand(CreatePipeInsulationViewModel viewModel)
    {
        ArgumentNullException.ThrowIfNull(viewModel, nameof(viewModel));

        _viewModel = viewModel;
    }

    protected override void Execute()
    {
        var insulationMaterial = _viewModel.InsulationMaterial;

        if (insulationMaterial is not { })
            throw new Exception("Не удалось создать изоляционный слой трубы!");

        var insulation = new RadialInsulation()
        {
            Material = insulationMaterial,
            InitialTemperature = _viewModel.InitialTemperature,
            Thickness = _viewModel.Thickness
        };

        WeakReferenceMessenger.Default.Send(new CreatePipeInsulationMessage(insulation));
        Reset();
    }

    public override bool CanExecute()
    {
        return _viewModel.IsValid && _viewModel.PipeInsulationLayers.Count < 3;
    }

    private void Reset()
    {
        _viewModel.InsulationMaterial = default;
        _viewModel.InitialTemperature = default;
        _viewModel.Thickness = default;
    }
}
