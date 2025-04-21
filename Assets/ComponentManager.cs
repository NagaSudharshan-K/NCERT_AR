using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CellComponentManager : MonoBehaviour
{
    public GameObject[] cellComponents; // Assign all components in Inspector
    public TextMeshProUGUI componentNameText; // Assign TextMeshPro UI Text in Inspector
    public Image ComponentBGimage; // Assign UI Image background
    public Button audioButton; // Assign the UI button for audio (Play)
    public Button pauseButton; // Assign the UI button for pause
    private AudioSource currentAudioSource; // Store the active component's audio source

    public void ActivateComponent(string componentName)
    {
        currentAudioSource = null; // Reset audio source

        foreach (GameObject component in cellComponents)
        {
            bool isActive = component.name == componentName;
            component.SetActive(isActive);

            if (isActive)
            {
                if (componentNameText != null)
                {
                    componentNameText.text = componentName;
                    componentNameText.gameObject.SetActive(true);
                }

                if (ComponentBGimage != null)
                {
                    ComponentBGimage.gameObject.SetActive(true);
                }

                // Get the AudioSource from the active component
                currentAudioSource = component.GetComponent<AudioSource>();
            }
        }

        // Enable/Disable Audio and Pause Buttons based on whether the component has an AudioSource
        if (audioButton != null && pauseButton != null)
        {
            bool hasAudio = currentAudioSource != null;
            audioButton.gameObject.SetActive(hasAudio);
            pauseButton.gameObject.SetActive(false);
        }
    }

    public void PlayAudio()
    {
        if (currentAudioSource != null)
        {
            currentAudioSource.Play();
            // Swap button states
            audioButton.gameObject.SetActive(false);
            pauseButton.gameObject.SetActive(true);
        }
    }

    public void PauseAudio()
    {
        if (currentAudioSource != null && currentAudioSource.isPlaying)
        {
            currentAudioSource.Pause();
            // Swap button states
            audioButton.gameObject.SetActive(true);
            pauseButton.gameObject.SetActive(false);
        }
    }
}
