namespace AdminConta.AuthAPI.Controllers;
using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Proyecto.PiteApi.Interfaces;
using Proyecto.PiteApi.Models;

[Route("api/[controller]")]
[ApiController]
public class UsersController : ControllerBase
{
    private IUserService _userService;
    private IMapper _mapper;
    private IConfiguration _configuration;

    public UsersController(IUserService userService, IConfiguration configuration, IMapper mapper) =>
        (_userService, _mapper, _configuration) = (userService, mapper, configuration);

    // GET: api/<UsersController>
    [HttpGet]
    public ActionResult<IEnumerable<User>> GetAll()
    {
        var users = _userService.GetAll();

        return Ok(users);
    }

    // GET api/<UsersController>/5
    [HttpGet("{id}")]
    public async Task<User> Get(Guid id)
    {
        return await _userService.GetByIdAsync(id);
    }

    // POST api/<UsersController>
    [HttpPost]
    public async Task<ActionResult<User>> Add([FromBody] User entity)
    {
        if (entity is null)
            return BadRequest(ModelState);

        var isValid = TryValidateModel(entity);
        if (!isValid)
            return BadRequest(ModelState);

        _userService.Add(entity);

        var result = await _userService.UnitOfWork.SaveChangesAsync();
        if (result <= 0)
            return BadRequest($"Tu usuario {entity.FullName}, no fue guardado.");

        return CreatedAtAction(nameof(Get), new { id = entity.Id }, entity);
    }

    // PUT api/<UsersController>/5
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] JsonPatchDocument<User> entity)
    {

        if (entity is null)
            return BadRequest(ModelState);

        var existEntity = await _userService.GetByIdAsync(id);
        if (existEntity is null)
            return NotFound($"El usuario con Id {id} no existe.");

        entity.ApplyTo(existEntity, ModelState);

        var isValid = TryValidateModel(existEntity);
        if (!isValid)
            return BadRequest(ModelState);

        try
        {
            await _userService.UnitOfWork.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            throw;
        }

        return NoContent();
    }

    // DELETE api/<UsersController>/5
    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(Guid id)
    {
        var entity = await _userService.GetByIdAsync(id);

        if (entity is null)
            return NotFound($"El usuario con Id {id} no existe.");

        _userService.Delete(entity);

        var result = await _userService.UnitOfWork.SaveChangesAsync();
        if (result <= 0)
            return BadRequest();

        return NoContent();
    }
}
