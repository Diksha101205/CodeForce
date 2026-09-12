using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DetectionOverlay : MonoBehaviour
{
    public RectTransform overlayRoot;
    public Font labelFont;

    readonly List<GameObject> activeBoxes = new List<GameObject>();

    public void Clear()
    {
        foreach (var go in activeBoxes)
            if (go != null) Destroy(go);
        activeBoxes.Clear();
    }

    public void Draw(IReadOnlyList<Detection> detections, float threshold)
    {
        Clear();
        if (overlayRoot == null) return;

        foreach (var d in detections)
        {
            if (d.confidence < threshold) continue;

            var box = new GameObject("DetectionBox");
            box.transform.SetParent(overlayRoot, false);

            var rect = box.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(d.box.xMin, d.box.yMin);
            rect.anchorMax = new Vector2(d.box.xMax, d.box.yMax);
            rect.offsetMin = Vector2.zero;
            rect.offsetMax = Vector2.zero;

            var image = box.AddComponent<Image>();
            image.color = d.className == "person"
                ? new Color(1f, 0.3f, 0.36f, 0.95f)
                : new Color(0.37f, 0.91f, 0.65f, 0.95f);

            var outline = box.AddComponent<Outline>();
            outline.effectColor = Color.black;
            outline.effectDistance = new Vector2(2, -2);

            var label = new GameObject("Label");
            label.transform.SetParent(box.transform, false);
            var labelRect = label.AddComponent<RectTransform>();
            labelRect.anchorMin = new Vector2(0, 1);
            labelRect.anchorMax = new Vector2(0, 1);
            labelRect.pivot = new Vector2(0, 1);
            labelRect.anchoredPosition = new Vector2(0, 0);
            labelRect.sizeDelta = new Vector2(220, 28);

            var text = label.AddComponent<Text>();
            text.font = labelFont != null ? labelFont : Resources.GetBuiltinResource<Font>("LegacyRuntime.ttf");
            text.fontSize = 15;
            text.fontStyle = FontStyle.Bold;
            text.color = Color.white;
            text.text = $"{d.className} {Mathf.RoundToInt(d.confidence * 100f)}%";
            text.horizontalOverflow = HorizontalWrapMode.Overflow;
            text.verticalOverflow = VerticalWrapMode.Overflow;

            activeBoxes.Add(box);
        }
    }
}
