using UnityEngine;
using UnityEngine.UI;

public class SettingsController : MonoBehaviour
{
    [Header("UI Panels")]
    public GameObject mainPanel;      // Drag 'MainPanel' here
    public GameObject settingsPanel;  // Drag 'SettingsPanel' here

    [Header("Settings Controls")]
    public Slider volumeSlider;       // Drag your Audio Volume Slider here
    public Slider brightnessSlider;   // Drag your Brightness Slider here
    public Image brightnessOverlay;   // Drag a full-screen black UI Image (overlay) here

    [Header("Audio Settings")]
    public AudioSource bgmAudioSource; // Drag your Background Music AudioSource here (optional)

    void Start()
    {
        // Set initial slider values from AudioListener / standard default
        if (volumeSlider != null)
        {
            volumeSlider.value = AudioListener.volume;
            volumeSlider.onValueChanged.AddListener(SetVolume);
        }

        if (brightnessSlider != null)
        {
            brightnessSlider.onValueChanged.AddListener(SetBrightness);
            // Initialize overlay alpha based on slider starting value
            SetBrightness(brightnessSlider.value);
        }
    }

    // Opens Settings panel and hides Main panel
    public void OpenSettings()
    {
        if (mainPanel != null) mainPanel.SetActive(false);
        if (settingsPanel != null) settingsPanel.SetActive(true);
    }

    // Closes Settings panel and returns to Main panel
    public void CloseSettings()
    {
        if (settingsPanel != null) settingsPanel.SetActive(false);
        if (mainPanel != null) mainPanel.SetActive(true);
    }

    // Adjusts master volume (0.0 to 1.0)
    public void SetVolume(float value)
    {
        AudioListener.volume = value;
    }

    // Adjusts brightness using a dark UI overlay alpha (Slider value: 0.0 = bright, 0.8 = dark)
    public void SetBrightness(float value)
    {
        if (brightnessOverlay != null)
        {
            Color color = brightnessOverlay.color;
            // Invert value so 1 = fully bright (0 alpha) and 0 = dark (high alpha)
            color.a = 1f - Mathf.Clamp01(value);
            brightnessOverlay.color = color;
        }
    }
}