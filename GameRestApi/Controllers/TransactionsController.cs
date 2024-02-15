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
    public class TransactionsController : ControllerBase
    {
        #region Конструктор
        private readonly AppDbContext _db;
        private readonly IMapper _mapper;
        public TransactionsController(AppDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }
        #endregion

        #region Transact
        [HttpPost("Transact")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> TransactAsync([FromBody] GameTransactionDto gameTransactionDto)
        {
            //Валидация
            if (gameTransactionDto == null)
                return BadRequest("Транзакция пуста");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            //Найти и проверить игроков
            User? sender = await _db.Users.Where(x=>x.Id==gameTransactionDto.SenderId).FirstOrDefaultAsync();
            if (sender == null)
                return StatusCode(StatusCodes.Status500InternalServerError,"Отправитель не найден");

            User? receiver = await _db.Users.Where(x => x.Id == gameTransactionDto.ReceiverId).FirstOrDefaultAsync();
            if (receiver == null)
                return StatusCode(StatusCodes.Status500InternalServerError,"Получатель не найден");

            if (sender.Balance < gameTransactionDto.Summ)
                return StatusCode(StatusCodes.Status500InternalServerError,"У отправителя не достаточно средств");
            
            //Совершить транзакцию
            sender.Balance -= gameTransactionDto.Summ;
            receiver.Balance += gameTransactionDto.Summ;

            GameTransaction gameTransaction = _mapper.Map<GameTransaction>(gameTransactionDto);

            await _db.AddAsync(gameTransaction);
            await _db.SaveChangesAsync();

            return Ok("Транзакция прошла успешно");
        }
        #endregion
    }
}
