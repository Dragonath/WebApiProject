# MongoDB Game Project

## Table of contents
* [General info](#general-info)
* [Technologies](#technologies)
* [Databases](#databases)
* [Controllers](#controllers)

## General info
This project is database for multiplayer roleplaying game with many players, items and enemies. This project currently doesn't have actual working game behind it, and is only a prototype database for possible game idea we had. In the game, you control your own player character. You defeat enemies, level up and collect items dropped by enemies. You can sell items in market districts in any city. 
	
## Technologies
Project is created with:
* Visual Studio Code
* Dot.NET WebAPI
* MongoDB
* Postman

## Databases
We decided to use 2 databases in our implementation; one for players and one for enemies. Player database is considerably larger, because Player class has an Inventory which is a List of Item class objects, such as armors and weapons. Enemy database is used to save enemy types so it can be easily accessed and used by multiple enemies. 

** INSERT DRAW.IO KAAVIO **

## Controllers
This project uses 3 different controllers to create, modify and delete data from database. Each controller can do multiple things.
* [Players](#players)
* [Enemies](#enemies)
* [Items](#items)

### Players
```
public class Player
{   
    public Guid Id { get; set; }
    public string Name { get; set; }
    public int Level { get; set; }
    public int Xp { get; set; }
    public int Money { get; set; }
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
```
Player class is the core of the project. Inventory is a list of Item class objects, Id is unique Guid, Name is name chosen by player, Level is players power and IsBanned boolean determines if player can play the game. Player also has total experience and money. Equipping items is handled by Item type variables helm, chest, legs, boots, sword and shield. 
* #### Create([FromBody] NewPlayer newPlayer)
	This function takes NewPlayer class from query body section and creates new level 1 player with unique guid and name from NewPlayer
	```	
	[HttpPost]
	[Route("Create")]
	public async Task<Player> Create([FromBody] NewPlayer newPlayer)
	{
	    DateTime cdate = DateTime.UtcNow;
	    Player new_player = new Player();
	    new_player.Name = newPlayer.name;
	    new_player.Id = Guid.NewGuid();
	    new_player.Level = 1;
	    new_player.Xp = 0;
	    new_player.Money = 0;
	    new_player.IsBanned = false;
	    new_player.CreationTime = cdate;
	    new_player.Inventory = new List<Item>();
	    await _irepository.CreatePlayer(new_player);
	    return null;
	}
	```
* #### GetAll()
	Returns all players saved in database
* #### Get(Guid id)
	Finds and returns player with specific guid from database
* #### Ban(Guid id)
	Bans player, but doesn't remove it from the database
* #### LevelUp(Guid id)
	Raises players level by one
* #### GetMoney(Guid id, int amount)
	Raises players money by given amount
* #### GetXp(Guid id, int amount)
	Raises players total experience by given amount

### Enemies
```
public class Enemy
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public int Level { get; set; }
    public int Hp { get; set; }
    public int Damage { get; set; }
}
```
Enemy class is used to save enemy types. Single enemies do not have whole enemy classes for themselves in the database. Instead enemy in database is used to spawn multiple enemies with same attributes ingame. 
* #### Create([FromBody] NewEnemy enemy)
	Takes NewEnemy from body and creates new enemy with name and level from NewEnemy and creates new unique Guid, Damage and Hp stat
	```
	[HttpPost]
	[Route("Create")]
	public async Task<Item> Create([FromBody] NewEnemy enemy)
	{
	    Enemy new_enemy = new Enemy();
	    new_enemy.Id = Guid.NewGuid();
	    new_enemy.Name = enemy.Name;
	    new_enemy.Level = enemy.Level;
	    new_enemy.Damage = enemy.Level * 3;
	    new_enemy.Hp = enemy.Level * enemy.Level * 2;
	    await _irepository.CreateEnemy(new_enemy);
	    return null;
	}
	```
* #### GetAll()
	Returns all enemies saved in database
* #### Get(Guid id)
	Finds and returns enemy with matching guid from database
* #### Delete(Guid id)
	Removes enemy with matching guid from the enemy database

### Items
```
public class Item
{
    public itemType itemType { get; set; }
    public int armor { get;  set; }
    public int damage { get;  set; }
    public Guid Id { get; set; }
    [Range(0, 99)]
    public int Level { get; set; }
    public DateTime CreationTime { get; set; }
}
```
Item class saves items stats and adds itself to inventory of a player. Player has a level integer with model validation built in attribute [Range(int minimum, int maximum)]. Items have itemType variable that determines what kind of item it is. Type itemType uses enumeration to make it easier to read. 
```
public enum itemType
{
    Helm,
    Chest,
    Legs,
    Boots,
    Sword,
    Shield
}
```
* #### CreateItem(Guid playerId, [FromBody] NewItem newItem)
	Creates item with variables from body of query and adds it to specified players inventory
	```
	[HttpPost]
	[Route("Create")]
	public async Task<Item> CreateItem(Guid playerId, [FromBody] NewItem newItem)
	{   
	    if(newItem.type.Equals("Helm")){
		Item new_helm = new Item(); 
		new_helm.itemType = itemType.Helm;
		new_helm.Id = Guid.NewGuid();
		new_helm.Level = newItem.level;
		new_helm.armor = new_helm.Level * 4;
		new_helm.damage = 0;
		new_helm.CreationTime = DateTime.UtcNow;
		return await _irepository.CreateHelm(playerId, new_helm);
	    }
	    if(newItem.type.Equals("Chest")) {
		Item new_chest = new Item();
		new_chest.itemType = itemType.Chest;
		new_chest.Id = Guid.NewGuid();
		new_chest.Level = newItem.level;
		new_chest.armor = new_chest.Level * 8;
		new_chest.damage = 0;
		new_chest.CreationTime = DateTime.UtcNow;
		return await _irepository.CreateChest(playerId, new_chest);
	    }
	    if(newItem.type.Equals("Legs")) {
		Item new_legs = new Item();
		new_legs.itemType = itemType.Legs;
		new_legs.Id = Guid.NewGuid();
		new_legs.Level = newItem.level;
		new_legs.armor = new_legs.Level * 6;
		new_legs.damage = 0;
		new_legs.CreationTime = DateTime.UtcNow;
		return await _irepository.CreateLegs(playerId, new_legs);
	    }
	    if(newItem.type.Equals("Boots")) {
		Item new_boots = new Item();
		new_boots.itemType = itemType.Boots;
		new_boots.Id = Guid.NewGuid();
		new_boots.Level = newItem.level;
		new_boots.armor = new_boots.Level * 3;
		new_boots.damage = 0;
		new_boots.CreationTime = DateTime.UtcNow;
		return await _irepository.CreateBoots(playerId, new_boots);
	    }
	    if(newItem.type.Equals("Sword")) {
		Item new_sword = new Item();
		new_sword.itemType = itemType.Sword;
		new_sword.Id = Guid.NewGuid();
		new_sword.Level = newItem.level;
		new_sword.damage = new_sword.Level * 13;
		new_sword.armor = 0;
		new_sword.CreationTime = DateTime.UtcNow;
		return await _irepository.CreateSword(playerId, new_sword);
	    }
	    if(newItem.type.Equals("Shield")) {
		Item new_shield = new Item();
		new_shield.itemType = itemType.Shield;
		new_shield.Id = Guid.NewGuid();
		new_shield.Level = newItem.level;
		new_shield.armor = new_shield.Level * 11;
		new_shield.damage = 0;
		new_shield.CreationTime = DateTime.UtcNow;
		return await _irepository.CreateShield(playerId, new_shield);
	    }
	    return null;
	}
	```
* #### GetAll(Guid playerId)
	Returns all items in player with matching playerIds inventory
* #### Get(Guid playerId, Guid itemId)
	Finds and returns specific item in specific players inventory
* #### Delete(Guid playerId, Guid itemId)
	Removes item with matching guid from the players inventory
	
* ### Equip	
	Following functions work similarly to each other. They equip specific item from players inventory to that specific matching equipment slot.
	* EquipHelm(Guid playerId, [FromBody] Item item)
	* EquipChest(Guid playerId, [FromBody] Item item)
	* EquipLegs(Guid playerId, [FromBody] Item item)
	* EquipBoots(Guid playerId, [FromBody] Item item)
	* EquipSword(Guid playerId, [FromBody] Item item)
	* EquipShield(Guid playerId, [FromBody] Item item)
	```
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
	```

