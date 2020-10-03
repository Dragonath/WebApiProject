using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
public class Player
{   
    
    public Guid Id { get; set; }
    public string Name { get; set; }
    public int Level { get; set; }
    public bool IsBanned { get; set; }
    public DateTime CreationTime { get; set; }
    public List<Item> Inventory = new List<Item>();

    public Helm helm { get; set;}
    public Chest chest { get; set; }
    public Legs legs { get; set; }
    public Boots boots { get; set; }
    public Sword sword { get; set; }
    public Shield shield { get; set; }
}