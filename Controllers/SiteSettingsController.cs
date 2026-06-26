using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NdisAgency.Data;
using NdisAgency.Models;

namespace NdisAgency.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class SiteSettingsController : ControllerBase
{
    private readonly SiteSettingRepository _repo;
    public SiteSettingsController(SiteSettingRepository repo) => _repo = repo;

    [HttpGet]
    public async Task<ActionResult<IEnumerable<SiteSetting>>> GetAll() =>
        Ok(await _repo.GetAllAsync());

    [HttpPut]
    public async Task<IActionResult> BulkUpdate([FromBody] List<SiteSetting> settings)
    {
        await _repo.BulkUpsertAsync(settings);
        return NoContent();
    }

    [HttpPut("{key}")]
    public async Task<IActionResult> Update(string key, [FromBody] SiteSetting setting)
    {
        setting.Key = key;
        await _repo.UpsertAsync(setting);
        return NoContent();
    }
}
