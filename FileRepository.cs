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

public class ListHolder
{
    public List<Player> list_players = new List<Player>();
}

public class FileRepository
{
    string fileName = "game-dev.txt";
    public async Task<Player> GetPlayer(Guid id)
    {
        ListHolder players = await ReadFile();

        foreach (var p in players.list_players)
        {
            if (p.Id == id)
            {
                return p;
            }
        }
        return null;
    }
    public async Task<Player[]> GetAllPlayers()
    {
        ListHolder players = await ReadFile();
        return players.list_players.ToArray();
    }
    public async Task<Player> CreatePlayer(Player player)
    {
        ListHolder players = await ReadFile();
        players.list_players.Add(player);
        File.WriteAllText(fileName, JsonConvert.SerializeObject(players));
        return player;
    }
    public async Task<Player> ModifyPlayer(Player player)
    {
        ListHolder players = await ReadFile();

        foreach (var p in players.list_players)
        {
            if (p.Id == player.Id)
            {
                p.Name = player.Name;
                p.Score = player.Score;
                p.Level = player.Level;
                p.IsBanned = player.IsBanned;
                p.items = player.items;
                p.CreationTime = player.CreationTime;
                File.WriteAllText(fileName, JsonConvert.SerializeObject(players));
                return p;
            }
        }
        return null;
    }
    public async Task<Player> DeletePlayer(Guid id)
    {
        ListHolder players = await ReadFile();

        for (int i = 0; i < players.list_players.Count; i++)
        {
            if (players.list_players[i].Id == id)
            {
                players.list_players.RemoveAt(i);
                File.WriteAllText(fileName, JsonConvert.SerializeObject(players));
                return null;
            }
        }
        return null;
    }

    public async Task<ListHolder> ReadFile()
    {
        var players = new ListHolder();
        string json = await File.ReadAllTextAsync(fileName);

        if (json.Length != 0)
        {
            return JsonConvert.DeserializeObject<ListHolder>(json);
        }

        return players;
    }

    public async Task<Item> CreateItem(Guid playerId, Item item)
    {
        ListHolder players = await ReadFile();

        foreach (var p in players.list_players)
        {
            if (p.Id == playerId)
            {
                p.items.Add(item);
                File.WriteAllText(fileName, JsonConvert.SerializeObject(players));
                return item;
            }
        }
        return null;
    }

    public async Task<Item> GetItem(Guid playerId, Guid itemId)
    {
        ListHolder players = await ReadFile();

        foreach (var p in players.list_players)
        {
            if (p.Id == playerId)
            {
                foreach (var i in p.items)
                {
                    if (i.Id == itemId)
                    {
                        return i;
                    }
                }
                return null;
            }
        }
        return null;
    }

    public async Task<Item[]> GetAllItems(Guid playerId)
    {
        ListHolder players = await ReadFile();

        foreach (var p in players.list_players)
        {
            if (p.Id == playerId)
            {
                return p.items.ToArray();
            }
        }
        return null;
    }

    public async Task<Item> ModifyItem(Guid playerId, Item item)
    {
        ListHolder players = await ReadFile();

        foreach (var p in players.list_players)
        {
            if (p.Id == playerId)
            {
                foreach (var i in p.items)
                {
                    if (i.Id == item.Id)
                    {
                        i.Level = item.Level;
                        i.CreationTime = item.CreationTime;
                        File.WriteAllText(fileName, JsonConvert.SerializeObject(players));
                        return i;
                    }
                }
                return null;
            }
        }
        return null;
    }

    public async Task<Item> DeleteItem(Guid playerId, Item item)
    {
        ListHolder players = await ReadFile();
        foreach (var p in players.list_players)
        {
            if (p.Id == playerId)
            {
                for (int i = 0; i < p.items.Count; i++)
                {
                    if (p.items[i].Id == item.Id)
                    {
                        p.items.RemoveAt(i);
                        File.WriteAllText(fileName, JsonConvert.SerializeObject(players));
                        return null;
                    }
                }
                return null;
            }
        }
        return null;
    }
}