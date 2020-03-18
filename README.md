# Storage
Extended version of PlayerPrefs to make life easier

![](SceneExample.PNG)


### Setup:
1. Copy Assets/Storage.cs into your project


### How To Use:

##### Saving Data:
1. Use Storage.Save to save the data that you need

example: 
```csharp
// Primatives
Storage.instance.Save("playerName", "JamesGames")

// Lists
List<string> playerList = ...;
Storage.instance.SaveList("players", playerList);
```


##### Loading Data:
1. Use Storage.Load to load in the data that has previously been saved


example: 
```csharp
string playerName = Storage.instance.Load("playerName", "I need a new name!")
```

##### Loading Lists:
2. Use Storage.LoadList to load in the data that has previously been saved


example: 
```csharp
List<string> players = Storage.instance.LoadList<string>("players", new List<string>())
```

