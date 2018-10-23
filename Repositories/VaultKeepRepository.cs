using System.Collections.Generic;
using System.Data;
using System.Linq;
using keepr.Models;
using Dapper;

namespace keepr.Repositories {
    public class VaultkeepRepository {
        private IDbConnection _db;
        public VaultkeepRepository(IDbConnection db) {
            _db = db;
        }
        public IEnumerable<Vaultkeep> GetAll() {
            return _db.Query<Vaultkeep>("SELECT * FROM vaultkeeps;");
        }
        public Vaultkeep GetById(int id) {
            return _db.Query<Vaultkeep>(@"Select * from vaultkeeps where id = @id", new { id }).FirstOrDefault();
        }
        public IEnumerable<Keep> GetByVaultID(int vaultId) {
            return _db.Query<Keep>(@"
                  SELECT * FROM vaultkeeps vk
                  INNER JOIN keeps k ON k.id = vk.keepId
                  WHERE (vaultId = @vaultId)", new { vaultId });
        }
        public IEnumerable<Vaultkeep> GetAllUserVaultKeeps(string id) {
            return _db.Query<Vaultkeep>("SELECT * FROM vaultkeeps WHERE id = @id;", new { id });
        }
        public Vaultkeep Create(Vaultkeep vaultkeep) {
            int id = _db.ExecuteScalar<int>(@"
                  INSERT INTO vaultkeeps (vaultid, keepid, userid)
                  Values (@VaultId, @KeepId, @UserId);
                  SELECT LAST_INSERT_ID();", vaultkeep
            );
            vaultkeep.Id = id;
            UpdateKeepCount(vaultkeep.KeepId);
            return vaultkeep;
        }
        public Vaultkeep Update(Vaultkeep vaultkeep) {
            _db.Execute(@"UPDATE vaultkeeps
                  SET vaultid = @VaultId, keepid = @KeepId
                  WHERE id = @Id;", vaultkeep);
            return vaultkeep;
        }
        public void UpdateKeepCount(int keepId) {
            _db.Execute(@"Update Keeps set keeps = (select count(*) from vaultkeeps where keepid = @keepid) where id = @keepId", new { keepId });
        }
        public int Delete(int id) {
            var vk = GetById(id);
            UpdateKeepCount(vk.KeepId);
            return _db.Execute("DELETE FROM vaultkeeps WHERE id = @id", new { id });
        }
    }
}