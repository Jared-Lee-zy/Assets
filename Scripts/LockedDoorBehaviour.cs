using UnityEngine;

/*
 * Author : Jared Lee
 * Date of Creation : 10th June 2025
 * Script Function : Controls the behaviour of the locked door in the game that can be unlocked by the player using a keycard.
 */
public class LockedDoorBehaviour : MonoBehaviour
{
    // Audio source that plays when the door is unlocked.
    [SerializeField]
    AudioSource lockedDoorAudioSource;

    // Attempts to unlock the door when the player interacts with it.
    public void Unlock(PlayerBehaviour player)
    {   
        // Check if the player has a keycard.
        if (player.Haskeycard())
        {
            Debug.Log("Door unlcked!");
            // Plays unlock sound and destroys the door after the sound finishes.
            lockedDoorAudioSource.Play();
            Destroy(gameObject, lockedDoorAudioSource.clip.length);
        }
        else
        {
            Debug.Log("Door is locked. You need a keycard.");
        }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
