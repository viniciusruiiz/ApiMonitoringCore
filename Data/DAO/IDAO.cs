namespace JscanMonitoringCore.Data.DAO
{
    /// <summary>
    /// Interface responsável por padronizar os nomes dos métodos CRUDS em cada classe
    /// </summary>
    public interface IDAO<T>
    {
        void Insert(T obj);
        T Get(int id);
    }
}
