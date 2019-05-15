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
            ApiDAO colocarTabela = new ApiDAO();

            var api = colocarTabela.Get(1);

            Console.WriteLine("API BUSCADA: " +api.Name+"\n");

            await MonitoringApi(api.EndPoint , new Correios());
        }

        static async Task MonitoringApi(string url, BaseBody @base)
        {
            _newRead = new ReadDAO();
            int minute = 60000;
            BaseBody api = @base;
            HttpResponseMessage response;

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    while (true)
                    {
                        response = await client.GetAsync(url);//Faz a requisição

                        api.ValidateStatusCode(response.StatusCode);//Valida o status code retornado da requisição

                        if (api.IsOnline)
                        {
                            Console.WriteLine("API Online!");
                        }
                        else
                        {
                            Console.WriteLine("API Offline :(");
                        }

                        api.ValidateResponseBody(await response.Content.ReadAsStringAsync());//Valida o corpo da requisição

                        if (api.IsValid)
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
                            Active = api.IsOnline,
                            Valid = api.IsValid,
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
