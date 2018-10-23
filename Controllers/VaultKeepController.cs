using System;
using System.Collections.Generic;
using keepr.Models;
using keepr.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace keepr.Controllers
{
  [Authorize]
  [Route("api/[controller]")]
  [ApiController]
  public class VaultkeepController : Controller
  {
    VaultkeepRepository _repo;
    public VaultkeepController(VaultkeepRepository repo)
    {
      _repo = repo;
    }

    [HttpGet]
    public IEnumerable<Vaultkeep> Get()
    {
      return _repo.GetAllUserVaultKeeps(HttpContext.User.Identity.Name);
    }

    [HttpGet("{id}")]
    public IEnumerable<Keep> getallkeepsinvault([FromRoute] int id)
    {
      return _repo.GetByVaultID(id);
    }

    [HttpPost]
    public Vaultkeep Post([FromBody] Vaultkeep vaultkeep)
    {
      if (ModelState.IsValid)
      {
        vaultkeep.UserId = HttpContext.User.Identity.Name;
        return _repo.Create(vaultkeep);
      }
      throw new Exception("INVALID VAULTKEEP");
    }

    [HttpPut]
    public Vaultkeep Put([FromBody] Vaultkeep vaultkeep)
    {
      return _repo.Update(vaultkeep);
    }

    [HttpDelete("{id}")]
    public void Delete([FromRoute] int id)
    {
      _repo.Delete(id);
    }
  }
}