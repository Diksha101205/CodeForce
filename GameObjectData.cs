using UnityEngine;

public enum GameObjectType
{
    Loot, Weapon, Enemy, Supply, Obstacle, Unknown
}

[System.Serializable]
public class GameObjectData
{
    public GameObjectType type;
    public string gameName;
    public Sprite icon;
    public GameObject prefab;
    public bool collectible;
    public int damage;
    public int hp;
    public string reward;
}
