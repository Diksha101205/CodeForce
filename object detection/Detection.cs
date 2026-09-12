using System;
using UnityEngine;

[Serializable]
public struct Detection
{
    public string className;
    [Range(0f, 1f)] public float confidence;
    // Normalized coordinates: x/y are bottom-left, width/height are normalized.
    public Rect box;

    public Detection(string name, float score, Rect normalizedBox)
    {
        className = name;
        confidence = score;
        box = normalizedBox;
    }
}
