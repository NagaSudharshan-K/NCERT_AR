using UnityEngine;
using UnityEngine.UI;

public class ModelController : MonoBehaviour
{
    public float rotationSpeed = 100f;
    public float minZoom = 0.5f;
    public float maxZoom = 2.5f;
    public Slider zoomSlider;
    public Button rotateLeftButton;
    public Button rotateRightButton;
    public Transform targetImage; // Assign the Target Image Transform in Inspector

    private Vector3 initialScale;

    void Start()
    {
        if (targetImage == null)
        {
            Debug.LogError("Target Image Transform not assigned!");
            return;
        }

        initialScale = transform.localScale;

        // Assign Slider
        if (zoomSlider != null)
        {
            zoomSlider.minValue = minZoom;
            zoomSlider.maxValue = maxZoom;
            zoomSlider.value = 1f;
            zoomSlider.onValueChanged.AddListener(OnSliderValueChanged);
        }

        // Assign Rotation Buttons
        if (rotateLeftButton != null)
            rotateLeftButton.onClick.AddListener(RotateLeft);

        if (rotateRightButton != null)
            rotateRightButton.onClick.AddListener(RotateRight);
    }

    // Rotate Left (around target image's axis)
    private void RotateLeft()
    {
        RotateModel(rotationSpeed * Time.deltaTime);
    }

    // Rotate Right (around target image's axis)
    private void RotateRight()
    {
        RotateModel(-rotationSpeed * Time.deltaTime);
    }

    // Function to rotate the model around the target image's axis
    private void RotateModel(float angle)
    {
        Vector3 rotationAxis = targetImage.transform.up; // Rotating around the Y-axis of the target image
        transform.RotateAround(targetImage.position, rotationAxis, angle);
    }

    // Called when slider value changes
    private void OnSliderValueChanged(float value)
    {
        transform.localScale = initialScale * value;
    }
}
