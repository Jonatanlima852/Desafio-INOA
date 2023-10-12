// See https://aka.ms/new-console-template for more information
using Desafio_INOA;

class Program
{
    static void Main(string[] args)
    {
        // Verifique se um argumento de linha de comando (nome do arquivo) foi fornecido.
        if (args.Length < 0)
        {
            Console.WriteLine("Não foram fornecidos todos os parâmetros." +
                "Forneça a ação, o limite superior e o limite inferior");
            return;
        }

        //string nomeArquivoConfig = "configuracao.json";

        string nomeArquivoConfig = "C:\\Users\\Jonatan\\source\\repos\\Desafio-INOA\\configuracao.json";

        //string nomeArquivoConfig = args[0]; // O primeiro argumento é o nome do arquivo.
        //int limiteInf = int.Parse(args[1]); // O segundo argumento é o preço inferior
        //int limiteSup = int.Parse(args[2]); // O terceiro argumento é o preço superior

        // Crie uma instância de LeituraEmails passando o nome do arquivo de configuração.
        LeituraEmails leitor = new LeituraEmails(nomeArquivoConfig);

        // Agora você pode acessar a propriedade EmailsDestino na instância leitor.
        foreach (string email in leitor.EmailsDestino)
        {
            Console.WriteLine("Email de destino: " + email);
        }
    }
}
