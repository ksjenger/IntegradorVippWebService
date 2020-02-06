using Visualset.IntegradorWebService.Entities;
using System;
using System.IO;
using System.Net;
using System.Text;
using System.Windows.Forms;
using System.Web.Script.Serialization;
using Newtonsoft.Json;

namespace Visualset.IntegradorWebService.DataLayer
{
    public class VippRestData
    {
        #region Verifica se o Login no VIPP e valido
        public bool Logar(string txtUsr, string txtPwd)
        {
            try
            {
                ServicePointManager.Expect100Continue = false;
                if (string.IsNullOrEmpty(Properties.Settings.Default.Site))
                {
                    return false;
                }

                HttpWebRequest httpWReq = (HttpWebRequest)WebRequest.Create(Properties.Settings.Default.Site + "/vipp/remoto/LoginRemoto.php");

                ASCIIEncoding encoding = new ASCIIEncoding();

                string postData = "";
                postData += "&Login=" + txtUsr;
                postData += "&Senha=" + txtPwd;

                byte[] data = encoding.GetBytes(postData);


                httpWReq.Method = "POST";
                httpWReq.ContentType = "application/x-www-form-urlencoded";
                httpWReq.ContentLength = data.Length;

                using (Stream stream = httpWReq.GetRequestStream())
                {
                    stream.Write(data, 0, data.Length);
                }

                HttpWebResponse response = (HttpWebResponse)httpWReq.GetResponse();

                string responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();

                JsonRetornoLogar oJsonRetorno = new JavaScriptSerializer().Deserialize<JsonRetornoLogar>(responseString);

                if (oJsonRetorno.Status.Equals("0"))
                {
                    MessageBox.Show(oJsonRetorno.Msg, "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }

                if (oJsonRetorno.Status.Equals("1"))
                {
                    return true;
                }

                return false;
            }
            catch (Exception)
            {
                MessageBox.Show("Impossível Acessar o Site, entre em contato com a VisualSet", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

        }

        #endregion

        #region Retorna do VIPP o Perfil de Importacao
        public Rootobject ProcessaListaPerfil(string txtUsr, string txtPwd)
        {
            Rootobject b = new Rootobject();

            try
            {
                ServicePointManager.Expect100Continue = false;


                HttpWebRequest httpWReq = (HttpWebRequest)WebRequest.Create(Properties.Settings.Default.Site + "/vipp/remoto/ListarPerfisRemoto.php");

                ASCIIEncoding encoding = new ASCIIEncoding();

                string postData = "";
                postData += "&Usr=" + txtUsr;
                postData += "&Pwd=" + txtPwd;

                byte[] data = encoding.GetBytes(postData);


                httpWReq.Method = "POST";
                httpWReq.ContentType = "application/x-www-form-urlencoded";
                httpWReq.ContentLength = data.Length;

                using (Stream stream = httpWReq.GetRequestStream())
                {
                    stream.Write(data, 0, data.Length);
                }

                HttpWebResponse response = (HttpWebResponse)httpWReq.GetResponse();

                using (StreamReader streamReader = new StreamReader(response.GetResponseStream()))
                {
                    string responseString = streamReader.ReadToEnd();
                    b = JsonConvert.DeserializeObject<Rootobject>(responseString);
                }

            }
            catch (Exception)
            {
                b.Data[0].NomePerfil = "Erro";

            }

            return b;
        }
        #endregion

    }
}
