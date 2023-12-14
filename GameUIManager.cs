using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class GameUIManager : MonoBehaviour
{
    public GameObject MainMenu;
    public GameObject Credits;
    public GameObject Settings;
    public GameObject StartMenu;
    public Material[] Themes; // array of material objects representing themes

    // Custom Function Start the whole game, quit the game
    public void StartGame() 
    { 
        // MainMenu.SetActive(false);
        Settings.SetActive(false);
        Credits.SetActive(false);
        StartMenu.SetActive(true); // Show start menu when start button pressed
    }

    // Quit Game
    public void QuitGame() 
    {
        Application.Quit();
    }

    // Toggle Credits Panel UI visibility
    public void DisplayCredits()
    {
        Credits.SetActive(!Credits.activeSelf);
    }

    // Toggle Settings Panel UI Visibility
    public void DisplaySettings()
    {
        Settings.SetActive(!Settings.activeSelf);
    }

    // Set theme based on index passed
    public void SetTheme(int themeNumber)
    {
        RenderSettings.skybox = Themes[themeNumber - 1];
    }

    // Update is called once per frame
    void Update()
    {    
        // For debugging, show start menu when enter pressed
        if (Input.GetKeyDown(KeyCode.Return))
        {
            StartMenu.SetActive(true);
            // StartMenu.transform.position = new Vector3(0.09f, 0.922f, 0.814f);
        }
    }
}
