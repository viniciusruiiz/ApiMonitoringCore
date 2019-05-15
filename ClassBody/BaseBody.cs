using System;
using System.Net;

namespace JscanMonitoringCore.ClassBody
{
    /// <summary>
    /// Classe base para todas as classes para guardar o corpo da requisição da API.
    /// </summary>
    public class BaseBody
    {
        public bool IsOnline;
        public bool IsValid;

        /// <summary>
        /// Método Responsável por ler o status code da API, e validar se a API está ou não online.
        /// </summary>
        public void ValidateStatusCode(HttpStatusCode code)
        {
            Console.WriteLine("validando codigo de resposta...");
            IsOnline = code == HttpStatusCode.OK;
        }

        /// <summary>
        /// Método Responsável por ler o corpo da API, e validar se a resposta é o esperado
        /// </summary>
        public virtual void ValidateResponseBody(string response)
        {
            Console.WriteLine("validando corpo de resposta...");

            IsValid = !(response is null);
        }
    }
}
