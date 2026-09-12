using UnityEngine;
using System.Collections.Generic;

public class InventoryManager : MonoBehaviour
{
    public List<string> items = new List<string>();

    public void AddItem(WorldObject item)
    {
        items.Add(item.gameName);
        Debug.Log("Collected: " + item.gameName);
        UpdateInventoryUI();
    }

    private void UpdateInventoryUI()
    {
        Debug.Log("Inventory: " + string.Join(", ", items));
    }

    public bool HasItem(string itemName) => items.Contains(itemName);

    public void RemoveItem(string itemName)
    {
        if (items.Contains(itemName)) items.Remove(itemName);
        UpdateInventoryUI();
    }
}
