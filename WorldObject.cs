using UnityEngine;

public class WorldObject : MonoBehaviour
{
    public string realWorldName;
    public string gameName;
    public GameObjectType objectType;
    public int hp;
    public int damage;
    public bool collectible;

    public void Setup(DetectedObject detectedObject, GameObjectData gameData)
    {
        realWorldName = detectedObject.objectName;
        gameName = gameData.gameName;
        objectType = gameData.type;
        hp = gameData.hp;
        damage = gameData.damage;
        collectible = gameData.collectible;
    }

    private void OnMouseDown()
    {
        if (collectible) Collect();
    }

    private void Collect()
    {
        InventoryManager inventory = FindObjectOfType<InventoryManager>();
        if (inventory != null) inventory.AddItem(this);
        Destroy(gameObject);
    }
}
