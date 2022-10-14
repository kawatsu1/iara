using Iara.Services.DTOS;
using Iara.Services.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Iara.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CotacaoController : ControllerBase
    {
        private readonly ILogger<CotacaoController> _logger;
        private readonly ICotacaoService _cotacaoService;
        private readonly ICotacaoItemService _cotacaoItemService;

        public CotacaoController(ILogger<CotacaoController> logger, ICotacaoService cotacaoService, ICotacaoItemService cotacaoItemService)
        {
            _logger = logger;
            _cotacaoService = cotacaoService;
            _cotacaoItemService = cotacaoItemService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateCotacao([FromBody] CotacaoDto cotacao)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = await _cotacaoService.CreateAsync(cotacao);
                    if (result is null) return BadRequest("Verifique se os campos estão preenchidos corretamente");
                    return Ok(result);
                }
                else
                    return BadRequest();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateCotacao([FromBody] CotacaoDto cotacao)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var exists = await _cotacaoService.GetAsync(cotacao.Id);
                    if (exists is null) return NotFound();
                    var result = await _cotacaoService.UpdateAsync(cotacao);
                    if (result is null) return BadRequest("Verifique se os campos estão preenchidos corretamente");
                    return Ok(result);
                }
                else
                    return BadRequest();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetById(long id)
        {
            var cotacao = await _cotacaoService.GetAsync(id);
            if (cotacao is null) return NotFound("Cotação não encontrada");
            return Ok(cotacao);
        }

        [HttpGet]
        [Route("getAll")]
        public async Task<IActionResult> GetAll()
        {
            var cotacoes = await _cotacaoService.GetAllAsync();
            if (cotacoes.Count == 0) return NotFound("Nenhuma cotação encontrada");
            return Ok(cotacoes);
        }

        [HttpGet]
        [Route("search-by-cnpj/{cnpj}")]
        public async Task<IActionResult> GetByCNPJ(string cnpj)
        {
            var cotacoes = await _cotacaoService.SearchByNameAsync(cnpj);
            if (cotacoes.Count == 0) return NotFound("Nenhuma cotação encontrada");
            return Ok(cotacoes);
        }

        [HttpDelete]
        [Route("remove/{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            var cotacao = await _cotacaoService.GetAsync(id);
            if (cotacao is null) return NotFound();
            await _cotacaoService.RemoveAsync(id);
            return Ok("Removido com sucesso");
        }

        [HttpDelete]
        [Route("remove/cotacao-item/{id}")]
        public async Task<IActionResult> DeleteCotacaoItem(long id)
        {
            var cotacao = await _cotacaoItemService.GetAsync(id);
            if (cotacao is null) return NotFound();
            await _cotacaoItemService.RemoveAsync(id);
            return Ok("Removido com sucesso");
        }
    }
}