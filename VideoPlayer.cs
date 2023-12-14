using System.Collections;
using UnityEngine;
using UnityEngine.Video;

public class CustomVideoPlayerController : MonoBehaviour
{
    public VideoClip[] videoclips;
    private VideoPlayer videoplayer;
    private int videoClipIndex;

    private MeshRenderer videoRenderer; // Assuming the video is displayed on a plane with a MeshRenderer

    private void Awake()
    {
        videoplayer = GetComponent<VideoPlayer>();
        videoRenderer = GetComponent<MeshRenderer>(); // Adjust this line based on your actual setup
    }

    // Start is called before the first frame update
    void Start()
    {
        videoplayer.clip = videoclips[0];

        // Check if the renderer exists before trying to access its material
        if (videoRenderer != null)
        {
            videoRenderer.material.mainTextureScale = new Vector2(-1, 1);
            videoRenderer.material.mainTextureOffset = new Vector2(1, 0); // Flip horizontally
        }
        else
        {
            UnityEngine.Debug.LogWarning("No MeshRenderer found on the GameObject.");
        }

        // Start playing the first video with a delay
        StartCoroutine(PlayVideoWithDelay(1f));
    }

    // Update is called once per frame
    void Update()
    {
        // Play next video clip
        if (Input.GetKeyDown(KeyCode.N))
        {
            PlayNext();
        }

        // Play previous video clip
        if (Input.GetKeyDown(KeyCode.V))
        {
            PlayPrevious();
        }

        // Toggle play/pause
        if (Input.GetKeyDown(KeyCode.B))
        {
            TogglePlayPause();
        }

        // Right controller primary hand trigger for PlayNext()
        if (OVRInput.GetDown(OVRInput.Button.PrimaryHandTrigger, OVRInput.Controller.RTouch))
        {
            PlayNext();
        }

        // Left controller primary hand trigger for PlayPrevious()
        if (OVRInput.GetDown(OVRInput.Button.PrimaryHandTrigger, OVRInput.Controller.LTouch))
        {
            PlayPrevious();
        }

        // Both controllers primary index triggers for TogglePlayPause()
        if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger, OVRInput.Controller.RTouch) ||
            OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger, OVRInput.Controller.LTouch))
        {
            TogglePlayPause();
        }


    }

    IEnumerator PlayVideoWithDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        // Play the current video
        PlayCurrentVideo();
    }

    public void PlayNext()
    {
        videoClipIndex++;
        if (videoClipIndex >= videoclips.Length)
        {
            videoClipIndex = videoClipIndex % videoclips.Length;
        }
        videoplayer.clip = videoclips[videoClipIndex];

        // Start playing the next video with a delay
        StartCoroutine(PlayVideoWithDelay(1f));
    }

    public void PlayPrevious()
    {
        videoClipIndex--;
        if (videoClipIndex < 0)
        {
            videoClipIndex = videoclips.Length - 1;
        }
        videoplayer.clip = videoclips[videoClipIndex];

        // Start playing the previous video with a delay
        StartCoroutine(PlayVideoWithDelay(1f));
    }

    public void TogglePlayPause()
    {
        if (videoplayer.isPlaying)
        {
            videoplayer.Pause();
        }
        else
        {
            videoplayer.Play();
        }
    }

    void PlayCurrentVideo()
    {
        // Logic to set up and play the current video
        videoplayer.Play();
    }

}
