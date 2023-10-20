using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class Options : MonoBehaviour
{
    public AudioSource music;
    public AudioMixer audioMixer;
    public Dropdown resolutionDropdown;
    public Slider volumeSlider;
    public Slider musicSlider;
    [Space]
    float currentVolume;
    float currentMusic;
    public Image[] grapich_Images;
    public Image fullScreenImage;
    [Space]
    public Sprite BtnSmall;
    public Sprite BtnSmall_Select;
    [Space]
    public Sprite borderNormal;
    public Sprite borderHiglight;
    Resolution[] resolutions;
    private List<Resolution> list_resolution = new List<Resolution>();
    private bool isFullScreen;

    private void Start()
    {
        isFullScreen = Screen.fullScreen;
        if(isFullScreen)
        {
            fullScreenImage.sprite = borderHiglight;
        }
        else
        {
            fullScreenImage.sprite = borderNormal;
        }
        resolutionDropdown.ClearOptions();
        List<string> options = new List<string>();
        resolutions = Screen.resolutions;
        int currentResolutionIndex = 0;
        string curRes = "Error";

        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            if (!options.Contains(option))
            {
                options.Add(option);
                list_resolution.Add(resolutions[i]);

                if (resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)
                {
                    currentResolutionIndex = i;
                    curRes = option;
                }      
            }
        }

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.RefreshShownValue();
        resolutionDropdown.captionText.text = curRes;

        music = GameObject.FindGameObjectWithTag("Music").GetComponent<AudioSource>();
        float msc = music.volume;
        musicSlider.value = msc;
        float vol = GetMasterLevel();
        volumeSlider.value = vol;
    }

    float GetMasterLevel()
    {
        bool result = audioMixer.GetFloat("Master", out float value);
        if (result)
        {
            return value;
        }
        else
        {
            return 0f;
        }
    }

    public void SetMasterVolume(float volume)
    {
        audioMixer.SetFloat("Master", volume);
        currentVolume = volume;
    }
    public void SetMusicVolume(float volume)
    {
        music.volume = volume;
        currentMusic = volume;
    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = list_resolution[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    public void SetFullscreen()
    {
        if (isFullScreen)
        {
            Screen.fullScreen = false;
            isFullScreen = false;
            fullScreenImage.sprite = borderNormal;
        }
        else
        {
            Screen.fullScreen = true;
            isFullScreen = true;
            fullScreenImage.sprite = borderHiglight;
        }      
    }

    public void SetTextureQuality(int textureIndex)
    {
        QualitySettings.masterTextureLimit = textureIndex;
    }
    public void SetAntiAliasing(int aaIndex)
    {
        QualitySettings.antiAliasing = aaIndex;
    }

    public void SetQuality(int qualityIndex)
    {
        if (qualityIndex != 6) // if the user is not using 
                               //any of the presets
            QualitySettings.SetQualityLevel(qualityIndex);
        for (int i = 0; i < grapich_Images.Length; i++)
        {
            grapich_Images[i].sprite = BtnSmall;
        }

        switch (qualityIndex)
        {
            case 0: // quality level - very low
                grapich_Images[0].sprite = BtnSmall_Select;
                break;
            case 1: // quality level - low
                grapich_Images[1].sprite = BtnSmall_Select;
                break;
            case 2: // quality level - medium
                grapich_Images[2].sprite = BtnSmall_Select;
                break;
            case 3: // quality level - high (dont use)

                break;
            case 4: // quality level - high (very High)
                grapich_Images[3].sprite = BtnSmall_Select;
                break;
            case 5: // quality level - ultra
                grapich_Images[4].sprite = BtnSmall_Select;
                break;
        }
    }
}
