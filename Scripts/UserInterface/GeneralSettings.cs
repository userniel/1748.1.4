using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class GeneralSettings : MonoBehaviour
{
    public AudioMixer audioMixer;

    public Carousel m_quality;
    public Carousel m_resolution;
    public Toggle m_fullscreen;
    public Slider m_volume;

    private void Awake()
    {
        string[] resolutions = new string[Screen.resolutions.Length];
        int currentResolutionIndex = 0;
        for (int i = 0; i < resolutions.Length; i++)
        {
            resolutions[i] = Screen.resolutions[i].width + " X " + Screen.resolutions[i].height;
            if (Screen.currentResolution.width == Screen.resolutions[i].width && Screen.currentResolution.height == Screen.resolutions[i].height) currentResolutionIndex = i;
        }

        m_quality.m_options = QualitySettings.names;
        m_quality.m_index = QualitySettings.GetQualityLevel();

        m_resolution.m_options = resolutions;
        m_resolution.m_index = currentResolutionIndex;

        m_fullscreen.isOn = Screen.fullScreen;

        audioMixer.GetFloat("MasterVolume", out float currentVolume);
        m_volume.value = currentVolume;
    }

    public void SetQuality(int index)
    {
        QualitySettings.SetQualityLevel(index);
    }
    public void SetResolution(int index)
    {
        Screen.SetResolution(Screen.resolutions[index].width, Screen.resolutions[index].height, Screen.fullScreen);
    }
    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }
    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("MasterVolume", volume);
    }
}
