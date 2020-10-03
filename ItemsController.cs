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
[Route("players/{playerId}/items")]
public class ItemsController : ControllerBase
{
    private readonly ILogger<ItemsController> _logger;
    private readonly IRepository _irepository;

    public ItemsController(ILogger<ItemsController> logger, IRepository irepository)
    {
        _logger = logger;
        _irepository = irepository;
    }

    [HttpPost] // {"Level" : 50, "Type" : 2}}
    [Route("Create")]
    public async Task<Item> CreateItem(Guid playerId, [FromBody] NewItem newItem)
    {   

        if(newItem.type.Equals("Helm")){
            Item new_helm = new Item(); 
            new_helm.itemType = itemType.Helm;
            new_helm.Id = Guid.NewGuid();
            new_helm.Level = newItem.level;
            new_helm.armor = new_helm.Level * 4;
            new_helm.damage = 0;
            new_helm.CreationTime = DateTime.UtcNow;
            return await _irepository.CreateHelm(playerId, new_helm);
        }
        if(newItem.type.Equals("Chest")) {
            Item new_chest = new Item();
            new_chest.itemType = itemType.Chest;
            new_chest.Id = Guid.NewGuid();
            new_chest.Level = newItem.level;
            new_chest.armor = new_chest.Level * 8;
            new_chest.damage = 0;
            new_chest.CreationTime = DateTime.UtcNow;
            return await _irepository.CreateChest(playerId, new_chest);
        }
        if(newItem.type.Equals("Legs")) {
            Item new_legs = new Item();
            new_legs.itemType = itemType.Legs;
            new_legs.Id = Guid.NewGuid();
            new_legs.Level = newItem.level;
            new_legs.armor = new_legs.Level * 6;
            new_legs.damage = 0;
            new_legs.CreationTime = DateTime.UtcNow;
            return await _irepository.CreateLegs(playerId, new_legs);
        }
        if(newItem.type.Equals("Boots")) {
            Item new_boots = new Item();
            new_boots.itemType = itemType.Boots;
            new_boots.Id = Guid.NewGuid();
            new_boots.Level = newItem.level;
            new_boots.armor = new_boots.Level * 3;
            new_boots.damage = 0;
            new_boots.CreationTime = DateTime.UtcNow;
            return await _irepository.CreateBoots(playerId, new_boots);
        }
        if(newItem.type.Equals("Sword")) {
            Item new_sword = new Item();
            new_sword.itemType = itemType.Sword;
            new_sword.Id = Guid.NewGuid();
            new_sword.Level = newItem.level;
            new_sword.damage = new_sword.Level * 13;
            new_sword.armor = 0;
            new_sword.CreationTime = DateTime.UtcNow;
            return await _irepository.CreateSword(playerId, new_sword);
        }
        if(newItem.type.Equals("Shield")) {
            Item new_shield = new Item();
            new_shield.itemType = itemType.Shield;
            new_shield.Id = Guid.NewGuid();
            new_shield.Level = newItem.level;
            new_shield.armor = new_shield.Level * 11;
            new_shield.damage = 0;
            new_shield.CreationTime = DateTime.UtcNow;
            return await _irepository.CreateShield(playerId, new_shield);
        }
        return null;
    }

    [HttpPost]
    [Route("GetAll/")]
    public Task<Item[]> GetAll(Guid playerId)
    {
        Task<Item[]> itemList = _irepository.GetAllItems(playerId);
        return itemList;
    }

    [HttpPost]
    [Route("Delete/{itemId:Guid}")]
    public async Task<Item> Delete(Guid playerId, Guid itemId)
    {
        Item item = new Item();
        item.Id = itemId;
        await _irepository.DeleteItem(playerId, item);
        return null;
    }

    [HttpPost]
    [Route("Get/{itemId:Guid}")]
    public async Task<Item> Get(Guid playerId, Guid itemId)
    {
        return await _irepository.GetItem(playerId, itemId);
    }

    [HttpPost]
    [Route("Modify")]
    public async Task<Item> Modify(Guid playerId, [FromBody] Item item)
    {
        await _irepository.ModifyItem(playerId, item);
        return null;
    }
    [HttpPost]
    [Route("Helm/Equip")]
    public async Task<Item> EquipHelm(Guid playerId, [FromBody] Item item)
     {
        return await _irepository.EquipHelm(playerId, item);
    }
    [HttpPost]
    [Route("Chest/Equip")]
    public async Task<Item> EquipChest(Guid playerId, [FromBody] Item item)
     {
        return await _irepository.EquipChest(playerId, item);
    }

    [HttpPost]
    [Route("Legs/Equip")]
    public async Task<Item> EquipLegs(Guid playerId, [FromBody] Item item)
    {
        return await _irepository.EquipLegs(playerId, item);
    }
    [HttpPost]
    [Route("Boots/Equip")]
    public async Task<Item> EquipBoots(Guid playerId, [FromBody] Item item)
    {
        return await _irepository.EquipBoots(playerId, item);
    }
    [HttpPost]
    [Route("Sword/Equip")]
    public async Task<Item> EquipSword(Guid playerId, [FromBody] Item item)
    {
        return await _irepository.EquipSword(playerId, item);
    }
        [HttpPost]
    [Route("Shield/Equip")]
    public async Task<Item> EquipShield(Guid playerId, [FromBody] Item item)
    {
        return await _irepository.EquipShield(playerId, item);
    }

}