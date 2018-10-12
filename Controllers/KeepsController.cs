using System;
using System.Collections.Generic;
using keepr.Models;
using keepr.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace keepr.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KeepsController : Controller
    {
        KeepsRepository _repo;
        private readonly UserManager<User> manager;
        public KeepsController(KeepsRepository repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public IEnumerable<Keep> Get()
        {
            var user = HttpContext.User.Identity.Name;
            return _repo.GetAll();
        }

        [HttpGet("{id}/vaultCount")]
        public int GetVaultCount(int id)
        {
            return _repo.GetVaultCount(id);
        }

        [Authorize]
        [HttpPost]
        public Keep Post([FromBody] Keep rawKeep)
        {
            if (!ModelState.IsValid) throw new Exception("Not a valid Keep.");
            rawKeep.UserId = HttpContext.User.Identity.Name;
            Keep keep = _repo.Create(rawKeep);
            if (keep == null) throw new Exception("Error inserting keep into the db.");
            return keep;
        }

        [HttpGet("{id}/addView")]
        public int AddView(int id)
        {
            Keep keep = _repo.GetById(id);
            keep.Views++;
            _repo.Update(keep);
            return keep.Views;
        }

        [HttpGet("{id}/addShare")]
        public int AddShare(int id)
        {
            Keep keep = _repo.GetById(id);
            keep.Shares++;
            _repo.Update(keep);
            return keep.Shares;
        }


        [Authorize]
        [HttpDelete("{id}")]
        public bool Delete(int id)
        {
            return _repo.Delete(id);
        }
    }
};