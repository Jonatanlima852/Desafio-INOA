using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net.Mail;
using System.Net;
using System.Text.Json.Nodes;
using System.Security.Cryptography.X509Certificates;
using System.Text.Json.Serialization;
using Newtonsoft.Json;
using System.Net.Http;

namespace Desafio_INOA
{
    internal class EnvioDeEmails
    {

        public List<string> EmailsDestino { get; set; }

        // Crie uma instância da classe SmtpClient
        private SmtpClient client = new SmtpClient();
        public string Usermame { get; set; }
        // public MailAddress Username { get; private set; }

        public EnvioDeEmails(string arquivo)
        {
            
            if(!File.Exists(arquivo))
            {
                Console.WriteLine("Uai");
                Console.WriteLine(arquivo);
                Console.WriteLine("Arquivo de configuração não foi encontrado.");
                return;
            }

            // código obtido a partir do site da API
            // site Microsoft: https://learn.microsoft.com/en-us/dotnet/api/system.net.mail.smtpclient.-ctor?view=net-7.0
            // Código para estabelecer os parâmetros gerais de conexão com a API 
            // Parâmetros gerais: host(email smtp), Credenciais(email e senha do usuário), porta e certififcado SSL
            // Parâmetros obtidos a partir do arquivo json de entrada
            using (Stream entrada = File.Open(arquivo, FileMode.Open))
            using (StreamReader configFile = new StreamReader(entrada))
            {
                string json = configFile.ReadToEnd();
                ConfigSMTP config = JsonConvert.DeserializeObject<ConfigSMTP>(json);

                this.Usermame = config.Username;
                client.Host = config.Host;
                client.Credentials = new NetworkCredential(config.Username, config.Password);
                client.EnableSsl = true; // Use SSL para conexão segura
                client.Port = config.Port;
                this.EmailsDestino = config.Emails;
            }

        }

        public void EnviarEmails(List<string> emails, double preco, string acao)
        {
            foreach(string email in emails)
            {
                // verificação se está abaixo ou acima do preço
                string situacao = "";
                if (acao == "comprar")
                {
                    situacao = "abaixo";
                } 
                if(acao == "vender")
                {
                    situacao = "acima";
                } 
                // Código para envio de email, obtido a  partir do site da API
                Console.WriteLine($" enviando´email para {email}");
                MailAddress fromAddress = new MailAddress(this.Usermame);
                MailAddress toAddress = new MailAddress(email);
                using (var message = new MailMessage(fromAddress, toAddress)
                {
                    Subject = "Alerta de mudança no ativo",
                    Body = "O preço da ação está em " + preco.ToString() + 
                    ", portanto, " + situacao + " do preço limite, " +
                    "de maneira que a ação recomendada é: " + acao.ToUpper() +"!",
                    IsBodyHtml = false
                })
                {
                    // código para criar um novo cliente e fechá-lo ao fim  do envio de email
                    using (var newClient = new SmtpClient
                    {
                        UseDefaultCredentials = false,
                        Credentials = this.client.Credentials,
                        Host = this.client.Host,
                        Port = this.client.Port,
                        EnableSsl = this.client.EnableSsl
                    })
                    {
                        newClient.Send(message);
                    }
                }
            }
        }

        // Classe para desserializar o json de configuração de forma simplificada
        public class ConfigSMTP
        {

            public string Host { get; set; }
            public string Username { get; set; }
            public string Password { get; set; }
            public int Port { get; set; }
            public List<string> Emails { get; set; }
        }
    }
}
