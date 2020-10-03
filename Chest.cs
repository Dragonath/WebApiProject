using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
public class Chest : Item {
    public Guid Id { get; set; }
    [Range(0, 99)]
    public int Level { get; set; }
    public DateTime CreationTime { get; set; }
    public int armor { get; set; }
}