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

            //string QUERY_URL = "https://www.alphavantage.co/query?function=GLOBAL_QUOTE&symbol="+ ativo +"&apikey=C2Q5WR4W8X2UJ4QT";
            string QUERY_URL = "https://www.alphavantage.co/query?function=GLOBAL_QUOTE&symbol=IBM&apikey=demo";
            Uri queryUri = new Uri(QUERY_URL);


            using (WebClient client = new WebClient())
            {
                //JavaScriptSerializer js = new JavaScriptSerializer();
                //dynamic json_data = js.Deserialize(client.DownloadString(queryUri), typeof(object));

                //Dictionary<string, object> json_data = JsonSerializer.Deserialize<Dictionary<string, dynamic>>(client.DownloadString(queryUri));
                dynamic json_data = JsonSerializer.Deserialize<Dictionary<string, dynamic>>(client.DownloadString(queryUri));
                Console.WriteLine("json data:" + json_data);

                //string cotacaoAtivo = json_data["Global Quote"]["05. price"];

                // if (json_data.TryGetValue("Global Quote", out var globalQuote) && globalQuote is Dictionary<string, object> globalQuoteDict)
                // {
                //    if (globalQuoteDict.TryGetValue("05. price", out var price))
                //    {
                //        if (price is string priceString)
                //        {
                //            string cotacaoAtivo = priceString;
                //            // Agora você tem o valor da cotação em 'cotacaoAtivo'
                //        }
                //    }
                //}

                // Suponha que 'json_data' seja seu objeto dynamic
                //string cotacaoAtivo = json_data["Global Quote"]["05. price"].ToString();
                string cotacaoAtivo = json_data["Global Quote"].GetProperty("05. price").GetString();
                // string cotacaoAtivo = json_data.GetProperty("Global Quote").GetProperty("05. price").GetString();

                //Console.WriteLine(cotacaoAtivo); if (json_data.TryGetValue("Global Quote", out var globalQuote) && globalQuote is Dictionary<string, object> globalQuoteDict)
                //{
                //    if (globalQuoteDict.TryGetValue("05. price", out var price))
                //    {
                //        if (price is string priceString)
                //        {
                //            string cotacaoAtivo = priceString;
                //            Console.WriteLine("Cotacao:" + cotacaoAtivo);
                //            this.Preco = Convert.ToDouble(cotacaoAtivo);
                //            // Agora você tem o valor da cotação em 'cotacaoAtivo'
                //        }
                //    }
                //}

                this.Preco = double.Parse(cotacaoAtivo, System.Globalization.CultureInfo.InvariantCulture);
                ;
            }

            Console.WriteLine("Ação:" + this.Preco.ToString());
            Console.WriteLine("mínimo:" + inf.ToString());
            Console.WriteLine("máximo:" + sup.ToString());


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
