using UnityEngine;

// Controls the behaviour of the fridge in the game that plays a sound when interacted with.
public class FridgeBehaviour : MonoBehaviour
{
    // Audio source that plays when the fridge is interacted with.
    [SerializeField]
    AudioSource fridgeSound;

    // Called when the player innteracts with the fridge.
    public void Interact()
    {
        // Plays the fridge sound.
        fridgeSound.Play();
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
