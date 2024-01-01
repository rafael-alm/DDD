using productManagement.domain.aggregates.supplier.validations;
using productManagement.domain.shared.seedWork.exceptions;
using System.Text.RegularExpressions;

namespace productManagement.domain.objectValues
{
    public record CNPJ
    {
        public CNPJ(string number)
        {
            if (!IsValid(number))
                throw new EntityValidationException(SupplierMessages.CNPJMustBeValid.Text);

            Number = onlyNumber(number);
        }

        public string Number { get; init; }

        public static bool IsValid(string number)
        {
            //if (string.IsNullOrEmpty(numero))
            //    return false;
            string cnpj = onlyNumber(number);

            if (cnpj.Length != 14)
                return false;

            int[] digitos, soma, resultado;
            int nrDig;
            string ftmt;
            bool[] CNPJOk;

            ftmt = "6543298765432";

            digitos = new int[14];

            soma = new int[2];
            soma[0] = 0;
            soma[1] = 0;

            resultado = new int[2];
            resultado[0] = 0;
            resultado[1] = 0;

            CNPJOk = new bool[2];
            CNPJOk[0] = false;
            CNPJOk[1] = false;

            for (nrDig = 0; nrDig < 14; nrDig++)
            {
                digitos[nrDig] = int.Parse(
                cnpj.Substring(nrDig, 1));

                if (nrDig <= 11)
                    soma[0] += (digitos[nrDig] *
                      int.Parse(ftmt.Substring(
                      nrDig + 1, 1)));

                if (nrDig <= 12)
                    soma[1] += (digitos[nrDig] *
                      int.Parse(ftmt.Substring(
                      nrDig, 1)));
            }

            for (nrDig = 0; nrDig < 2; nrDig++)
            {
                resultado[nrDig] = (soma[nrDig] % 11);

                if ((resultado[nrDig] == 0) || (resultado[nrDig] == 1))
                    CNPJOk[nrDig] = (digitos[12 + nrDig] == 0);
                else
                    CNPJOk[nrDig] = (digitos[12 + nrDig] == (11 - resultado[nrDig]));
            }

            return (CNPJOk[0] && CNPJOk[1]);
        }

        private static string onlyNumber(string cnpj)
            => Regex.Replace(cnpj, "[^\\d]", string.Empty);
    }
}