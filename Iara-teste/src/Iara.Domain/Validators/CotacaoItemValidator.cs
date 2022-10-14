using FluentValidation;
using Iara.Domain.Entities;

namespace Iara.Domain.Validators
{
    public class CotacaoItemValidator : AbstractValidator<CotacaoItem>
    {
        public CotacaoItemValidator()
        {
            RuleFor(x => x).NotEmpty().NotNull().WithMessage("A entidade não pode ser nula");

            RuleFor(x => x.Descricao).NotEmpty().NotNull().WithMessage("A descrição não pode ser vazio ou nulo");
            RuleFor(x => x.NumeroItem).NotEmpty().NotNull().WithMessage("O número do item não pode ser vazio ou nulo");
            RuleFor(x => x.Quantidade).NotEmpty().NotNull().WithMessage("A quantidade não pode ser vazio ou nulo");
        }
    }
}
