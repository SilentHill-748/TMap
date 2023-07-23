namespace TMap.MVVM.Validation.Validators;

public class RoadInputDataValidator : AbstractValidator<RoadInputDataViewModel>
{
    public RoadInputDataValidator()
    {
        RuleFor(viewModel => viewModel.Width)
            .GreaterThanOrEqualTo(700)
            .WithMessage("Ширина проезжей части должна быть больше 700 см!");

        RuleFor(viewModel => viewModel.MoundWidth)
            .InclusiveBetween(50, 100)
            .When(viewModel => viewModel.HasMound)
            .WithMessage("Ширина насыпи должна быть между 50 и 100 см!");

        RuleFor(viewModel => viewModel.MoundHeight)
            .InclusiveBetween(20, 100)
            .When(viewModel => viewModel.HasMound)
            .WithMessage("Высота насыпи должна быть между 20 и 100 см!");

        RuleFor(viewModel => viewModel.RoadsideWidth)
            .InclusiveBetween(25, 50)
            .WithMessage("Ширина обочины должна быть между 25 и 50 см!");

        RuleFor(viewModel => viewModel.EdgeWidth)
            .GreaterThanOrEqualTo(50)
            .WithMessage("Край дороги должен быть не менее 50 см!");
    }
}
