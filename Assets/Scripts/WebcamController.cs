using UnityEngine;
using UnityEngine.UI;

public class WebcamController : MonoBehaviour
{
    public RawImage webcamDisplay;

    private WebCamTexture webcamTexture;

    void Start()
    {
        WebCamDevice[] devices = WebCamTexture.devices;

        if (devices.Length == 0)
        {
            Debug.LogError("No webcam found.");
            return;
        }

        webcamTexture = new WebCamTexture(devices[0].name);
        webcamDisplay.texture = webcamTexture;
        webcamTexture.Play();

        Debug.Log("Webcam started: " + devices[0].name);
    }

    void OnDestroy()
    {
        if (webcamTexture != null && webcamTexture.isPlaying)
        {
            webcamTexture.Stop();
        }
    }
}