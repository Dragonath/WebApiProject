using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
public class Enemy
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public int Level { get; set; }
    public int Hp { get; set; }
    public int Damage { get; set; }
}