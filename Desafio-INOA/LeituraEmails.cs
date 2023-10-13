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
    internal class LeituraEmails
    {

        public class ConfigSMTP
        {

            public string Host { get; set; }
            public string Username { get; set; }
            public string Password { get; set; }
            public int Port { get; set; }
            public List<string> Emails { get; set; }
        }

        public List<string> EmailsDestino { get; set; }

        // Crie uma instância da classe SmtpClient
        private SmtpClient client = new SmtpClient();
        public string Usermame { get; set; }
        // public MailAddress Username { get; private set; }

        public LeituraEmails(string arquivo)
        {
            
            if(!File.Exists(arquivo))
            {
                Console.WriteLine("Uai");
                Console.WriteLine(arquivo);
                Console.WriteLine("Arquivo de configuração não foi encontrado.");
                return;
            }

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

        public void EnviarEmails(List<string> emails)
        {
            foreach(string email in emails)
            {
                MailAddress fromAddress = new MailAddress(this.Usermame);
                MailAddress toAddress = new MailAddress(email);
                using (var message = new MailMessage(fromAddress, toAddress)
                {
                    Subject = "Email teste",
                    Body = "Esse é um email teste",
                    IsBodyHtml = false


                })
                {
                    using (var newClient = new SmtpClient
                    {
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
    }
}
