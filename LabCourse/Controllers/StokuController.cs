using AutoMapper;
using LabCourse.Entities;
using LabCourse.Interfaces;
using LabCourse.Models;
using LabCourse.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;

namespace LabCourse.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class StokuController : ControllerBase
    {
        private readonly ILogger<StokuController> _logger;
        private readonly IStokuRepository _stokuRepository;
        private readonly IMapper _mapper;

        public StokuController(ILogger<StokuController> logger, IStokuRepository stokuRepository, IMapper mapper)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _stokuRepository = stokuRepository ?? throw new ArgumentNullException(nameof(stokuRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }


        [HttpGet("AllStocks")]

        public async Task<IActionResult> GetStocksAsync()
        {

            try
            {
                var stocks = await _stokuRepository.GetStocksAsync();

                if (stocks == null || !stocks.Any())
                {
                    return Ok(Enumerable.Empty<StokuDto>());
                }

                var stockDtos = _mapper.Map<IEnumerable<StokuDto>>(stocks);
                return Ok(stockDtos);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while getting all stocks.");
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
            };
        }   

        [HttpGet("StokuById/{id}")]
        public async Task<IActionResult> GetStokuDtosAsync(int id)
        {
            if (!await _stokuRepository.StockExistsAsync(id))
            {
                _logger.LogInformation($"Stock with id {id} wasn't found when trying to access stocks.");
                return NotFound();
            }

            var stocks = await _stokuRepository.GetStockByIdAsync(id);
            var stockDtos = _mapper.Map<StokuDto>(stocks);

            return Ok(stockDtos);
        }

        [HttpPost("AddStock")]
        public async Task<IActionResult> AddStockAsync(Stoku stokuDto)
        {
            if(stokuDto == null)
            {
                _logger.LogInformation("Recived null data");
                return BadRequest("Stock Data is required");
            }

            var newStockDto = await _stokuRepository.AddStockAsync(stokuDto);
            var newStockDtos = _mapper.Map<StokuDto>(newStockDto);

            return Ok(newStockDtos);
        }
        [HttpPut("UpdateStock/{id}")]
        public async Task<IActionResult> UpdateStockAsync( int id, [FromBody] StokuDtoForUpdate stokuDtoForUpdate)
        {
            // Check if the stock exists
            if (!await _stokuRepository.StockExistsAsync(id))
            {
                return NotFound();
            }

            // Retrieve the existing stock
            var existingStock = await _stokuRepository.GetStockByIdAsync(id);
            if (existingStock == null)
            {
                return NotFound();
            }

            // Map the updated data from the DTO to the existing stock entity
            _mapper.Map(stokuDtoForUpdate, existingStock);


            // Save changes to the database
            await _stokuRepository.SaveChangesAsync();

            return NoContent();

        }

        [HttpDelete("Delete{id}")]

        public async Task<IActionResult> DeleteStocks(int id)
        {
            if(!await _stokuRepository.StockExistsAsync(id))
            {
                return NotFound();
            }
            var stockEntity = await _stokuRepository.GetStockByIdAsync(id);
            if(stockEntity == null)
            {
                return NotFound();
            }

            _stokuRepository.DeleteStocks(stockEntity);
            await _stokuRepository.SaveChangesAsync();

            return NoContent();


        }

    }
}