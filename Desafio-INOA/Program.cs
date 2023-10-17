// See https://aka.ms/new-console-template for more information
using Desafio_INOA;

class Program
{
    static void Main(string[] args)
    {
        // Verificando se os argumentos de linha de comando foram fornecidos
        if (args.Length < 3)
        {
            Console.WriteLine("Não foram fornecidos todos os parâmetros." +
                "Forneça a ação, o limite superior e o limite inferior para os preços");
            return;
        }

        string nomeArquivoConfig = "C:\\Users\\Jonatan\\source\\repos\\Desafio-INOA\\configuracao.json";

        //Para teste sem a prompt, utilizar seguintes parâmetros:
        //string nomeAtivo = "EMBR3.SA";
        //double limiteInf = 17.1;
        //double limiteSup = 17.2;

        string nomeAtivo = args[0]; // O primeiro argumento é o nome do ativo.
        double limiteInf = double.Parse(args[1], System.Globalization.CultureInfo.InvariantCulture); ; // O segundo argumento é o preço inferior
        double limiteSup = double.Parse(args[2], System.Globalization.CultureInfo.InvariantCulture); ; // O terceiro argumento é o preço superior


        // Criando uma instância de LeituraEmails passando o nome do arquivo de configuração. 
        // Essa instância permite realizar a conexão com a API e enviar os emails posteriormente
        EnvioDeEmails sistemaEmail = new EnvioDeEmails(nomeArquivoConfig);

        // Criando uma instância de MonitorarAtivo, passando o ativo e os limites estipulados pelo usuário
        // Essa instância é responsável por obter o preço do ativo e dizer quando ultrapassou algum limite
        MonitorarAtivo ativo = new MonitorarAtivo(nomeAtivo, limiteInf, limiteSup);

        while (true)
        {
            ativo.AtualizarAtivo(nomeAtivo);
            if (ativo.AbaixoInf())
            {
                sistemaEmail.EnviarEmails(sistemaEmail.EmailsDestino, ativo.Preco, "comprar");
            }
            if (ativo.AcimaSup())
            {
                sistemaEmail.EnviarEmails(sistemaEmail.EmailsDestino, ativo.Preco, "vender");
            }
            Thread.Sleep(60000); // Espera 1 minuto antes de verificar o preço novamente
        }        
    }
}
