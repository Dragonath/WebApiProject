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
    Task<Player> LevelUp(Guid id);
    Task<Player> BanPlayer(Guid playerId);

    Task<Item> CreateHelm(Guid playerId, Item item);
    Task<Item> CreateChest(Guid playerId, Item item);
    Task<Item> CreateLegs(Guid playerId, Item item);
    Task<Item> CreateBoots(Guid playerId, Item item);
    Task<Item> CreateSword(Guid playerId, Item item);
    Task<Item> CreateShield(Guid playerId, Item item);
    Task<Item> GetItem(Guid playerId, Guid itemId);
    Task<Item[]> GetAllItems(Guid playerId);
    Task<Item> ModifyItem(Guid playerId, Item item);
    Task<Item> DeleteItem(Guid playerId, Item item);
    Task<Item> EquipHelm(Guid playerId, Item item);
    Task<Item> EquipChest(Guid playerId, Item item);
    Task<Item> EquipLegs(Guid playerId, Item item);
    Task<Item> EquipBoots(Guid playerId, Item item);
    Task<Item> EquipSword(Guid playerId, Item item);
    Task<Item> EquipShield(Guid playerId, Item item);
    Task<Item> RemoveHelm(Guid playerId);
    Task<Item> RemoveChest(Guid playerId);
    Task<Item> RemoveLegs(Guid playerId);
    Task<Item> RemoveBoots(Guid playerId);
    Task<Item> RemoveSword(Guid playerId);
    Task<Item> RemoveShield(Guid playerId);

    Task<Player> GetByName(string name);
    Task<Player[]> PlayerItemLevel(int level);
    Task<Player[]> ItemsSize(int size);
    Task<Enemy> CreateEnemy(Enemy newenemy);
    Task<Enemy> DeleteEnemy(Guid id);
    Task<Enemy> GetEnemy(Guid id);
    Task<Enemy[]> GetAllEnemies();
    Task<Player> GetMoney(Guid id, int amount);
    Task<Player> GetXp(Guid id, int amount);
}

