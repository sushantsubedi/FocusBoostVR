using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MusicManager : MonoBehaviour
{
    private AudioSource _audioSource;
    public AudioClip[] songs; // array to hold the sound tracks
    public float volume;
    private bool isPlaying = true; // Track the music play state

    // Keep track of the current song index
    private int currentSongIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        _audioSource = GetComponent<AudioSource>();

        // Start playing the first song when the game starts
        ChangeSong(currentSongIndex);
        isPlaying = true; // assume the music starts playing
    }

    // Update is called once per frame
    void Update()
    {
        _audioSource.volume = volume;
        if (Input.GetKeyDown(KeyCode.Space))
            PlayPauseSong();



        // Check if the song has ended and loop it
        if (!_audioSource.isPlaying && isPlaying)
        PlayCurrentSong();

        // Check for the right arrow key to skip to the next song
        if (Input.GetKeyDown(KeyCode.RightArrow))
            NextSong();

        if (Input.GetKeyDown(KeyCode.LeftArrow))
            PreviousSong();
    }

    public void PlayPauseSong()
    {
        // Toggle play/pause when Space key is pressed
        if (isPlaying)
            PauseSong();
        else
            ResumeSong();
    }

    // Play the current song (used for initial play and looping)
    public void PlayCurrentSong()
    {
        _audioSource.clip = songs[currentSongIndex];
        _audioSource.Play();
        isPlaying = true;
    }

    public void PauseSong()
    {
        _audioSource.Pause();
        isPlaying = false;
    }

    public void ResumeSong()
    {
        _audioSource.UnPause();
        isPlaying = true;
    }

    public void NextSong()
    {
        // Move to the next song index
        currentSongIndex++;

        // If it reaches  the end of the array, loop back to the first song
        if (currentSongIndex >= songs.Length)
        {
            currentSongIndex = 0;
        }

        // Play the new song
        PlayCurrentSong();
    }
    
    public void PreviousSong()
    {
        // Move to the previos song index
        currentSongIndex--;

        // If it reaches the beginning of the array, loop back to the last song
        if (currentSongIndex < 0)
        {
            currentSongIndex = songs.Length - 1;
        }

        PlayCurrentSong();
    }

    public void ChangeSong(int songPicked)
    {
        _audioSource.clip = songs[songPicked];
        _audioSource.Play();
    }
}
