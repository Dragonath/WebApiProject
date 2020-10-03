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
* #### Create([FromBody] NewPlayer newPlayer)
	This function takes NewPlayer class from query body section and creates new level 1 player with unique guid and name from NewPlayer
* #### GetAll()
	Returns all players saved in database
* #### Get(Guid id)
	Finds and returns player with specific guid from database
* #### Ban(Guid id)
	Bans player, but doesnt remove it from database
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

![Image of Yaktocat](https://octodex.github.com/images/yaktocat.png)
