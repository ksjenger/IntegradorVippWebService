﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Visualset.IntegradorWebService.Entities
{
    public class JsonRetornoLogar
    {
        #region Atributos
        public string Status { get; set; }

        public string Msg { get; set; }
        #endregion

        #region Construtores
        public JsonRetornoLogar()
        {
            Status = string.Empty;
            Msg = string.Empty;        
        }

        #endregion

    }
}
