using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;

namespace Desafio_INOA
{
    internal class MonitorarAtivo
    {
        private string ApiKey { private get; private set }
        private double CotacaoInf { get; set; }
        private double CotacaoSup { get; set; }
        private double Price { get; set; }
        public MonitorarAtivo(string ativo)
        {
            // Conexão com a API de monitoramento de ativos - 

            string QUERY_URL = "https://www.alphavantage.co/query?function=GLOBAL_QUOTE&symbol=IBM&apikey=demo"
            Uri queryUri = new Uri(QUERY_URL);

            string cotacaoAtivo = ""; 

            using (WebClient client = new WebClient())
            {
                
                dynamic json_data = JsonSerializer.Deserialize<Dictionary<string, dynamic>>(client.DownloadString(queryUri));

                string cotacaoAtivo = json_data.First["Global Quote"].First["05. price"].ToString();

                Price = Convert.ToDouble(cotacaoAtivo);
            }


            // Monitoramento se o ativo está maior ou menor que as cotas estipuladas

        }

        public bool CotacaoInf(double cotacao)
        {
            // Verifica se o ativo está abaixo do limite inferior estipulado
            if(cotacao < CotacaoInf)
            {
                return true;
            }
            return false;
        }

        public bool CotacaoSup(double cotacao)
        {
            // verifica se o ativo superou o limite superior estipulado
            if (cotacao > CotacaoSup)
            {
                return true;
            }
            return false;
        }
    }
}
