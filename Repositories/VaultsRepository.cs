using System.Collections.Generic;
using System.Data;
using System.Linq;
using keepr.Models;
using Dapper;

namespace keepr.Repositories
{
    public class VaultsRepository
    {
        private IDbConnection _db;
        public VaultsRepository(IDbConnection db)
        {
            _db = db;
        }

        public Vault GetById(int id)
        {
            return _db.Query<Vault>("SELECT * FROM vaults WHERE id = @id", new { id }).FirstOrDefault();
        }
        public IEnumerable<Vault> GetByUserId(string id)
        {
            return _db.Query<Vault>("SELECT * FROM vaults WHERE userid = @id", new { id });
        }
        public IEnumerable<Vault> GetAll()
        {
            return _db.Query<Vault>("SELECT * FROM vaults");
        }

        public IEnumerable<Vault> GetVaultByUserId(string id)
        {
            return _db.Query<Vault>(@"
            SELECT * FROM vaults
            WHERE userId = @id", new { id });
        }

        public IEnumerable<Keep> GetKeeps(int vaultId)
        {
            return _db.Query<Keep>(@"
            SELECT * FROM keeps 
            WHERE id in (
                SELECT keepid FROM vaultkeeps 
                WHERE vaultid = @vaultId
            )
            ", new {vaultId});
        }
        public Vault Create(Vault vault)
        {
            int id = _db.ExecuteScalar<int>(@"
            INSERT INTO vaults (name, description, userid)
            VALUES (@Name, @Description, @UserId);
            SELECT LAST_INSERT_ID();", vault
            );
            vault.Id = id;
            return vault;
        }
        public Vault Update(Vault vault)
        {
            _db.Execute(@"UPDATE vaults
            SET name = @Name, description = @Description
            WHERE id= @Id;", vault);
            return vault;
        }
        public bool Delete(int id)
        {
            int successfulDelete = _db.Execute("DELETE FROM vaults WHERE id = @id", new { id });
            return successfulDelete == 1;
        }
    }
}