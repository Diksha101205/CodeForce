using UnityEngine;
using System.Collections.Generic;

public class ObjectMappingManager : MonoBehaviour
{
    public GameObjectMapping mappingDatabase;
    public Transform battlefieldParent;
    public List<GameObject> spawnedGameObjects = new List<GameObject>();

    public void ProcessDetection(DetectedObject detectedObject)
    {
        if (detectedObject.confidence < 0.50f) return;

        GameObjectData gameData = mappingDatabase.GetMapping(detectedObject.objectName);
        if (gameData == null)
        {
            Debug.Log("No mapping found for: " + detectedObject.objectName);
            return;
        }

        SpawnGameObject(detectedObject, gameData);
    }

    private void SpawnGameObject(DetectedObject detectedObject, GameObjectData gameData)
    {
        if (gameData.prefab == null)
        {
            Debug.LogWarning("Prefab missing for " + gameData.gameName);
            return;
        }

        Vector3 position = new Vector3(detectedObject.x, 0, detectedObject.y);
        GameObject obj = Instantiate(gameData.prefab, position, Quaternion.identity, battlefieldParent);

        WorldObject worldObject = obj.GetComponent<WorldObject>();
        if (worldObject != null)
            worldObject.Setup(detectedObject, gameData);

        spawnedGameObjects.Add(obj);
    }
}
