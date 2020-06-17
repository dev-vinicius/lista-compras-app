using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Text.Json.Serialization;
using System.Linq;

namespace ListaCompras.Shared.Models
{
    [Serializable]
    public abstract class GenericModel
    {
        public List<string> ValidationMessages()
        {
            List<ValidationResult> resultadoValidacao = new List<ValidationResult>();

            ValidationContext contexto = new ValidationContext(this, null, null);

            Validator.TryValidateObject(this, contexto, resultadoValidacao, true);

            resultadoValidacao.AddRange(this.AdditionalValidation());

            return (from _r in resultadoValidacao
                    select _r.ErrorMessage).ToList();
        }

        protected virtual List<ValidationResult> AdditionalValidation()
        {
            return new List<ValidationResult>();
        }

        public string ValidationMessagesString()
        {
            StringBuilder sbErros = new StringBuilder();

            ValidationMessages().ForEach(x => sbErros.AppendLine(x));

            return sbErros.ToString();
        }

        [NotMapped]
        [JsonIgnore]
        public bool IsValid
        {
            get
            {
                return ValidationMessagesString().Length == 0;
            }
        }

    }
}
