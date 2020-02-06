using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Visualset.IntegradorWebService.Business.Process;
using Visualset.IntegradorWebService.Entities;

namespace Visualset.IntegradorWebService.Business
{
    public class VIPPRestBusiness
    {
        #region LogarVIPPRest
        public bool Logar(string txtUsr, string txtPwd)
        {
            return new ViPPRestProcess().Logar(txtUsr, txtPwd);
        }
        #endregion

        #region 
        public Rootobject ProcessaListaPerfil(string txtUsr, string txtPwd)
        {
            return new ViPPRestProcess().ProcessaListaPerfil(txtUsr, txtPwd);
        }
        #endregion
    }
}
