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
        public double Preco { get; set; }

        public MonitorarAtivo(string ativo, double inf, double sup)
        {
            // Colocando os valores sup e inf no constructor 
            this.CotacaoInf = inf;
            this.CotacaoSup = sup;


            // Conexão com a API de monitoramento de ativos
            string QUERY_URL = "https://www.alphavantage.co/query?function=GLOBAL_QUOTE&symbol="+ ativo +"&apikey=C2Q5WR4W8X2UJ4QT";
           
            // Query teste a seguir, sem apiKey
            //string QUERY_URL = "https://www.alphavantage.co/query?function=GLOBAL_QUOTE&symbol=IBM&apikey=demo";
            Uri queryUri = new Uri(QUERY_URL);

            using (WebClient client = new WebClient())
            {
                // Código obtido a partir do site da API.
                // Site encontrado em:https://www.alphavantage.co/documentation/
                dynamic json_data = JsonSerializer.Deserialize<Dictionary<string, dynamic>>(client.DownloadString(queryUri));

                string cotacaoAtivo = json_data["Global Quote"].GetProperty("05. price").GetString();

                // Transformando o texto para double independente da linguagem do visual studio (código obtido pelo chatGPT)
                this.Preco = double.Parse(cotacaoAtivo, System.Globalization.CultureInfo.InvariantCulture);
                
            }

            Console.WriteLine("Ação:" + this.Preco.ToString());
            Console.WriteLine("mínimo:" + inf.ToString());
            Console.WriteLine("máximo:" + sup.ToString());
        }

        public void AtualizarAtivo(string ativo)
        {

            // Conexão com a API de monitoramento de ativos
            string QUERY_URL = "https://www.alphavantage.co/query?function=GLOBAL_QUOTE&symbol=" + ativo + "&apikey=C2Q5WR4W8X2UJ4QT";

            // Query teste a seguir, sem apiKey
            //string QUERY_URL = "https://www.alphavantage.co/query?function=GLOBAL_QUOTE&symbol=IBM&apikey=demo";
            Uri queryUri = new Uri(QUERY_URL);

            using (WebClient client = new WebClient())
            {
                // Código obtido a partir do site da API.
                // Site encontrado em:https://www.alphavantage.co/documentation/
                dynamic json_data = JsonSerializer.Deserialize<Dictionary<string, dynamic>>(client.DownloadString(queryUri));

                string cotacaoAtivo = json_data["Global Quote"].GetProperty("05. price").GetString();

                // Transformando o texto para double independente da linguagem do visual studio (código obtido pelo chatGPT)
                this.Preco = double.Parse(cotacaoAtivo, System.Globalization.CultureInfo.InvariantCulture);

            }

            Console.WriteLine("Ação:" + this.Preco.ToString());
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
