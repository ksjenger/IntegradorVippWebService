using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Visualset.IntegradorWebService.DataLayer.ServiceReference;
using Visualset.IntegradorWebService.DataLayer;

namespace Visualset.IntegradorWebService.Business.Process
{
    public class PostarObjetoProcess
    {

        #region Postar Objeto VIPP
        public string Postagem(List<Postagem> lPostagem, Form frm)
        {
            return new PostarObjetoData().Postagem(lPostagem);
        }
        #endregion

    }
}
