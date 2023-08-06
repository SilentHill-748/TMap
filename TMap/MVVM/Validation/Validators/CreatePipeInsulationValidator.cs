namespace TMap.MVVM.Validation.Validators;

public class CreatePipeInsulationValidator : AbstractValidator<CreatePipeInsulationViewModel>
{
    public CreatePipeInsulationValidator()
    {
        RuleFor(viewModel => viewModel.Thickness)
            .GreaterThanOrEqualTo(1)
            .WithMessage("Толщина слоя изоляции трубы должна быть между 1 и 50 см!");

        RuleFor(viewModel => viewModel.InitialTemperature)
            .InclusiveBetween(-70.00, 170.00)
            .WithMessage("Начальная температура изоляционного материала должна быть между -70.00 и +170.00 °С!");
        
        RuleFor(viewModel => viewModel.InsulationMaterial)
            .Must(material => !material.Name.Equals("None"))
            .WithMessage("Не выбран материал изоляции!");
    }
}
