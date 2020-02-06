using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Visualset.IntegradorWebService.Business;
using Visualset.IntegradorWebService.Entities;

namespace Visualset.IntegradorWebService.Facade
{
    public class VIPPRestFacade
    {
        #region LogarVIPPRest
        public bool Logar(string txtUsr, string txtPwd)
        {
            return new VIPPRestBusiness().Logar(txtUsr, txtPwd);
        }
        #endregion

        #region 
        public Rootobject ProcessaListaPerfil(string txtUsr, string txtPwd)
        {
            return new VIPPRestBusiness().ProcessaListaPerfil(txtUsr, txtPwd);
        }
        #endregion
    }
}
