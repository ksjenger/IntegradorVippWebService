using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Visualset.IntegradorWebService.Entities
{
    public class Datum
    {
        #region Atributos
        public string IdPerfil { get; set; }
        public string NomePerfil { get; set; }
        #endregion

        #region Construtores
        public Datum()
        {
            IdPerfil = string.Empty;
            NomePerfil = string.Empty;
        }
        #endregion
    }
}
