using UnityEngine;

public class ARCameraViewport : MonoBehaviour
{
    public Camera arCamera;

    void Start()
    {
        // Set viewport rect (x, y, width, height)
        arCamera.rect = new Rect(0.075f, 0.075f, 0.7f, 0.9f);	 // Adjust as per your frame size
    }
}
