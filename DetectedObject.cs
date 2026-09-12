using UnityEngine;

[System.Serializable]
public class DetectedObject
{
    public string objectName;
    public float confidence;
    public float x, y, width, height;

    public DetectedObject(string name, float confidence, float x, float y, float width, float height)
    {
        this.objectName = name;
        this.confidence = confidence;
        this.x = x;
        this.y = y;
        this.width = width;
        this.height = height;
    }
}
