using UnityEngine;
using TMPro;
using UnityEngine.Video;

public class TimerController : MonoBehaviour
{
    public TextMeshProUGUI timerText;
    public GameObject warningPanel;
    public VideoPlayer videoPlayer;
    public AudioClip beepSound;
    private AudioSource audioSource;
    private float timer;
    private bool isTimerInStudyState = true;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = beepSound;
        InitializeTimer();
    }

    void Update()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
            UpdateTimerText();
        }
        else
        {
            // Toggle the visibility of the warning panel and handle VideoPlayer
            ToggleWarningVisibility();

            if (!isTimerInStudyState)
            {
                audioSource.Play();
            }

            // Set the appropriate timer duration and state
            timer = isTimerInStudyState ? 5f : 45f;
            isTimerInStudyState = !isTimerInStudyState;
            UpdateTimerText();
        }
    }

    void InitializeTimer()
    {
        timer = isTimerInStudyState ? 30f : 45f;
        UpdateTimerText();
    }

    void UpdateTimerText()
    {
        int seconds = Mathf.FloorToInt(timer);
        string stateMessage = isTimerInStudyState ? "" : "";
        timerText.text = stateMessage + " " + string.Format("{0:00}", seconds);
    }

    void ToggleWarningVisibility()
    {
        // Toggle the visibility of the warning panel
        warningPanel.SetActive(!warningPanel.activeSelf);

        // Handle VideoPlayer based on the current timer state
        if (isTimerInStudyState)
        {
            // If in the study state (30s), pause the VideoPlayer
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
