using UnityEngine;
using UnityEngine.UI;

public class WebcamManager : MonoBehaviour
{
    public RawImage cameraDisplay;
    private WebCamTexture webcam;

    private void Start()
    {
        WebCamDevice[] devices = WebCamTexture.devices;

        if (devices.Length == 0)
        {
            Debug.LogError("No webcam found.");
            return;
        }

        webcam = new WebCamTexture(devices[0].name);
        cameraDisplay.texture = webcam;
        webcam.Play();
    }

    private void OnDestroy()
    {
        if (webcam != null && webcam.isPlaying)
            webcam.Stop();
    }
}
