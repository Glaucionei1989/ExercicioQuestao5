using Questao5.Domain.Entities;

namespace Questao5.Application.Interfaces
{
    public interface IIdEmpotenciaService
    {
        string Save(string key, string request, string result);
        void Update(string key, string result);
        void Delete(string key);
        IEnumerable<IdEmpotencia> GetIdEmpotenciaByKeyQuery(string key);
    }
}