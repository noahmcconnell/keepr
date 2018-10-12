using System.Collections.Generic;
using System.Data;
using System.Linq;
using keepr.Models;
using Dapper;
using System;

namespace keepr.Repositories
{
    public class KeepsRepository
    {
        private IDbConnection _db;
        public KeepsRepository(IDbConnection db)
        {
            _db = db;
        }
        public Keep GetById(int id)
        {
            return _db.Query<Keep>("SELECT * FROM keeps WHERE id = @id", new { id }).FirstOrDefault();
        }
        public IEnumerable<Keep> GetKeepByUserId(string id)
        {
            return _db.Query<Keep>(@"
            SELECT * FROM keeps
            WHERE userId = @id", new { id });
        }
        public IEnumerable<Keep> GetPublicKeeps()
        {
            return _db.Query<Keep>(" SELECT * FROM keeps WHERE isprivate = 0");
        }

        public IEnumerable<Keep> GetAll()
        {
            return _db.Query<Keep>("SELECT * FROM keeps");
        }

        public int GetVaultCount(int keepId)
        {
            return _db.ExecuteScalar<int>("SELECT count(*) WHERE keepId = @keepId ", new {keepId});
        }

        public Keep Create(Keep keep)
        {
            int id = _db.ExecuteScalar<int>(@"
             INSERT INTO keeps (name, description, img, userid, isprivate) 
            VALUES (@Name, @Description, @Img, @UserId, @IsPrivate);
            SELECT LAST_INSERT_ID();", keep
            );
            keep.Id = id;
            return keep;
        }

        public Keep Update(Keep keep)
        {
            _db.Execute(@"UPDATE keeps
            SET name = @Name, description = @Description, isprivate = @IsPrivate, views = @Views, shares = @Shares
            WHERE id = @Id;", keep);
            return keep;
        }

        public bool Delete(int id)
        {
            int successfulDelete = _db.Execute("DELETE FROM keeps WHERE id = @id", new { id });
            return successfulDelete == 1;
        }


    }
}