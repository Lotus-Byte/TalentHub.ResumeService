using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using ResumeBll.Contracts.Command;
using ResumeBll.Contracts.Interface;
using ResumeBll.Contracts.Dto;

namespace ResumeAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public sealed class ResumesController : ControllerBase
{
    private readonly IResume _resumeService;

    public ResumesController(IResume resumeService) =>
        _resumeService = resumeService;

    /// <summary>
    /// Поиск резюме 
    /// </summary>
    /// <param name="command"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    /// <example>POST: api/<ResumesController></example>
    [HttpPost("search")]
    public async Task<IReadOnlyCollection<ResumeDataDto>> Search(SearchCommand command, CancellationToken cancellationToken = default)
    {
        return await _resumeService.SearchAsync(command, cancellationToken);
    }

    /// <summary>
    /// Информация о резюме по идентификатору
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    /// <example>GET api/<ResumesController>/5</example>
    [HttpGet("{id}")]
    public async Task<ResumeDataDto> Get(int id, CancellationToken cancellationToken = default)
    {
        return await _resumeService.GetResumeDataAsync(id, cancellationToken);
    }

    /// <summary>
    /// Создание резюме
    /// </summary>
    /// <param name="command"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    /// <example>POST api/<ResumesController></example>
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] CreateResumeCommand command, CancellationToken cancellationToken = default)
    {
        var result = await _resumeService.CreateResumeAsync(command, cancellationToken);

        var ret = new CreatedResult();
        ret.Value = result;

        return ret;
    }

    /// <summary>
    /// Редактирование резюме
    /// </summary>
    /// <param name="command"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    /// <example>PUT api/<ResumesController>/5</example>
    [HttpPut]
    public async Task<IActionResult> Put([FromBody] EditResumeCommand command, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Удаление вакансии
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    /// <example>DELETE api/<ResumesController>/5</example>
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken = default)
    {
        await _resumeService.RemoveResumeAsync(id, cancellationToken);

        return new NoContentResult();
    }
}
