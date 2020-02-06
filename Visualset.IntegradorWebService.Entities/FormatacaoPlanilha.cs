namespace Visualset.IntegradorWebService.Entities
{
    public class FormatacaoPlanilha
    {
        #region Atributos
        public string NomeAtributo { get; set; }
        public int Coluna { get; set; }

        public int Inicio { get; set; }

        public int Tamanho { get; set; }

        public string Delimitador { get; set; }       

        public string Valor { get; set; }

        #endregion

        #region Construtores
        public FormatacaoPlanilha()
        {
            NomeAtributo = string.Empty;
            Coluna = int.MinValue;
            Tamanho = int.MinValue;
            Delimitador = string.Empty;
            Valor = string.Empty;
        }
        #endregion

    }
}