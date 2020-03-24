using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Text.Json.Serialization;

namespace APIListaCompras.Util
{
    [Serializable]
    public abstract class GenericModel
    {
        public GenericModel()
        {
            this.GlobalID = Guid.NewGuid();
        }

        [NotMapped]
        [JsonIgnore]
        public Guid GlobalID { get; set; }

        public List<string> ValidarModelo()
        {
            List<ValidationResult> resultadoValidacao = new List<ValidationResult>();

            ValidationContext contexto = new ValidationContext(this, null, null);

            Validator.TryValidateObject(this, contexto, resultadoValidacao, true);

            resultadoValidacao.AddRange(this.ValidacaoLocal());

            return (from _r in resultadoValidacao
                    select _r.ErrorMessage).ToList();
        }
        
        protected virtual List<ValidationResult> ValidacaoLocal()
        {
            return new List<ValidationResult>();
        }

        public string ValidarModeloString()
        {
            StringBuilder sbErros = new StringBuilder();

            ValidarModelo().ForEach(x => sbErros.AppendLine(x));

            return sbErros.ToString();
        }

        public object Clone()
        {
            if (!this.GetType().IsSerializable)
                throw new Exception(string.Format("Classe {0} não é serializável", this.GetType().FullName));

            BinaryFormatter formatter = new BinaryFormatter();
            MemoryStream stream = new MemoryStream();

            formatter.Serialize(stream, this);
            stream.Seek(0, SeekOrigin.Begin);

            // Dim Tipo As Type = Me.GetType

            return formatter.Deserialize(stream);
        }

        [NotMapped]
        [JsonIgnore]
        public StatusRegistro? Status { get; set; }
        public enum StatusRegistro
        {
            INSERTED = 1,
            UPDATED = 2,
            DELETED = 3
        }
    }
}

