using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MongoDB.Bson.Serialization.Attributes;

public enum itemType
{
    Helm,
    Chest,
    Legs,
    Boots,
    Sword,
    Shield
}

public class Item
{
    public itemType itemType { get; set; }
    public int armor { get; set; }
    public int damage { get; set; }
    public Guid Id { get; set; }
    [Range(0, 99)]
    public int Level { get; set; }
    public DateTime CreationTime { get; set; }
}