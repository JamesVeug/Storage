# Storage
Extended version of PlayerPrefs to make life easier

![](SceneExample.PNG)


### Setup:
1. Copy Assets/Storage.cs into your project


### How To Use:

##### Saving Data:
1. Use Storage.Save to save the data that you need

example: 
Storage.instance.Save("playerName", "JamesGames")

##### Saving Lists:
1. Use Storage.SaveList to save the data that you need

example: 
List<string> playerList = ...;
Storage.instance.SaveList("players", playerList);


##### Loading Data:
1. Use Storage.Load to load in the data that has previously been saved


example: 
string playerName = Storage.instance.Load("playerName", "I need a new name!")

##### Loading Lists:
2. Use Storage.LoadList to load in the data that has previously been saved


example: 
List<string> players = Storage.instance.LoadList<string>("players", new List<string>())

