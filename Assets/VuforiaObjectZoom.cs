using UnityEngine;
using UnityEngine.UI;
using Vuforia;
using TMPro;
using System.Collections; // Required for IEnumerator

public class VuforiaGroupZoom : MonoBehaviour
{
    public Camera arCamera;
    public Slider zoomSlider;
    public Transform parentModel;
    public float minZoom = 0.5f;
    public float maxZoom = 2f;
    public TextMeshProUGUI ComponentNameBox;

    public Button speakerButton; // UI Button for playing audio
    public Button pauseButton; // UI Button for pausing audio
    public AudioSource audioSource; // Audio source component

    private Vector3 originalScale;

    void Start()
    {
        if (parentModel != null)
        {
            originalScale = parentModel.localScale;
        }

        zoomSlider.minValue = 0;
        zoomSlider.maxValue = 1;
        zoomSlider.value = 0;
        zoomSlider.onValueChanged.AddListener(UpdateZoom);
        zoomSlider.gameObject.SetActive(false);

        // Hide speaker and pause buttons initially
        speakerButton.gameObject.SetActive(false);
        pauseButton.gameObject.SetActive(false);

        // Add button listeners
        speakerButton.onClick.AddListener(PlayAudio);
        pauseButton.onClick.AddListener(PauseAudio);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            DetectTappedObject(Input.mousePosition);
        }
        else if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                DetectTappedObject(touch.position);
            }
        }
    }

    void DetectTappedObject(Vector2 inputPosition)
    {
        Ray ray = arCamera.ScreenPointToRay(inputPosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            Transform selectedParent = hit.transform.parent;

            if (selectedParent != null)
            {
                // Reset previous model scale
                if (parentModel != null && parentModel != selectedParent)
                {
                    parentModel.localScale = originalScale;
                }

                parentModel = selectedParent;
                originalScale = parentModel.localScale;

                // Assign audio dynamically from the selected parent
                audioSource = selectedParent.GetComponent<AudioSource>();

                zoomSlider.gameObject.SetActive(true);
                ComponentNameBox.gameObject.SetActive(true);
                ComponentNameBox.text = parentModel.name;

                // Reset zoom slider
                zoomSlider.value = 0;

                // Show speaker button and hide pause button
                speakerButton.gameObject.SetActive(true);
                pauseButton.gameObject.SetActive(false);
            }
        }
    }





    void UpdateZoom(float value)
    {
        if (parentModel != null)
        {
            Debug.Log("Parent Model: " + parentModel.name);
            float scaleFactor = Mathf.Lerp(1f, maxZoom / originalScale.x, value);
            parentModel.localScale = originalScale * scaleFactor;
        }
    }



    void PlayAudio()
    {
        if (audioSource != null)
        {
            audioSource.Play();
            speakerButton.gameObject.SetActive(false);
            pauseButton.gameObject.SetActive(true);

            // Start checking when the audio finishes
            StartCoroutine(WaitForAudioToEnd());
        }
    }

    IEnumerator WaitForAudioToEnd()
    {
        yield return new WaitWhile(() => audioSource.isPlaying);

        // Once the audio finishes, reset the buttons
        speakerButton.gameObject.SetActive(true);
        pauseButton.gameObject.SetActive(false);
    }


    void PauseAudio()
    {
        if (audioSource != null)
        {
            if (audioSource.isPlaying)
            {
                audioSource.Pause();
                pauseButton.gameObject.SetActive(false);
                speakerButton.gameObject.SetActive(true);
            }
        }
    }
}