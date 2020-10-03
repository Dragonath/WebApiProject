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

public class MongoDbRepository : IRepository
{
    private readonly IMongoCollection<Player> _playerCollection;
    private readonly IMongoCollection<BsonDocument> _bsonDocumentCollection;

    public MongoDbRepository()
    {
        var mongoClient = new MongoClient("mongodb://localhost:27017");
        var database = mongoClient.GetDatabase("game");
        _playerCollection = database.GetCollection<Player>("players");
        _bsonDocumentCollection = database.GetCollection<BsonDocument>("players");

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
    public async Task<Player> BanPlayer(Guid playerId)
    {
        var filter = Builders<Player>.Filter.Eq(player => player.Id, playerId);
        Player player = await _playerCollection.Find(filter).FirstAsync();
        player.IsBanned = true;
        return null;
    }
    public async Task<Item> CreateItem(Guid playerId, Item item)
    {

        var filter = Builders<Player>.Filter.Eq(player => player.Id, playerId);
        Player player = await _playerCollection.Find(filter).FirstAsync();
        if (player.Inventory != null)
        {
            player.Inventory.Add(item);
        }
        else
        {
            List<Item> itemit = new List<Item>();
            itemit.Add(item);
            player.Inventory = itemit;
        }
        await _playerCollection.ReplaceOneAsync(filter, player);
        return null;

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

    public async Task<Player> UpdateName(string name)
    {
        var filter = Builders<Player>.Filter.Eq(player => player.Name, name);
        await _playerCollection.FindOneAndUpdateAsync(filter, Builders<Player>.Update.Set(p => p.Name, name));
        return null;
    }

}