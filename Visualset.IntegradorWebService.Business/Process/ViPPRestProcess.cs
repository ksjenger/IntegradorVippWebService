using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Visualset.IntegradorWebService.DataLayer;
using Visualset.IntegradorWebService.Entities;

namespace Visualset.IntegradorWebService.Business.Process
{
    public class ViPPRestProcess
    {
        #region LogarVIPPRest
        public bool Logar(string txtUsr, string txtPwd)
        {
            return new VippRestData().Logar(txtUsr, txtPwd);
        }
        #endregion

        #region 
        public Rootobject ProcessaListaPerfil(string txtUsr, string txtPwd)
        {            
            return new VippRestData().ProcessaListaPerfil(txtUsr, txtPwd);
        }
        #endregion


    }
}
