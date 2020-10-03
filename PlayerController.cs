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
[Route("players")]
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
    public async Task<Player> Create([FromBody] NewPlayer newPlayer)
    {
        DateTime cdate = DateTime.UtcNow;
        Player new_player = new Player();
        new_player.Name = newPlayer.name;
        new_player.Id = Guid.NewGuid();
        new_player.Level = 1;
        new_player.Xp = 0;
        new_player.Money = 0;
        new_player.IsBanned = false;
        new_player.CreationTime = cdate;
        new_player.Inventory = new List<Item>();
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
    [Route("{id:Guid}/Ban")]
    public async Task<Player> Ban(Guid id)
    {
        await _irepository.BanPlayer(id);
        return null;
    }

    [HttpPost]
    [Route("{id:Guid}/Get")]
    public async Task<Player> Get(Guid id)
    {
        return await _irepository.GetPlayer(id);
    }

    [HttpPost]
    [Route("{id:Guid}/Modify/LevelUp")]
    public async Task<Player> Modify([FromBody] Player player)
    {
        await _irepository.ModifyPlayer(player);
        return null;
    }

    [HttpPost]
    [Route("{id:Guid}/Modify/LevelUp")]
    public async Task<Player> LevelUp(Guid id)
    {
        await _irepository.LevelUp(id);
        return null;
    }

    [HttpPost]
    [Route("{id:Guid}/Modify/Money/{amount:int}")]
    public async Task<Player> GetMoney(Guid id, int amount)
    {
        await _irepository.GetMoney(id,amount);
        return null;
    }

    [HttpPost]
    [Route("{id:Guid}/Modify/Xp/{amount:int}")]
    public async Task<Player> GetXp(Guid id, int amount)
    {
        await _irepository.GetXp(id,amount);
        return null;
    }
/* 
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
*/
}