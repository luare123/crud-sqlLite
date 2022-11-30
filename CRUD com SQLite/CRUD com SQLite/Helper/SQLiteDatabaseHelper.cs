using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CRUD_com_SQLite.Model;
using SQLite;

namespace CRUD_com_SQLite.Helper
{
    public class SQLiteDatabaseHelper
    {
        readonly SQLiteAsyncConnection _conn;

        public SQLiteDatabaseHelper(string path)
        {
            _conn = new SQLiteAsyncConnection(path);
            _conn.CreateTableAsync<produto>().Wait();
        }
        public Task<int> insert(produto p)
        {
            return _conn.InsertAsync(p);
        }
        public Task<List<produto>> update(produto p)
        {
            string sql = "UPDATE produto SET descricao=?, quantidade=?, preco=? WHERE id=? ";
            return _conn.QueryAsync<produto>(sql, p.descricao, p.quantidade, p.preco, p.id);
        }
        
        public Task<List<produto>> getAll(string q)
        {
            return _conn.Table<produto>().ToListAsync();
        }
        public Task<int> delete(int id)
        {
            return _conn.Table<produto>().DeleteAsync(i => i.id == id);
        }
        public Task<List<produto>> Search(string q)
        {
            string sql = "SELECT * FROM produto WHERE descricao LIKE '%" + q + "%' ";
            return _conn.QueryAsync<produto>(sql);
        }
    }
}
