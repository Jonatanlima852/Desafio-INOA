using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.Net;

namespace Desafio_INOA
{
    internal class MonitorarAtivo
    {
        private string ApiKey { get; set; }
        private double CotacaoInf { get; set; }
        private double CotacaoSup { get; set; }
        private double Preco { get; set; }

        public MonitorarAtivo(string ativo, double inf, double sup)
        {
            // Colocando os valores sup e inf no constructor 
            this.CotacaoInf = inf;
            this.CotacaoSup = sup;

            // Conexão com a API de monitoramento de ativos - 

            string QUERY_URL = "https://www.alphavantage.co/query?function=GLOBAL_QUOTE&symbol=IBM&apikey=C2Q5WR4W8X2UJ4QT";
            Uri queryUri = new Uri(QUERY_URL);

            string cotacaoAtivo = "";

            using (WebClient client = new WebClient())
            {
                
                dynamic json_data = JsonSerializer.Deserialize<Dictionary<string, dynamic>>(client.DownloadString(queryUri));

                cotacaoAtivo += json_data.First["Global Quote"].First["05. price"].ToString();

                Preco = Convert.ToDouble(cotacaoAtivo);
            }

            // Monitoramento se o ativo está maior ou menor que as cotas estipuladas

        }

        public bool AbaixoInf()
        {
            // Verifica se o ativo está abaixo do limite inferior estipulado
            if(this.Preco < CotacaoInf)
            {
                return true;
            }
            return false;
        }

        public bool AcimaSup()
        {
            // verifica se o ativo superou o limite superior estipulado
            if (this.Preco > CotacaoSup)
            {
                return true;
            }
            return false;
        }
    }
}
