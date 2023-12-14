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
    public Material[] Themes;


    private bool isMainMenuVisible = true; // Track the visibility of the main menu



    // Custom Function Start the whole game, quit the game
    public void StartGame() 
    { 
        // MainMenu.SetActive(false);
        Settings.SetActive(false);
        Credits.SetActive(false);
        StartMenu.SetActive(true);
    }

    public void QuitGame() 
    {
        Application.Quit();
    }

    public void DisplayCredits()
    {
        Credits.SetActive(!Credits.activeSelf);
    }

    public void DisplayMenu()
    {
        isMainMenuVisible = !isMainMenuVisible; // Toggle the visibility state

        // if (MainMenu != null)
        // {
        //     if (isMainMenuVisible)
        //     {
        //         // Move the main menu back to its original position or keep it visible
        //         // MainMenu.transform.position = new Vector3(0.09f, 0.922f, 0.814f); // Adjust this position
        //         // mainMenu.transform.Rotate(new Vector3(10f, 37f, 0f));
        //         
        //     }
        //     else
        //     {
        //         // Move the main menu to an off-screen location (e.g., far away from the camera)
        //         // MainMenu.transform.position = new Vector3(0.525f, 1f, 0f); // Example off-screen position
        //         // mainMenu.transform.Rotate(new Vector3(10f, 37f, 0f));
        //         MainMenu.transform.rotation = new
        //         Settings.SetActive(isMainMenuVisible); // Additional actions for other UI elements if needed
        //         Credits.SetActive(isMainMenuVisible);
        //     }
        // }
    }

    public void DisplaySettings()
    {
        Settings.SetActive(!Settings.activeSelf);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void SetTheme(int themeNumber)
    {
        RenderSettings.skybox = Themes[themeNumber - 1];
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            DisplayMenu();
            StartMenu.SetActive(true);
            // StartMenu.transform.position = new Vector3(0.09f, 0.922f, 0.814f);
        }

        if (Input.GetKeyDown(KeyCode.Escape) || OVRInput.GetDown(OVRInput.Button.One))
        {
            DisplayMenu();
        }

        if (Input.GetKeyDown(KeyCode.C) && isMainMenuVisible)
        {
            DisplayCredits();
        }

        if (Input.GetKeyDown(KeyCode.S) && isMainMenuVisible)
        {
            DisplaySettings();
        }

        if (Input.GetKeyDown(KeyCode.Alpha1)) {
            SetTheme(1);
        } else if (Input.GetKeyDown(KeyCode.Alpha2)) {
            SetTheme(2);
        } else if (Input.GetKeyDown(KeyCode.Alpha3)) {
            SetTheme(3);
        } else if (Input.GetKeyDown(KeyCode.Alpha4)) {
            SetTheme(4);
        } else if (Input.GetKeyDown(KeyCode.Alpha5)) {
            SetTheme(5);
        } else if (Input.GetKeyDown(KeyCode.Alpha6)) {
            SetTheme(6);
        } else if (Input.GetKeyDown(KeyCode.Alpha7)) {
            SetTheme(7);
        } else if (Input.GetKeyDown(KeyCode.Alpha8)) {
            SetTheme(8);
        } else if (Input.GetKeyDown(KeyCode.Alpha9)) {
            SetTheme(9);
        }
    }
}
