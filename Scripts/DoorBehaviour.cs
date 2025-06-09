using UnityEngine;

public class DoorBehaviour : MonoBehaviour
{

    bool Open = false;

    AudioSource doorAudioSource;

    public void Interact()
    {
        if (!Open)
        {
            doorAudioSource.Play();
            // Rotate the door 90 degrees around the Y-axis
            transform.eulerAngles += new Vector3(0, 90f, 0);
            Open = true;
        }
        else
        {
            transform.eulerAngles -= new Vector3(0, 90f, 0);
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
