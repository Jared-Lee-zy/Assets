using UnityEngine;

/*
 * Author : Jared Lee
 * Date of Creation : 19th April 2025
 * Script Function : Controls the behaviour of the door in the game that can be opened and closed by the player.
 */
public class DoorBehaviour : MonoBehaviour
{
    // Tracks whether the door is currently opened or closed.
    bool Open = false;

    // Audio source that plays when the door is opened or closed.
    AudioSource doorAudioSource;

    // Called when the player interacts with the door.
    public void Interact()
    {
        if (!Open)
        {
            // Plays door sound
            doorAudioSource.Play();
            // Rotate the door 90 degrees around the Y-axis.
            transform.Rotate(0, 90f, 0);
            Open = true;
        }
        else
        {
            // Plays door sound
            doorAudioSource.Play();
            // Rotate the door back to its original position.
            transform.Rotate(0, -90f, 0);
            Open = false;
        }


    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        doorAudioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
