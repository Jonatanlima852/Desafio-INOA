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


                // Crie uma instância da classe SmtpClient
                SmtpClient client = new SmtpClient();

                // Especifique seu host de correspondência, nome de usuário, senha, número da porta e opção de segurança
                // client.Host = "mail.server.com";
                // client.Credentials = new NetworkCredential("seuemail@gmail.com", "suasenha");
                // client.Port = 587;

                List<string> emails = config.Emails;

                client.Host = config.Host;
                client.Credentials = new NetworkCredential(config.Username, config.Password);
                client.EnableSsl = true; // Use SSL para conexão segura
                client.Port = config.Port;
                this.EmailsDestino = emails;
            }

        }

    }
}
