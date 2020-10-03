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
    public async Task<Item> Create(Guid playerId, [FromBody] int level)
    {
        DateTime cdate = DateTime.UtcNow;
        Item new_item = new Item();
        new_item.Id = Guid.NewGuid();
        new_item.Level = level;
        new_item.CreationTime = cdate;

        await _irepository.CreateItem(playerId, new_item);
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

}