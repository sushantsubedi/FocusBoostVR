using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static System.Net.Mime.MediaTypeNames;

public class StartMenuManager : MonoBehaviour
{
    public GameObject Video;
    public GameObject Quiz;
    public GameObject PomodoroTimer;

    // Custom Function Start the whole game, quit the game
    public void OpenVideo()
    {
        Quiz.SetActive(false);
        Video.SetActive(true);
        PomodoroTimer.SetActive(true);
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            OpenVideo();
        }
    }
}
