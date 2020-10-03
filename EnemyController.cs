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
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

[ApiController]
[Route("enemies")]

public class EnemyController : ControllerBase
{
    private readonly ILogger<EnemyController> _logger;
    private readonly IRepository _irepository;

    public EnemyController(ILogger<EnemyController> logger, IRepository irepository)
    {
        _logger = logger;
        _irepository = irepository;
    }

    [HttpPost] // {"Name" : "Orc", "Level" : 2}}
    [Route("Create")]
    public async Task<Item> Create([FromBody] NewEnemy enemy)
    {
        Enemy new_enemy = new Enemy();
        new_enemy.Id = Guid.NewGuid();
        new_enemy.Name = enemy.Name;
        new_enemy.Level = enemy.Level;
        new_enemy.Damage = enemy.Level * 3;
        new_enemy.Hp = enemy.Level * enemy.Level * 2;
        await _irepository.CreateEnemy(new_enemy);
        return null;
    }

    [HttpPost]
    [Route("GetAll")]
    public Task<Enemy[]> GetAll()
    {
        Task<Enemy[]> enemyList = _irepository.GetAllEnemies();
        return enemyList;
    }

    [HttpPost]
    [Route("{id:Guid}/Delete")]
    public async Task<Enemy> Delete(Guid id)
    {
        await _irepository.DeleteEnemy(id);
        return null;
    }

    [HttpPost]
    [Route("{id:Guid}/Get")]
    public async Task<Enemy> Get(Guid id)
    {
        return await _irepository.GetEnemy(id);
    }

}