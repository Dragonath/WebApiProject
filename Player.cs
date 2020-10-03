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

    public Item helm { get; set;}
    public Item chest { get; set; }
    public Item legs { get; set; }
    public Item boots { get; set; }
    public Item sword { get; set; }
    public Item shield { get; set; }
}