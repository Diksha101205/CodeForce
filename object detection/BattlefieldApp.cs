using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattlefieldApp : MonoBehaviour
{
    [Header("References")]
    public WebcamController webcam;
    public DetectionOverlay overlay;
    public Text statusText;
    public Text objectsText;
    public Button startButton;
    public Button stopButton;

    [Header("Detection")]
    [Range(0f, 1f)] public float confidenceThreshold = 0.5f;
    public bool demoMode = true;

    readonly List<Detection> detections = new List<Detection>();
    float demoTimer;

    void Start()
    {
        SetStatus("AI model ready — Unity camera system ready");
        if (startButton != null) startButton.onClick.AddListener(StartCamera);
        if (stopButton != null) stopButton.onClick.AddListener(StopCamera);
        if (stopButton != null) stopButton.gameObject.SetActive(false);
        UpdateObjectList();
    }

    public void StartCamera()
    {
        if (webcam == null) return;

        webcam.StartWebcam();
        SetStatus("Camera active — detecting objects");

        if (startButton != null) startButton.gameObject.SetActive(false);
        if (stopButton != null) stopButton.gameObject.SetActive(true);
    }

    public void StopCamera()
    {
        if (webcam != null) webcam.StopWebcam();
        detections.Clear();
        overlay?.Clear();
        UpdateObjectList();
        SetStatus("Camera stopped");

        if (startButton != null) startButton.gameObject.SetActive(true);
        if (stopButton != null) stopButton.gameObject.SetActive(false);
    }

    void Update()
    {
        if (webcam == null || !webcam.IsRunning) return;

        // This keeps the Unity port immediately runnable while the real ONNX detector
        // is being connected. Replace RunDemoDetection() with Sentis inference when
        // the COCO-SSD ONNX model is imported.
        if (demoMode)
        {
            demoTimer += Time.deltaTime;
            if (demoTimer > 0.75f)
            {
                demoTimer = 0;
                RunDemoDetection();
            }
        }
    }

    void RunDemoDetection()
    {
        // UI/integration test detections. They are intentionally labeled DEMO.
        detections.Clear();
        detections.Add(new Detection("person", 0.87f, new Rect(0.15f, 0.15f, 0.28f, 0.62f)));
        overlay?.Draw(detections, confidenceThreshold);
        UpdateObjectList();
        SetStatus("Camera active — DEMO detection (import ONNX for real AI)");
    }

    void UpdateObjectList()
    {
        if (objectsText == null) return;

        if (detections.Count == 0)
        {
            objectsText.text = "No objects detected yet.";
            return;
        }

        var lines = new List<string>();
        foreach (var d in detections)
            if (d.confidence >= confidenceThreshold)
                lines.Add($"{d.className}    {Mathf.RoundToInt(d.confidence * 100f)}%");

        objectsText.text = lines.Count == 0 ? "No objects detected yet." : string.Join("\n", lines);
    }

    void SetStatus(string value)
    {
        if (statusText != null) statusText.text = value;
    }

    void OnDestroy()
    {
        if (startButton != null) startButton.onClick.RemoveListener(StartCamera);
        if (stopButton != null) stopButton.onClick.RemoveListener(StopCamera);
    }
}
