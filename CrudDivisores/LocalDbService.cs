using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrudDivisores
{
    public class LocalDbService
    {
        private const string DB_NAME = "demo_registrosA_db.db3"; 
        private SQLiteAsyncConnection _connection; 

        public LocalDbService()
        {
            _connection = new SQLiteAsyncConnection(Path.Combine(FileSystem.AppDataDirectory, DB_NAME));
            _connection.CreateTableAsync<TRegistros>(); 
        }

        public async Task<List<TRegistros>> GetRecordsAsync()
        {
            return await _connection.Table<TRegistros>().ToListAsync();
        }

        public async Task<int> SaveRecordAsync(TRegistros registro)
        {
            return await _connection.InsertAsync(registro);
        }

        public async Task<int> UpdateRecordAsync(TRegistros registro)
        {
            return await _connection.UpdateAsync(registro);
        }

        public async Task<int> DeleteRecordAsync(TRegistros registro)
        {
            return await _connection.DeleteAsync(registro);
        }
    }
}
