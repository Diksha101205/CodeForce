using UnityEngine;
using UnityEngine.UI;

public class WebcamController : MonoBehaviour
{
    public RawImage target;
    public AspectRatioFitter aspectFitter;

    public int requestedWidth = 1280;
    public int requestedHeight = 720;

    public WebCamTexture Webcam { get; private set; }
    public bool IsRunning => Webcam != null && Webcam.isPlaying;

    public void StartWebcam()
    {
        if (IsRunning) return;

        if (WebCamTexture.devices.Length == 0)
        {
            Debug.LogError("No webcam found.");
            return;
        }

        Webcam = new WebCamTexture(WebCamTexture.devices[0].name, requestedWidth, requestedHeight, 30);
        if (target != null) target.texture = Webcam;
        Webcam.Play();
    }

    public void StopWebcam()
    {
        if (Webcam != null)
        {
            Webcam.Stop();
            Destroy(Webcam);
            Webcam = null;
        }
    }

    void Update()
    {
        if (!IsRunning || target == null) return;

        float ratio = (float)Webcam.width / Mathf.Max(1, Webcam.height);
        if (aspectFitter != null) aspectFitter.aspectRatio = ratio;

        // WebCamTexture can report vertical mirroring on some cameras.
        target.uvRect = new Rect(
            Webcam.videoVerticallyMirrored ? 0 : 0,
            Webcam.videoVerticallyMirrored ? 1 : 0,
            1,
            Webcam.videoVerticallyMirrored ? -1 : 1
        );
    }

    void OnDestroy() => StopWebcam();
}
