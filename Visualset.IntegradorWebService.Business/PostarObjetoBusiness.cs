using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Visualset.IntegradorWebService.Business.Process;
using Visualset.IntegradorWebService.DataLayer.ServiceReference;

namespace Visualset.IntegradorWebService.Business
{
    public class PostarObjetoBusiness
    {
        #region Postar Objeto VIPP
        public string Postagem(List<Postagem> lPostagem, Form frm)
        {
            return new PostarObjetoProcess().Postagem(lPostagem, frm);
        }
        #endregion
    }
}
