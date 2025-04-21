using UnityEngine;

public class ComponentClickHandler : MonoBehaviour
{
    public float zoomDuration = 1f; // Time to zoom into the object
    private Vector3 originalPosition;
    private Quaternion originalRotation;
    private Vector3 originalScale;
    private bool isZoomed = false;
    private Camera arCamera;

    void Start()
    {
        arCamera = Camera.main; // Use the AR Camera
        originalPosition = transform.position;
        originalRotation = transform.rotation;
        originalScale = transform.localScale;
    }

    void OnMouseDown()
    {
        if (!isZoomed)
        {
            StopAllCoroutines();
            StartCoroutine(ZoomToComponent());
        }
        else
        {
            ResetPosition();
        }
    }

    private System.Collections.IEnumerator ZoomToComponent()
    {
        isZoomed = true;
        Vector3 targetPosition = arCamera.transform.position + arCamera.transform.forward * 0.2f;
        Vector3 targetScale = originalScale * 2f;
        Quaternion targetRotation = Quaternion.LookRotation(arCamera.transform.forward);

        float elapsedTime = 0;
        while (elapsedTime < zoomDuration)
        {
            transform.position = Vector3.Lerp(originalPosition, targetPosition, elapsedTime / zoomDuration);
            transform.localScale = Vector3.Lerp(originalScale, targetScale, elapsedTime / zoomDuration);
            transform.rotation = Quaternion.Lerp(originalRotation, targetRotation, elapsedTime / zoomDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
    }

    private void ResetPosition()
    {
        StopAllCoroutines();
        transform.position = originalPosition;
        transform.rotation = originalRotation;
        transform.localScale = originalScale;
        isZoomed = false;
    }
}
