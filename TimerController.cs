using UnityEngine;
using TMPro;
using UnityEngine.Video;

public class TimerController : MonoBehaviour
{
    // Public variables to expose in Unity Editor
   public TextMeshProUGUI timerText; // variable for referencing the Text component for timer display
    public GameObject warningPanel; // variable for referencing the GameObject representing the warning panel
    public VideoPlayer videoPlayer; // variable for referencing the VideoPlayer component
    public AudioClip beepSound; // variable for the audio clip to be played as a beep

    // Private variables for internal use
    private AudioSource audioSource; // variable for the AudioSource component
    private float timer; // variable for tracking the timer value
    private bool isTimerInStudyState = true; // variable indicating whether the timer is in study state

    // Start is called before the first frame update
    void Start()
    {
        // Set up audio source and initialize the timer
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = beepSound;
        InitializeTimer();
    }

    // Update is called once per frame
    void Update()
    {
        // Check if the timer has not reached zerp
        if (timer > 0)
        {
            // Decrement timer and call UpdateTimerText() for display
            timer -= Time.deltaTime;
            UpdateTimerText();
        }
        else //If timer reaches 0
        {
            // Toggle warning visibility and handle VideoPlayer
            ToggleWarningVisibility();

            // Play beep sound if not in study state
            if (!isTimerInStudyState)
            {
                audioSource.Play();
            }

            // Set the appropriate timer duration by switching the study state
            timer = isTimerInStudyState ? 5f : 45f;
            isTimerInStudyState = !isTimerInStudyState;
            UpdateTimerText();
        }
    }

    // Initialize timer based on the initial state
    void InitializeTimer()
    {
        timer = isTimerInStudyState ? 5f : 45f;
        UpdateTimerText();
    }

    // Update the text displayed on the timer UI
    void UpdateTimerText()
    {
        int seconds = Mathf.FloorToInt(timer);
        timerText.text = string.Format("{0:00}", seconds);
    }

    // Toggle visibility of the warning panel and control VideoPlayer based on timer state
    void ToggleWarningVisibility()
    {
        // Toggle the visibility of the warning panel
        warningPanel.SetActive(!warningPanel.activeSelf);

        // Handle VideoPlayer based on the current timer state
        if (isTimerInStudyState)
        {
            // If in the study state (45s), pause the VideoPlayer
            videoPlayer.Pause();
        }
        else
        {
            // If in the break state (5s), toggle the VideoPlayer's playback
            if (videoPlayer.isPlaying)
                videoPlayer.Pause();
            else
                videoPlayer.Play();
        }
    }
}
