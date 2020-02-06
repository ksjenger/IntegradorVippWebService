using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Visualset.IntegradorWebService.Business;
using Visualset.IntegradorWebService.DataLayer.ServiceReference;

namespace Visualset.IntegradorWebService.Facade
{
    public class PostarObjetoFacade
    {
        #region Postar Objeto VIPP
        public string Postagem(List<Postagem> lPostagem, Form frm)
        {
            return new PostarObjetoBusiness().Postagem(lPostagem, frm);
        }
        #endregion
    }
}
