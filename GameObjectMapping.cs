using UnityEngine;

[CreateAssetMenu(fileName = "GameObjectMapping", menuName = "AI Battlefield/Game Object Mapping")]
public class GameObjectMapping : ScriptableObject
{
    [System.Serializable]
    public class Mapping
    {
        public string realWorldObject;
        public GameObjectData gameObject;
    }

    public Mapping[] mappings;

    public GameObjectData GetMapping(string objectName)
    {
        objectName = objectName.ToLower().Trim();
        foreach (Mapping mapping in mappings)
        {
            if (mapping.realWorldObject.ToLower().Trim() == objectName)
                return mapping.gameObject;
        }
        return null;
    }
}
