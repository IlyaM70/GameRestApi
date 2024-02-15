using AutoMapper;
using GameRestApi.Data;
using GameRestApi.Model;
using GameRestApi.Model.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GameRestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MatchController : ControllerBase
    {
        #region Конструктор
        private readonly AppDbContext _db;
        private readonly IMapper _mapper;
        private readonly TransactionsController _transactionsController;
        public MatchController(AppDbContext db, IMapper mapper,
            TransactionsController transactionsController)
        {
            _db = db;
            _mapper = mapper;
            _transactionsController = transactionsController;
        }
        #endregion

        #region ExecuteMatch
        [HttpPost("ExecuteMatch")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ExecuteMatchAsync([FromBody] Match match)
        {
            //Валидация
            if (match == null)
                return BadRequest("Матч пуст");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            #region Найти и проверить игроков
            User? playerOne = await _db.Users.Where(x => x.Id == match.PlayerOneId).FirstOrDefaultAsync();
            if (playerOne == null)
                return StatusCode(StatusCodes.Status500InternalServerError, "Игрок 1 не найден");

            User? playerTwo = await _db.Users.Where(x => x.Id == match.PlayerTwoId).FirstOrDefaultAsync();
            if (playerTwo == null)
                return StatusCode(StatusCodes.Status500InternalServerError, "Игрок 2 не найден");

            if (playerOne.Balance < match.Rate)
                return StatusCode(StatusCodes.Status500InternalServerError, "У игрока 1 не достаточно средств");

            if (playerTwo.Balance < match.Rate)
                return StatusCode(StatusCodes.Status500InternalServerError, "У игрока 2 не достаточно средств");
            
            #endregion

            #region Определить победителя
            User winner;
            User loser;

            int playerOneNumber = 0;

            Random rnd = new Random();
            int winnerNumber = rnd.Next(2);

            if (winnerNumber == playerOneNumber)
            {
                winner = playerOne;
                loser = playerTwo;
            }
            else
            {
                winner = playerTwo;
                loser = playerOne;
            }
            #endregion

            #region Проигравший платит победителю
            ObjectResult result;
            GameTransactionDto gameTransactionDto = new()
            {
                Summ = match.Rate,
                ReceiverId = winner.Id,
                SenderId = loser.Id
            };

            result = (ObjectResult)await _transactionsController.TransactAsync(gameTransactionDto);
            if (result is not OkObjectResult)
                return StatusCode((int)result!.StatusCode, result.Value);
            #endregion

            // Записать историю матча
            MatchHistory matchHistory = new()
            {
                WinnerId = winner.Id,
                LoserId = loser.Id,
                Rate = match.Rate
            };

            await _db.AddAsync(matchHistory);
            await _db.SaveChangesAsync();

            return Ok($"Победил {winner.Name}. Проигравший {loser.Name} заплатил {match.Rate}");
        }
        #endregion
    }
}
