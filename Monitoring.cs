using JscanMonitoringCore.ClassBody;
using JscanMonitoringCore.Data.DAO;
using JscanMonitoringCore.Data.Model;
using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace JscanMonitoringCore
{
    class Monitoring
    {
        private static ReadDAO _newRead;

        static async Task Main()
        {
            _newRead = new ReadDAO();
            int minute = 60000;

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    HttpResponseMessage response;

                    while (true)
                    {
                        response = await client.GetAsync("https://viacep.com.br/ws/03901010/json/");//Faz a requisição

                        Correios.ValidateStatusCode(response.StatusCode);//Valida o status code retornado da requisição

                        if (Correios.IsOnline)
                        {
                            Console.WriteLine("API Online!");
                        }
                        else
                        {
                            Console.WriteLine("API Offline :(");
                        }

                        Correios.ValidateResponseBody(await response.Content.ReadAsStringAsync());//Valida o corpo da requisição

                        if (Correios.IsValid)
                        {
                            Console.WriteLine("Resposta da API validada!");
                        }
                        else
                        {
                            Console.WriteLine("Resposta da API ínvalida! :(");
                        }

                        //Instanciando um novo objeto de leitura
                        Read read = new Read
                        {
                            Active = Correios.IsOnline,
                            Valid = Correios.IsValid,
                            ReadMoment = DateTime.Now
                        };

                        //Inserindo a leitura no banco
                        _newRead.Insert(read);

                        Thread.Sleep(minute);//Tempo de delay de monitoramento, a validar.

                        Console.WriteLine("");//Quebra de linha para o próximo monitoramento
                    }
                }
                catch (HttpRequestException e)
                {
                    //tratamento de exceção
                    Console.WriteLine("\nOcorreu uma exceção!");
                    Console.WriteLine("Mensagem :{0} ", e.Message);
                }
            }
        }
    }
}
