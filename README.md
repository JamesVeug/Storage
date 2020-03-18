# Storage
Wrapper for PlayerPrefs to add in additional functionality to make life easier

![](SceneExample.PNG)


### Setup:
1. Copy Assets/Storage.cs into your project



### Saving:
1. Use Storage.instance.Save to save the data that you need

example: 
```csharp
// Primatives
Storage.instance.Save("playerName", "JamesGames")
Storage.instance.Save("playerAge", 29)

public enum PlayerTypes{
  SinglePlayer = 0,
  Multiplayer = 1
}
Storage.instance.Save("playerType", PlayerTypes.SinglePlayer)

// Lists
List<string> playerList = ...;
Storage.instance.SaveList("players", playerList);
```


### Loading:
```csharp
// Primatives
string playerName = Storage.instance.Load("playerName", "I need a new name!")
int playerAge = Storage.instance.Load("playerAge", -1)
PlayerTypes playerType = Storage.instance.Load("playerType", PlayerTypes.SinglePlayer)

// Lists
List<string> players = Storage.instance.LoadList<string>("players", new List<string>())
```

