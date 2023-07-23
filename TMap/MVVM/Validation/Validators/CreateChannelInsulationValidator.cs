namespace TMap.MVVM.Validation.Validators;

public class CreateChannelInsulationValidator : AbstractValidator<CreateChannelInsulationViewModel>
{
    public CreateChannelInsulationValidator()
    {
        RuleFor(viewModel => viewModel.Thickness)
            .GreaterThanOrEqualTo(1)
            .WithMessage("Толщина слоя изоляции коллектора должна быть между 1 и 50 см!");

        RuleFor(viewModel => viewModel.InitialTemperature)
            .InclusiveBetween(-70.00, 170.00)
            .WithMessage("Начальная температура изоляционного материала должна быть между -70.00 и +170.00 °С!");

        RuleFor(viewModel => viewModel.Material)
            .NotNull()
            .WithMessage("Не выбран материал изоляции!");
    }
}
