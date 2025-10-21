using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using api.Data;
using api.Interfaces;
using api.Models;
using api.Models.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserBudgetController : ControllerBase
    {
        private readonly IUserBudgetRepository _budgetRepo;
        private readonly ApplicationDbContext _context;

        public UserBudgetController(IUserBudgetRepository budgetRepo, ApplicationDbContext context)
        {
            _context = context;
            _budgetRepo = budgetRepo;
        }

        [HttpPost]
        [Route("change")] //do poprawy, funkcja changeBudget musi coś zwracać zeby sprawdzić czy się powiodlo
        [Authorize]
        public async Task<IActionResult> ChangeMonthlyBudget([FromBody] decimal budget)
        {
            var getUser = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var changes = await _budgetRepo.ChangeBudget(getUser, budget);
            if (changes == true)
                return Ok($"Monthly budget is changed to: {budget}");

            return BadRequest();
        }

        [HttpPost]
        [Route("savings")]
        public async Task<IActionResult> ChangeSavings([FromBody] decimal savings)
        {
            var getUser = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            await _budgetRepo.ChangeSavings(getUser, savings);

            return Ok($"Monthly budget is changed to: {savings}");
        }

        [HttpGet]
        [Route("accountInfo")]
        [Authorize] //czy wykonywać sprawdzenie czy znaleziono
        public async Task<IActionResult> GetAccountInformation()
        {
            var getId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var user = await _context.UserDatas.FirstOrDefaultAsync(x => x.Id == getId);

            return Ok(user);
        }

        [HttpPost]
        [Route("expenses")]
        public async Task<IActionResult> ChangeExpenses([FromBody] decimal expenses)
        {
            var getUser = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            await _budgetRepo.ChangeSavings(getUser, expenses);

            return Ok($"Monthly budget is changed to: {expenses}");
        }
    }
}