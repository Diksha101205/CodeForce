/*
 * REAL AI DETECTOR HOOK
 * ---------------------
 * The React version used TensorFlow.js COCO-SSD:
 *   cocoSsd.load()
 *   model.detect(video)
 *
 * Unity cannot directly execute the browser TensorFlow.js model.
 * For the Unity version, export/obtain the detector as ONNX and import it
 * under Assets/Models. Then connect Unity Sentis (com.unity.ai.inference)
 * here. The rest of the app already uses the same Detection structure:
 * className + confidence + normalized bounding box.
 *
 * This file is deliberately kept as an integration point so the project
 * remains clean instead of pretending a placeholder detector is real AI.
 */
using UnityEngine;

public class SentisObjectDetector : MonoBehaviour
{
    [Tooltip("Place the exported COCO-SSD/YOLO ONNX model in Assets/Models and assign it here.")]
    public Object modelAssetPlaceholder;

    [Range(0f, 1f)]
    public float confidenceThreshold = 0.5f;

    public bool ModelAssigned => modelAssetPlaceholder != null;

    public void LogModelRequirement()
    {
        Debug.Log("Import an ONNX object-detection model and wire its preprocessing/postprocessing here.");
    }
}
