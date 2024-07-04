using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using CyMvc.Entities;
using CyMvc.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;


namespace CyMvc.Controllers
{
    [Authorize]
    public class ClientController : Controller
    {
        private readonly CymvcContext _context;

        public ClientController(CymvcContext client)
        {
            _context = client;
        }

        public async Task<IActionResult> Index(int id)
        {
            List<ClientInfoViewModel> clientViewModels = await _context.ClientInfos
                .Select(client => new ClientInfoViewModel
                {
                    Id = client.Id,
                    FirstName = client.FirstName,
                    LastName = client.LastName,
                    Address = client.Address,
                    ZipCode = client.ZipCode,
                    Birthday = client.Birthday,
                    Age = client.Age,
                    NameOfFather = client.NameOfFather,
                    NameOfMother = client.NameOfMother,
                    CivilStatus = client.CivilStatus,
                    Religion = client.Religion,
                    Occupation = client.Occupation,
                })
                .ToListAsync();

            return View(clientViewModels); 
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(ClientInfoViewModel model)
        {
            if (ModelState.IsValid)
            {
                var client = new ClientInfo
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Address = model.Address,
                    ZipCode = model.ZipCode,
                    Birthday = model.Birthday,
                    Age = model.Age,
                    NameOfFather = model.NameOfFather,
                    NameOfMother = model.NameOfMother,
                    CivilStatus = model.CivilStatus,
                    Religion = model.Religion,
                    Occupation = model.Occupation
                };

                _context.ClientInfos.Add(client);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }


        public async Task<IActionResult> Update(int id)
        {
    
            var client = await _context.ClientInfos.FindAsync(id); 
            if (client == null)
            {
                return NotFound();
            }

            var clientViewModel = new ClientInfoViewModel
            {
                Id = client.Id,
                FirstName = client.FirstName,
                LastName = client.LastName,
                Address = client.Address,
                ZipCode = client.ZipCode,
                Birthday = client.Birthday,
                Age = client.Age,
                NameOfFather = client.NameOfFather,
                NameOfMother = client.NameOfMother,
                CivilStatus = client.CivilStatus,
                Religion = client.Religion,
                Occupation = client.Occupation,
            };

            return View(clientViewModel);
        }


        [HttpPost]
        public ActionResult Update(ClientInfoViewModel clientinfo){

            if(!ModelState.IsValid)
            return View(clientinfo);

            ClientInfo s = new ClientInfo
            {
                Id = clientinfo.Id,
                FirstName = clientinfo.FirstName,
                LastName = clientinfo.LastName,
                Address = clientinfo.Address,
                ZipCode = clientinfo.ZipCode,
                Birthday = clientinfo.Birthday,
                Age = clientinfo.Age,
                NameOfFather = clientinfo.NameOfFather,
                NameOfMother = clientinfo.NameOfMother,
                CivilStatus = clientinfo.CivilStatus,
                Religion = clientinfo.Religion,
                Occupation = clientinfo.Occupation
            };

            _context.ClientInfos.Update(s);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }


        [HttpGet]
        public IActionResult Delete(int id)
        {
            try
            {   
                var clientLoans = _context.Loans.Where(l => l.ClientId == id);
                _context.Loans.RemoveRange(clientLoans);

                var client = _context.ClientInfos.Where(q => q.Id == id).FirstOrDefault();
                _context.ClientInfos.Remove(client);

                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index"); 
            }
        }
    }
}
