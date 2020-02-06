using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Visualset.IntegradorWebService.Entities
{
    public class Rootobject
    {
        #region Atributos
        public Datum[] Data { get; set; }
        public int Status { get; set; }
        #endregion

        #region Construtores
        public Rootobject()
        {
            Data = null;
            Status = int.MinValue;
        }
        #endregion
    }
}
