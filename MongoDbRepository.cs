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
using Newtonsoft.Json;
using MongoDB.Driver;
using MongoDB.Bson;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

public class MongoDbRepository : IRepository
{
    private readonly IMongoCollection<Player> _playerCollection;
    private readonly IMongoCollection<Enemy> _enemyCollection;
    private readonly IMongoCollection<BsonDocument> _bsonDocumentCollection;

    public MongoDbRepository()
    {
        var mongoClient = new MongoClient("mongodb://localhost:27017");
        var database = mongoClient.GetDatabase("game");
        _playerCollection = database.GetCollection<Player>("players");
        _bsonDocumentCollection = database.GetCollection<BsonDocument>("players");
        _enemyCollection = database.GetCollection<Enemy>("enemies");
    }

    public async Task<Player> CreatePlayer(Player player)
    {
        await _playerCollection.InsertOneAsync(player);
        return player;
    }
    public async Task<Player[]> GetAllPlayers()
    {
        var players = await _playerCollection.Find(new BsonDocument()).ToListAsync();
        return players.ToArray();
    }
    public Task<Player> GetPlayer(Guid id)
    {
        var filter = Builders<Player>.Filter.Eq(player => player.Id, id);
        return _playerCollection.Find(filter).FirstAsync();
    }
    public async Task<Player> ModifyPlayer(Player player)
    {
        FilterDefinition<Player> filter = Builders<Player>.Filter.Eq(p => p.Id, player.Id);
        await _playerCollection.ReplaceOneAsync(filter, player);
        return player;
    }

    public async Task<Player> LevelUp(Guid id)
    {
        var filter = Builders<Player>.Filter.Eq(player => player.Id, id);
        Player player = await _playerCollection.Find(filter).FirstAsync();
        player.Level += 1;
        return null;
    }

    public async Task<Player> BanPlayer(Guid playerId)
    {
        var filter = Builders<Player>.Filter.Eq(player => player.Id, playerId);
        Player player = await _playerCollection.Find(filter).FirstAsync();
        player.IsBanned = true;
        return null;
    }
    public async Task<Item> CreateHelm(Guid playerId, Item item)
    {
        var filter = Builders<Player>.Filter.Eq(player => player.Id, playerId);
        Player player = await _playerCollection.Find(filter).FirstAsync();
        player.Inventory.Add(item);
        await _playerCollection.ReplaceOneAsync(filter, player);
        return item;
    }   

    public async Task<Item> CreateChest(Guid playerId, Item item)
    {
        var filter = Builders<Player>.Filter.Eq(player => player.Id, playerId);
        Player player = await _playerCollection.Find(filter).FirstAsync();
        player.Inventory.Add(item);
                await _playerCollection.ReplaceOneAsync(filter, player);
        return item;
    }   
    public async Task<Item> CreateLegs(Guid playerId, Item item)
    {
        var filter = Builders<Player>.Filter.Eq(player => player.Id, playerId);
        Player player = await _playerCollection.Find(filter).FirstAsync();
        player.Inventory.Add(item);
        await _playerCollection.ReplaceOneAsync(filter, player);
        return item;
    }   
    public async Task<Item> CreateBoots(Guid playerId, Item item)
    {
        var filter = Builders<Player>.Filter.Eq(player => player.Id, playerId);
        Player player = await _playerCollection.Find(filter).FirstAsync();
        player.Inventory.Add(item);
        await _playerCollection.ReplaceOneAsync(filter, player);
        return item;
    }   
    public async Task<Item> CreateSword(Guid playerId, Item item)
    {
        var filter = Builders<Player>.Filter.Eq(player => player.Id, playerId);
        Player player = await _playerCollection.Find(filter).FirstAsync();
        player.Inventory.Add(item);
        await _playerCollection.ReplaceOneAsync(filter, player);
        return item;
    }   
    public async Task<Item> CreateShield(Guid playerId, Item item)
    {
        var filter = Builders<Player>.Filter.Eq(player => player.Id, playerId);
        Player player = await _playerCollection.Find(filter).FirstAsync();
        player.Inventory.Add(item);
        await _playerCollection.ReplaceOneAsync(filter, player);
        return item;
    }   

    public async Task<Item> GetItem(Guid playerId, Guid itemId)
    {
        var filter = Builders<Player>.Filter.Eq(p => p.Id, playerId);
        Player player = await _playerCollection.Find(filter).FirstAsync();
        if (player.Inventory != null)
        {
            for (int i = 0; i < player.Inventory.Count; i++)
            {
                if (player.Inventory[i].Id == itemId)
                {
                    return player.Inventory[i];
                }
            }
        }
        return null;


    }

    public async Task<Item[]> GetAllItems(Guid playerId)
    {
        var filter = Builders<Player>.Filter.Eq(player => player.Id, playerId);
        Player player = await _playerCollection.Find(filter).FirstAsync();
        if (player.Inventory != null)
        {
            return player.Inventory.ToArray();
        }
        else
        {
            return null;
        }
    }

    public async Task<Item> ModifyItem(Guid playerId, Item item)
    {
        var filter = Builders<Player>.Filter.Eq(p => p.Id, playerId);
        Player player = await _playerCollection.Find(filter).FirstAsync();
        if (player.Inventory != null)
        {
            for (int i = 0; i < player.Inventory.Count; i++)
            {
                if (player.Inventory[i].Id == item.Id)
                {
                    player.Inventory[i] = item;
                }
            }
            await _playerCollection.ReplaceOneAsync(filter, player);
        }
        return null;
    }

    public async Task<Item> DeleteItem(Guid playerId, Item item)
    {
        var filter = Builders<Player>.Filter.Eq(p => p.Id, playerId);
        Player player = await _playerCollection.Find(filter).FirstAsync();
        if (player.Inventory != null)
        {
            for (int i = 0; i < player.Inventory.Count; i++)
            {
                if (player.Inventory[i].Id == item.Id)
                {
                    player.Inventory.RemoveAt(i);
                }
            }
            await _playerCollection.ReplaceOneAsync(filter, player);
        }
        return null;
    }

    public Task<Player> GetByName(string name)
    {
        var filter = Builders<Player>.Filter.Eq(player => player.Name, name);
        return _playerCollection.Find(filter).FirstAsync();
    }

    public async Task<Player[]> PlayerItemLevel(int level)
    {
        var filter = Builders<Player>.Filter.ElemMatch<Item>(p => p.Inventory, Builders<Item>.Filter.Eq(i => i.Level, level));
        List<Player> players = await _playerCollection.Find(filter).ToListAsync();
        return players.ToArray();
    }

    public async Task<Player[]> ItemsSize(int size)
    {
        var filter = Builders<Player>.Filter.Eq(player => player.Inventory.Count, size);
        List<Player> players = await _playerCollection.Find(filter).ToListAsync();
        return players.ToArray();
    }

    public async Task<Item> EquipHelm(Guid playerId, Item item)
    {
        if(item.itemType != itemType.Helm) 
        {
            throw new WrongItemTypeException(System.Net.HttpStatusCode.NotAcceptable, "That item is not a helm");            
        }
        var filter = Builders<Player>.Filter.Eq(p => p.Id, playerId);
        Player player = await _playerCollection.Find(filter).FirstAsync();
        if (player.helm != null)
        {
            Item oldItem = player.helm;
            player.helm = item;
            player.Inventory.Add(oldItem);

            await _playerCollection.ReplaceOneAsync(filter, player);
            await DeleteItem(playerId, item);
        }
        else
        {
            player.helm = item;
            await _playerCollection.ReplaceOneAsync(filter, player);
            await DeleteItem(playerId, item);
        }
        return item;
    }
    public async Task<Item> EquipChest(Guid playerId, Item item)
    {
        if(item.itemType != itemType.Chest) 
        {
            throw new WrongItemTypeException(System.Net.HttpStatusCode.NotAcceptable, "That item is not a chest");            
        }
        var filter = Builders<Player>.Filter.Eq(p => p.Id, playerId);
        Player player = await _playerCollection.Find(filter).FirstAsync();
        if (player.chest != null)
        {
            Item oldItem = player.chest;
            player.chest = item;
            player.Inventory.Add(oldItem);

            await _playerCollection.ReplaceOneAsync(filter, player);
            await DeleteItem(playerId, item);
        }
        else
        {
            player.chest = item;
            await _playerCollection.ReplaceOneAsync(filter, player);
            await DeleteItem(playerId, item);
        }
        return item;
    }
    public async Task<Item> EquipLegs(Guid playerId, Item item)
    {
        if(item.itemType != itemType.Legs) 
        {
            throw new WrongItemTypeException(System.Net.HttpStatusCode.NotAcceptable, "That item is not legs");            
        }
        var filter = Builders<Player>.Filter.Eq(p => p.Id, playerId);
        Player player = await _playerCollection.Find(filter).FirstAsync();
        if (player.legs != null)
        {
            Item oldItem = player.legs;
            player.legs = item;
            player.Inventory.Add(oldItem);

            await _playerCollection.ReplaceOneAsync(filter, player);
            await DeleteItem(playerId, item);
        }
        else
        {
            player.legs = item;
            await _playerCollection.ReplaceOneAsync(filter, player);
            await DeleteItem(playerId, item);
        }
        return item;
    }
    public async Task<Item> EquipBoots(Guid playerId, Item item)
    {
        if(item.itemType != itemType.Boots) 
        {
            throw new WrongItemTypeException(System.Net.HttpStatusCode.NotAcceptable, "That item is not boots");            
        }
        var filter = Builders<Player>.Filter.Eq(p => p.Id, playerId);
        Player player = await _playerCollection.Find(filter).FirstAsync();
        if (player.boots != null)
        {
            Item oldBoots = player.boots;
            player.boots = item;
            player.Inventory.Add(oldBoots);

            await _playerCollection.ReplaceOneAsync(filter, player);
            await DeleteItem(playerId, item);
        }
        else
        {
            player.boots = item;
            await _playerCollection.ReplaceOneAsync(filter, player);
            await DeleteItem(playerId, item);
        }
        return item;
    }

    public async Task<Item> EquipSword(Guid playerId, Item item)
    {
        if(item.itemType != itemType.Sword) 
        {
            throw new WrongItemTypeException(System.Net.HttpStatusCode.NotAcceptable, "That item is not a sword");            
        }
        var filter = Builders<Player>.Filter.Eq(p => p.Id, playerId);
        Player player = await _playerCollection.Find(filter).FirstAsync();
        if (player.sword != null)
        {
            Item oldItem = player.sword;
            player.sword = item;
            player.Inventory.Add(oldItem);

            await _playerCollection.ReplaceOneAsync(filter, player);
            await DeleteItem(playerId, item);
        }
        else
        {
            player.sword = item;
            await _playerCollection.ReplaceOneAsync(filter, player);
            await DeleteItem(playerId, item);
        }
        return item;
    }
    public async Task<Item> EquipShield(Guid playerId, Item item)
    {

        if(item.itemType != itemType.Shield) {
            throw new WrongItemTypeException(System.Net.HttpStatusCode.NotAcceptable, "That item is not a shield");            
        }

        var filter = Builders<Player>.Filter.Eq(p => p.Id, playerId);
        Player player = await _playerCollection.Find(filter).FirstAsync();
        if (player.shield != null)
        {
            Item oldItem = player.shield;
            player.shield = item;
            player.Inventory.Add(oldItem);

            await _playerCollection.ReplaceOneAsync(filter, player);
            await DeleteItem(playerId, item);
        }
        else
        {
            player.shield = item;
            await _playerCollection.ReplaceOneAsync(filter, player);
            await DeleteItem(playerId, item);
        }
        return item;
    }
    public async Task<Enemy> CreateEnemy(Enemy newenemy)
    {
        await _enemyCollection.InsertOneAsync(newenemy);
        return newenemy;
    }
    public async Task<Enemy> DeleteEnemy(Guid id)
    {
        FilterDefinition<Enemy> filter = Builders<Enemy>.Filter.Eq(e => e.Id, id);
        return await _enemyCollection.FindOneAndDeleteAsync(filter);
    }
    public async Task<Enemy[]> GetAllEnemies()
    {
        var enemies = await _enemyCollection.Find(new BsonDocument()).ToListAsync();
        return enemies.ToArray();
    }
    public Task<Enemy> GetEnemy(Guid id)
    {
        var filter = Builders<Enemy>.Filter.Eq(e => e.Id, id);
        return _enemyCollection.Find(filter).FirstAsync();
    }
    public async Task<Player> GetMoney(Guid id, int amount){
        var filter = Builders<Player>.Filter.Eq(player => player.Id, id);
        Player player = await _playerCollection.Find(filter).FirstAsync();
        player.Money += amount;
        return null;
    }
    public async Task<Player> GetXp(Guid id, int amount){
        var filter = Builders<Player>.Filter.Eq(player => player.Id, id);
        Player player = await _playerCollection.Find(filter).FirstAsync();
        player.Xp += amount;
        return null;
    }

    public Task<Player> UpdateName(string name)
    {
        throw new NotImplementedException();
    }

    public async Task<Item> RemoveHelm(Guid playerId)
    {
        var filter = Builders<Player>.Filter.Eq(p => p.Id, playerId);
        Player player = await _playerCollection.Find(filter).FirstAsync();
        
        if(player.helm == null) {
            return null;
        }

        Item item = player.helm;
        player.Inventory.Add(item);
        player.helm = null;
        await _playerCollection.ReplaceOneAsync(filter, player);
        return item;
    }
        
    public async Task<Item> RemoveChest(Guid playerId)
    {
        var filter = Builders<Player>.Filter.Eq(p => p.Id, playerId);
        Player player = await _playerCollection.Find(filter).FirstAsync();
        
        if(player.chest == null) {
            return null;
        } 

        Item item = player.chest;
        player.Inventory.Add(item);
        player.chest = null;
        await _playerCollection.ReplaceOneAsync(filter, player);
        return item;
    }

    public async Task<Item> RemoveLegs(Guid playerId)
    {
        var filter = Builders<Player>.Filter.Eq(p => p.Id, playerId);
        Player player = await _playerCollection.Find(filter).FirstAsync();
                
        if(player.chest == null) {
            return null;
        } 
       
        Item item = player.legs;
        player.Inventory.Add(item);
        player.legs = null;
        await _playerCollection.ReplaceOneAsync(filter, player);
        return item;
    }

    public async Task<Item> RemoveBoots(Guid playerId)
    {
        var filter = Builders<Player>.Filter.Eq(p => p.Id, playerId);
        Player player = await _playerCollection.Find(filter).FirstAsync();
                
        if(player.chest == null) {
            return null;
        } 

        Item item = player.boots;
        player.Inventory.Add(item);
        player.boots = null;
        await _playerCollection.ReplaceOneAsync(filter, player);
        return item;
    }

    public async Task<Item> RemoveSword(Guid playerId)
    {
        var filter = Builders<Player>.Filter.Eq(p => p.Id, playerId);
        Player player = await _playerCollection.Find(filter).FirstAsync();
        
        if(player.chest == null) {
            return null;
        }   

        Item item = player.sword;
        player.Inventory.Add(item);
        player.sword = null;
        await _playerCollection.ReplaceOneAsync(filter, player);
        return item;
    }

    public async Task<Item> RemoveShield(Guid playerId)
    {
        var filter = Builders<Player>.Filter.Eq(p => p.Id, playerId);
        Player player = await _playerCollection.Find(filter).FirstAsync();
        
        if(player.chest == null) {
            return null;
        } 
        
        Item item = player.shield;
        player.Inventory.Add(item);
        player.shield = null;
        await _playerCollection.ReplaceOneAsync(filter, player);
        return item;
    }
}