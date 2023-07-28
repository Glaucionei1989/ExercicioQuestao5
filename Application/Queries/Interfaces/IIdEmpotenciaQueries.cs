using Questao5.Domain.Entities;

namespace Questao5.Application.Queries.Interfaces
{
    public interface IIdEmpotenciaQueries
    {
        string InsertIdEmpotenciaQuery(string key, string request, string result);
        void UpdateIdEmpotenciaQuery(string key, string result);
        void DeleteIdEmpotenciaQuery(string key);
        IEnumerable<IdEmpotencia> GetIdEmpotenciaByKeyQuery(string key);
    }
}