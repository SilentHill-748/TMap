namespace TMap.MVVM.Validation.Validators;

public class CreateRoadLayerValidator : AbstractValidator<CreateRoadLayerViewModel>
{
    public CreateRoadLayerValidator()
    {
        RuleFor(viewModel => viewModel.Thickness)
            .GreaterThanOrEqualTo(1)
            .WithMessage("Толщина слоя грунта должна быть не меньше 1 см!");

        RuleFor(viewModel => viewModel.Width)
            .GreaterThanOrEqualTo(700)
            .WithMessage("Ширина слоя грунта должна быть больше 700 см!");

        RuleFor(viewModel => viewModel.Humidity)
            .GreaterThanOrEqualTo(0.00)
            .WithMessage("Влажность должна быть не ниже 0 гр/м³!");

        RuleFor(viewModel => viewModel.InitialTemperature)
            .InclusiveBetween(-70.00, 170.00)
            .WithMessage("Начальная температура материала грунта должна быть между -70.00 и +170.00 °С!");

        RuleFor(viewModel => viewModel.Material)
            .Must(material => !material.Name.Equals("None"))
            .WithMessage("Не выбран материал грунта!");
    }
}
