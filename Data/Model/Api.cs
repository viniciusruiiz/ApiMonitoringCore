using System;

namespace JscanMonitoringCore.Data.Model
{
    ///<summary>
    /// Classe responsável por ser o modelo na hora de buscar a API do banco
    ///</summary>
    public class Api
        {
            public virtual int Id { get; set; }
            public virtual string Name { get; set; }
            public virtual string Type { get; set; }
            public virtual string EndPoint { get; set; }
        }
}