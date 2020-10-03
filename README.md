# MongoDB Game Project

## Table of contents
* [General info](#general-info)
* [Technologies](#technologies)
* [Controllers](#controllers)

## General info
This project is database for multiplayer roleplaying game with many players, items and enemies. This project currently doesn't have actual working game behind it, and is only a prototype database for possible game idea we had.
	
## Technologies
Project is created with:
* Visual Studio Code
* Dot.NET WebAPI
* MongoDB
* Postman

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
    public bool IsBanned { get; set; }
    public DateTime CreationTime { get; set; }
    public List<Item> Inventory = new List<Item>();
}
```
Player class is the core of the project. Inventory is a list of Item class objects, Id is unique Guid, Name is name chosen by player, Level is players power and IsBanned boolean determines if player can play the game. 
* #### Create([FromBody] NewPlayer newPlayer)
	This function takes NewPlayer class from query body section and creates new level 1 player with unique guid and name from NewPlayer
* #### GetAll()
	Returns all players saved in database
* #### Get(Guid id)
	Finds and returns player with specific guid from database
* #### Ban(Guid id)
	Bans player, but doesn't remove it from the database
* #### LevelUp(Guid id)
	Raises players level by one

### Enemies
* #### Create([FromBody] NewEnemy enemy)
	Takes NewEnemy from body and creates new enemy with name and level from NewEnemy and creates new unique Guid, Damage and Hp stat
* #### GetAll()
	Returns all enemies saved in database
* #### Get(Guid id)
	Finds and returns enemy with matching guid from database
* #### Delete(Guid id)
	Removes enemy with matching guid from the enemy database

### Items


