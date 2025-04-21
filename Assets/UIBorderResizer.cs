using UnityEngine;

public class UIBorderResizer : MonoBehaviour
{
    public RectTransform leftBorder;
    public RectTransform rightBorder;
    public RectTransform topBorder;
    public RectTransform bottomBorder;

    [Range(0f, 1f)] public float borderWidthRatio = 0.2f;  // 20% of screen width
    [Range(0f, 1f)] public float borderHeightRatio = 0.2f; // 20% of screen height

    void Start()
    {
        ResizeBorders();
    }

    void ResizeBorders()
    {
        float canvasWidth = Screen.width;
        float canvasHeight = Screen.height;

        float borderWidth = canvasWidth * borderWidthRatio;
        float borderHeight = canvasHeight * borderHeightRatio;

        // Set Left Border
        leftBorder.anchorMin = new Vector2(0, 0);
        leftBorder.anchorMax = new Vector2(0, 1);
        leftBorder.pivot = new Vector2(0.5f, 0.5f);
        leftBorder.sizeDelta = new Vector2(borderWidth, 0);

        // Set Right Border
        rightBorder.anchorMin = new Vector2(1, 0);
        rightBorder.anchorMax = new Vector2(1, 1);
        rightBorder.pivot = new Vector2(0.5f, 0.5f);
        rightBorder.sizeDelta = new Vector2(borderWidth, 0);

        // Set Top Border
        topBorder.anchorMin = new Vector2(0, 1);
        topBorder.anchorMax = new Vector2(1, 1);
        topBorder.pivot = new Vector2(0.5f, 1);
        topBorder.sizeDelta = new Vector2(0, borderHeight);

        // Set Bottom Border
        bottomBorder.anchorMin = new Vector2(0, 0);
        bottomBorder.anchorMax = new Vector2(1, 0);
        bottomBorder.pivot = new Vector2(0.5f, 0);
        bottomBorder.sizeDelta = new Vector2(0, borderHeight);
    }
}
