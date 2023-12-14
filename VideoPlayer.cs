using System.Collections;
using UnityEngine; 
using UnityEngine.Video; 

public class CustomVideoPlayerController : MonoBehaviour
{
    public VideoClip[] videoclips; // Public array to hold VideoClips
    private VideoPlayer videoplayer; // Private variable for the VideoPlayer component
    private int videoClipIndex; // Private variable to keep track of the current video clip index

    private MeshRenderer videoRenderer; // Private variable for the MeshRenderer component since displayed on 3D cube

    private void Awake() // Awake is called when the script instance is being loaded
    {
        videoplayer = GetComponent<VideoPlayer>(); // Get the VideoPlayer component attached to the same GameObject
        videoRenderer = GetComponent<MeshRenderer>(); // Get the MeshRenderer component attached to the same GameObject
    }

    // Start is called before the first frame update
    void Start()
    {
        videoplayer.clip = videoclips[0]; // Set the initial video clip

        // Check if the renderer exists before trying to access its material
        if (videoRenderer != null)
        {
            videoRenderer.material.mainTextureScale = new Vector2(-1, 1); // Flip the video horizontally
            videoRenderer.material.mainTextureOffset = new Vector2(1, 0);
        }
        else
        {
            UnityEngine.Debug.LogWarning("No MeshRenderer found on the GameObject."); // Log a warning if no MeshRenderer is found
        }

        // Start playing the first video with a delay of 1 second
        StartCoroutine(PlayVideoWithDelay(1f));
    }

    // Update is called once per frame
    void Update()
    {
        // Right controller primary hand trigger for PlayNext() in Oculus VR
        if (OVRInput.GetDown(OVRInput.Button.PrimaryHandTrigger, OVRInput.Controller.RTouch))
        {
            PlayNext();
        }

        // Left controller primary hand trigger for PlayPrevious() in Oculus VR
        if (OVRInput.GetDown(OVRInput.Button.PrimaryHandTrigger, OVRInput.Controller.LTouch))
        {
            PlayPrevious();
        }

        // Both controllers primary index triggers for TogglePlayPause() in Oculus VR
        if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger, OVRInput.Controller.RTouch) ||
            OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger, OVRInput.Controller.LTouch))
        {
            TogglePlayPause();
        }
    }

    // Coroutine to play the video with a delay
    IEnumerator PlayVideoWithDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        // Play the current video
         videoplayer.Play();
    }

    // Method to play the next video clip
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

    // Method to play the previous video clip
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

    // Method to toggle play/pause
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
