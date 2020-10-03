using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
public interface IRepository
{
    //Task<Player> Get(Guid id);
    //Task<Player[]> GetAll();
    //Task<Player> Create(Player player);
    //Task<Player> Modify(Guid id, ModifiedPlayer player);
    //Task<Player> Delete(Guid id);

    Task<Player> CreatePlayer(Player player);
    Task<Player> GetPlayer(Guid playerId);
    Task<Player[]> GetAllPlayers();
    Task<Player> ModifyPlayer(Player player);
    Task<Player> BanPlayer(Guid playerId);

    Task<Item> CreateItem(Guid playerId, Item item);
    Task<Item> GetItem(Guid playerId, Guid itemId);
    Task<Item[]> GetAllItems(Guid playerId);
    Task<Item> ModifyItem(Guid playerId, Item item);
    Task<Item> DeleteItem(Guid playerId, Item item);

    Task<Player> GetByName(string name);
    Task<Player[]> PlayerItemLevel(int level);
    Task<Player[]> ItemsSize(int size);
    Task<Player> UpdateName(string name);
}