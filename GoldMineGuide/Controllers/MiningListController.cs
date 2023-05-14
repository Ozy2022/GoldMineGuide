using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GoldMineGuide.Models; //link with flower structure class
using GoldMineGuide.Data; //link with db class
using Microsoft.EntityFrameworkCore; //use the entityframework core sdk thing
using Microsoft.AspNetCore.Mvc.Rendering;
using Amazon;
using Amazon.SQS;
using Amazon.SQS.Model;
using Microsoft.Extensions.Configuration;
using System.IO;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Authorization;

namespace GoldMineGuide.Controllers
{

    [Authorize(Roles = "Staff")]
    public class MiningListController : Controller
    {
        private readonly GoldMineGuideContext _context;

        public MiningListController(GoldMineGuideContext context)
        {
            _context = context; //database

        }


        public async Task<IActionResult> Index(string MethodName, string msg = "") // Server-Based //string MiningName, string MiningType, string msg = ""
        {

            ViewBag.msg = msg;
       
            var Mininglist = from m in _context.Flower
                             select m;
            if(! string.IsNullOrEmpty(MethodName))
            {
                Mininglist = Mininglist.Where(s => s.Method_Name.Contains(MethodName));
            }
            return View(Mininglist);
        }

        // create finction for load form

        public IActionResult createFunction()
        {
            return View();
        }

        // create finction process form
        [HttpPost]
        public async Task<IActionResult> createFunction([Bind("Method_Name", "Mining_Place", "Process_Type", "Business_Type", "Mining_Produced_Date")] Flower mining)
        {
            if (ModelState.IsValid)
            {
                _context.Flower.Add(mining);
                await _context.SaveChangesAsync(); //save changes after adding a new item
            }
            else
            {
                return View(mining);
            }
            return RedirectToAction("Index", "MiningList");
        }

        public async Task<IActionResult> deleteFunction(String MethodName, int ? ID)
        {
            String Method_Name = MethodName;

            if(ID == null)
            {
                return NotFound();
            }
            try
            {
                var mining = await _context.Flower.FindAsync(ID);
                _context.Flower.Remove(mining);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "MiningList", new { msg = "Mine gold Method" + Method_Name + "is deleted now! " });
            }
            catch(Exception ex)
            {
                return RedirectToAction("Index", "MiningList", new { msg = "Mine gold Method" + Method_Name + "is unable to delete! Error:  " + ex.Message });
            }

        }

        public async Task<IActionResult> editFunction(int ?ID)
        {
            if(ID == null)
            {
                return NotFound();
            }
            var mining = await _context.Flower.FindAsync(ID);
            if(mining == null)
            {
                return NotFound();
            }
            return View(mining);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> editFunction(int? ID, [Bind("Mining_ID", "Method_Name", "Mining_Place", "Process_Type", "Business_Type", "Mining_Produced_Date")] Flower mining)
        {

            if(ModelState.IsValid)
            {
                try
                {
                    _context.Update(mining);
                    await _context.SaveChangesAsync();
                }
                catch(DbUpdateConcurrencyException ex)
                {
                    return BadRequest("Unable to update the mining of " + mining.Method_Name + ". Error: " + ex.Message);
                }

                return RedirectToAction(nameof(Index));
            }

            return View(mining);
        }



    }
    

}
