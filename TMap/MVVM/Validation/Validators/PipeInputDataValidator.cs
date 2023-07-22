namespace TMap.MVVM.Validation.Validators;

public class PipeInputDataValidator : AbstractValidator<PipeInputDataViewModel>
{
    public PipeInputDataValidator()
    {
        RuleFor(viewModel => viewModel.Radius)
            .InclusiveBetween(6, 27)
            .WithMessage("Радиус трубы должен быть между 6 и 27 см!");

        RuleFor(viewModel => viewModel.MaterialTemperature)
            .InclusiveBetween(-10.00, 170.00)
            .WithMessage("Температура материала трубы должна быть между -10.00 и +170.00 °С!");

        RuleFor(viewModel => viewModel.CoolantTemperature)
            .InclusiveBetween(1.00, 400.00)
            .WithMessage("Температура теплоносителя трубы должна быть между +1.00 и +400.00 °С");

        RuleFor(viewModel => viewModel.Thickness)
            .InclusiveBetween(1, 3)
            .WithMessage("Толщина трубы должна быть между 1 и 3 см!");

        RuleFor(viewModel => viewModel.PipeType)
            .NotNull()
            .WithMessage("Не выбран тип трубы!");
    }
}
