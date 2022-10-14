using FluentValidation;
using Iara.Core.Utils;
using Iara.Domain.Entities;

namespace Iara.Domain.Validators
{
    public class CotacaoValidator : AbstractValidator<Cotacao>
    {
        public CotacaoValidator()
        {
            RuleFor(x => x).NotEmpty().NotNull().WithMessage("A entidade não pode ser nula");

            RuleFor(x => x.CNPJComprador.Length).Equal(CnpjValidacao.TamanhoCnpj)
                   .WithMessage("O campo CNPJ precisa ter {ComparisonValue} caracteres e foi fornecido {PropertyValue}.");

            RuleFor(x => x.CNPJFornecedor.Length).Equal(CnpjValidacao.TamanhoCnpj)
                   .WithMessage("O campo CNPJ precisa ter {ComparisonValue} caracteres e foi fornecido {PropertyValue}.");

            RuleFor(x => x.NumeroCotacao).NotEmpty().NotNull().WithMessage("O número da cotação não pode ser vazio ou nulo");
            RuleFor(x => x.DataCotacao).NotEmpty().NotNull().WithMessage("A data de cotação não pode ser vazio ou nulo");
            RuleFor(x => x.DataEntregaCotacao).NotEmpty().NotNull().WithMessage("A data de entrega da cotação não pode ser vazio ou nulo");
            RuleFor(x => x.CEP).NotEmpty().NotNull().WithMessage("O cep não pode ser vazio ou nulo");

            RuleFor(x => CnpjValidacao.Validar(x.CNPJComprador)).Equal(true)
                .WithMessage("O documento fornecido é inválido.");

            RuleFor(x => CnpjValidacao.Validar(x.CNPJFornecedor)).Equal(true)
                .WithMessage("O documento fornecido é inválido.");
        }
    }
}
