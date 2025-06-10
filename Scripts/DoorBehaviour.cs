using UnityEngine;

public class DoorBehaviour : MonoBehaviour
{
    bool Open = false;

    AudioSource doorAudioSource;

    public void Interact()
    {
        Debug.Log("Door Interact() called at time: " + Time.time + " on " + gameObject.name);
        
        if (!Open)
        {
            doorAudioSource.Play();
            // Rotate the door 90 degrees around the Y-axis
            transform.Rotate(0, 90f, 0);
            Open = true;
        }
        else
        {
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
