using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public bool showOp, mute, resolution;
    public float audioSlider, volMute;
    public bool res1, res2, res3, res4, fullScreen, windowed;
    public AudioSource audi;

    // Use this for initialization
    void Start()
    {
        // Set the audi source
        audi = GameObject.Find("Audio Source").GetComponent<AudioSource>();
        if (PlayerPrefs.HasKey("mute"))
        {
            // If mute is 0, then audi's Audio Source is set to a float known as volume
            if (PlayerPrefs.GetInt("mute") == 0)
            {
                mute = false;
                audi.volume = PlayerPrefs.GetFloat("volume");
            }
            else
            {
                // Otherwise mute is set to 1, which becomes true
                mute = true;
                audi.volume = 0;
                volMute = PlayerPrefs.GetFloat("volume");
            }
        }
        res3 = true;
        fullScreen = true;
        audioSlider = audi.volume;
    }

    // Update is called once per frame
    void Update()
    {
        if (audi.volume != audioSlider)
        {
            audi.volume = audioSlider;
        }
    }

    void OnGUI()
    {
        // Screen apsect ratio
        float scrW = Screen.width / 16;
        float scrH = Screen.height / 9;

        GUI.Box(new Rect(0, 0, Screen.width, Screen.height), "");
        if (!showOp)
        {
            // Game title box
            GUI.Box(new Rect(4 * scrW, 1.8f * scrH, 8 * scrW, 2 * scrH), "Excalibronze");

            // Load scene via play button
            if (GUI.Button(new Rect(5 * scrW, 4 * scrH, 6 * scrW, scrH), "Play"))
            {
                SceneManager.LoadScene("Game Screen");
            }

            // When true, options is displayed
            if (GUI.Button(new Rect(5.5f * scrW, 5 * scrH, 5 * scrW, 0.75f * scrH), "Options"))
            {
                showOp = true;
            }

            // Quit the game
            if (GUI.Button(new Rect(6 * scrW, 5.75f * scrH, 4 * scrW, 0.5f * scrH), "Quit"))
            {
                Application.Quit();
            }
        }
        else
        {
            // ToggleMute
            if (GUI.Button(new Rect(8 * scrW, 1.2f * scrH, scrW, 0.5f * scrH), "Mute"))
            {
                ToggleMute();
            }

            // Audio Slider
            GUI.Box(new Rect(0.5f * scrW, 0.5f * scrH, 7.125f * scrW, 1f * scrH), "Audio"); // Title for audio 

            audioSlider = GUI.HorizontalSlider(new Rect(0.5f * scrW, 1.4f * scrH, 7.125f * scrW, 0.25f * scrH), audioSlider, 0.0F, 1.0F); // Audio Horizontal slider

            GUI.Label(new Rect(4f * scrW, 1f * scrH, 0.75f * scrW, 0.5f * scrH), Mathf.FloorToInt(audioSlider * 100).ToString()); // 0 out of 100

            if (GUI.Button(new Rect(14 * scrW, 8.5f * scrH, 2 * scrW, 0.5f * scrH), "Main Menu"))
            {
                SaveOptions();
                showOp = false;
            }

            // Resolution
            GUI.Box(new Rect(0.5f * scrW, 2.9f * scrH, 7.125f * scrW, 6 * scrH), "Resolutions");

            #region Resolution Types
            if (GUI.Toggle(new Rect(1.5f * scrW, 3.9f * scrH, 2.5f * scrW, 0.5f * scrH), res1, "1024 x 576"))
            {
                Screen.SetResolution(1024, 576, fullScreen); // Screen resolution is set to 1024 x 576, fullscreen

                res1 = true; // res1 is true, while all others are false

                res2 = false;

                res3 = false;

                res4 = false;
            }

            if (GUI.Toggle(new Rect(1.5f * scrW, 4.4f * scrH, 2.5f * scrW, 0.5f * scrH), res2, "1366 x 768"))
            {
                Screen.SetResolution(1366, 768, fullScreen); //Screen resolution is set to 1366 x 768, fullscreen

                res2 = true; // res2 is true, while all others are false

                res1 = false;

                res3 = false;

                res4 = false;
            }

            if (GUI.Toggle(new Rect(1.5f * scrW, 4.9f * scrH, 2.5f * scrW, 0.5f * scrH), res3, "1600 x 900"))
            {
                Screen.SetResolution(1600, 900, fullScreen); // 900p, fullscreen

                res3 = true; // res3 is true, all remainders are false

                res1 = false;

                res2 = false;

                res4 = false;
            }

            if (GUI.Toggle(new Rect(1.5f * scrW, 5.4f * scrH, 2.5f * scrW, 0.5f * scrH), res4, "1920 x 1080"))
            {
                Screen.SetResolution(1920, 1080, fullScreen); // 1080p, fullscreen

                res4 = true; // res4 is true, all remainders are false

                res1 = false;

                res2 = false;

                res3 = false;
            }
            #endregion

            if (GUI.Toggle(new Rect(5.5f * scrW, 3.9f * scrH, 2 * scrW, 0.5f * scrH), fullScreen, "FullScreen"))
            {
                Screen.fullScreen = true; // Fullscreen enabled when true

                fullScreen = true; // When true, GUI may be filled

                windowed = false; // When false window is unselected
            }

            if (GUI.Toggle(new Rect(5.5f * scrW, 4.4f * scrH, 2 * scrW, 0.5f * scrH), windowed, "Windowed"))//when pressed
            {
                Screen.fullScreen = false; // Set fullscreen to false

                fullScreen = false; // False, fullscreen unselected

                windowed = true; // When true, changes to windowed and GUI can be filled
            }
        }
    }

    void SaveOptions()
    {
        if (!mute)
        {
            // If not muted value is 0
            // False
            // Set volume to audioSlider
            PlayerPrefs.SetInt("mute", 0);
            PlayerPrefs.SetFloat("volume", audioSlider);
        }
        else
        {
            // Otherwise we are muted, value is 1
            // True
            // Set volume to volMute
            PlayerPrefs.SetInt("mute", 1);
            PlayerPrefs.SetFloat("volume", volMute);
        }
    }

    bool ToggleMute()
    {
        // If muted, our audioSlider is set to a placeholder volume value, then unmute
        if (mute)
        {
            audioSlider = volMute;
            mute = false;
            return false;
        }
        else
        {
            // Otherwise we are unmuted, volume is equal to that of our current volume, then mute
            volMute = audioSlider;
            audioSlider = 0;
            mute = true;
            return true;
        }
    }
}