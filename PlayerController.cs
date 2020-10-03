using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("")]
public class PlayerController : ControllerBase
{
    private readonly ILogger<PlayerController> _logger;
    private readonly IRepository _irepository;

    public PlayerController(ILogger<PlayerController> logger, IRepository irepository)
    {
        _logger = logger;
        _irepository = irepository;
    }

    [HttpPost]
    [Route("Create")]
    public async Task<Player> Create([FromBody] Player player)
    {
        DateTime cdate = DateTime.UtcNow;
        Player new_player = new Player();
        new_player.Name = player.Name;
        new_player.Id = Guid.NewGuid();
        new_player.Score = 0;
        new_player.Level = 0;
        new_player.IsBanned = false;
        new_player.CreationTime = cdate;
        new_player.items = new List<Item>();
        await _irepository.CreatePlayer(new_player);
        return null;
    }

    [HttpPost]
    [Route("GetAll")]
    public Task<Player[]> GetAll()
    {
        Task<Player[]> playerList = _irepository.GetAllPlayers();
        return playerList;
    }

    [HttpPost]
    [Route("Delete/{id:Guid}")]
    public async Task<Player> Delete(Guid id)
    {
        await _irepository.DeletePlayer(id);
        return null;
    }

    [HttpPost]
    [Route("Get/{id:Guid}")]
    public async Task<Player> Get(Guid id)
    {
        return await _irepository.GetPlayer(id);
    }

    [HttpPost]
    [Route("Modify/{id:Guid}")]
    public async Task<Player> Modify([FromBody] Player player)
    {
        await _irepository.ModifyPlayer(player);
        return null;
    }

    [HttpPost]
    [Route("Score/{minScore:int}")]
    public async Task<Player[]> Score(int minScore){
        return await _irepository.Score(minScore);
    }

    [HttpPost]
    [Route("{name:string}")]
    public async Task<Player> GetByName(string name){
        return await _irepository.GetByName(name);
    }

    [HttpPost]
    [Route("{id:Guid}")]
    public async Task<Player> GetById(Guid id)
    {
        return await _irepository.GetPlayer(id);
    }

    [HttpPost]
    [Route("Item/{level:int}")]
    public async Task<Player[]> PlayerItemLevel(int level){
        return await _irepository.PlayerItemLevel(level);
    }

    [HttpPost]
    [Route("Items/{size:int}")]
    public async Task<Player[]> ItemsSize(int size){
        return await _irepository.ItemsSize(size);
    }

    [HttpPost]
    [Route("Update/{name:string}")]
    public async Task<Player> UpdateName(string name){
        return await _irepository.UpdateName(name);
    }

    [HttpPost]
    [Route("Sorting")]
    public async Task<Player[]> GetAllPlayersByScore(){
        return await _irepository.GetAllPlayersByScore();
    }
}