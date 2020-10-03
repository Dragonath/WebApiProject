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
* [Items](#items)
* [Enemies](#enemies)

### Players
* #### Create([FromBody] NewPlayer newPlayer)
This function takes NewPlayer class from query body section and creates new level 1 player with unique guid and name from NewPlayer
### Items


### Enemies


![Image of Yaktocat](https://octodex.github.com/images/yaktocat.png)
