using System;
using System.Collections.Generic;
using keepr.Models;
using keepr.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace keepr.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class VaultsController : Controller
    {
        VaultsRepository _repo;
        private readonly UserManager<User> manager;
        public VaultsController(VaultsRepository repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public IEnumerable<Vault> Get()
        {
            return _repo.GetAll();
        }

        [HttpGet("byuser/{id}")]
        public IEnumerable<Vault> Get(string id)
        {
            return _repo.GetByUserId(id);
        }

        [HttpGet("{vaultId}/keeps")]
        public IEnumerable<Keep> Get(int vaultId)
        {
            return _repo.GetKeeps(vaultId);
        }

        [HttpPost]
       public Vault Post([FromBody] Vault rawVault)
       {
           if (!ModelState.IsValid) throw new Exception("Invalid Vault.");
           rawVault.UserId = HttpContext.User.Identity.Name;
           Vault vault = _repo.Create(rawVault);
           if (vault == null) throw new Exception("Error inserting Vault into the db.");
           return vault;
       }

        [Authorize]
       [HttpDelete("{id}")]
        public bool Delete(int id)
        {
            return _repo.Delete(id);
        }
   }
};