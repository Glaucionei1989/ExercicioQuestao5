using Dapper;
using Microsoft.Data.Sqlite;
using Questao5.Application.Extensions;
using Questao5.Application.Interfaces;
using Questao5.Application.Queries.Interfaces;
using Questao5.Application.Queries.Responses;
using Questao5.Domain.Entities;
using Questao5.Domain.Enumerators;

namespace Questao5.Infrastructure.Services
{
    public class IdEmpotenciaService : IIdEmpotenciaService
    {
        private readonly IIdEmpotenciaQueries idEmpotenciaQueries;

        public IdEmpotenciaService(IIdEmpotenciaQueries idEmpotenciaQueries)
        {
            this.idEmpotenciaQueries = idEmpotenciaQueries;
        }

        public string Save(string key, string request, string result)
        {
            return idEmpotenciaQueries.InsertIdEmpotenciaQuery(key, request, result);
        }

        public void Update(string key, string result)
        {
            idEmpotenciaQueries.UpdateIdEmpotenciaQuery(key, result);
        }

        public void Delete(string key)
        {
            idEmpotenciaQueries.DeleteIdEmpotenciaQuery(key);
        }

        public IEnumerable<IdEmpotencia> GetIdEmpotenciaByKeyQuery(string key)
        {
            return idEmpotenciaQueries.GetIdEmpotenciaByKeyQuery(key);
        }
    }
}
