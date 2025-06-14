using UnityEngine;

// Controls the behaviour of the keycard collectible in the game.
public class keycardBehaviour : MonoBehaviour
{
    // Audio source that plays when the keycard is collected.
    [SerializeField]
    AudioSource keycardAudioSource;

    // Controls the floating and rotating behaviour of the keycard game object.
    [SerializeField] float floatAmplitude = 0.25f;  // How much it floats up/down

    [SerializeField] float floatFrequency = 1f;     // How fast it floats

    [SerializeField] float rotationSpeed = 50f;     // Degrees per second

    // Stores the initial position of the keycard for floating effect.
    Vector3 startPos;
    // Called when the player collects the keycard.
    public void Collect(PlayerBehaviour player)
    {

        Debug.Log("Collect() called on keycard");

        // Gives the player the keycard.
        player.PickupKeycard();

        // Plays the keycard collection sound and destroys the keycard object after the sound finishes.
        if (keycardAudioSource != null)
        {
            keycardAudioSource.Play();
            Destroy(gameObject, keycardAudioSource.clip.length);
        }

    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    // Called once the keycard is instantiated in the scene.
    void Start()
    {
        startPos = transform.position;  // Save the initial position
    }

    // Update is called once per frame
    void Update()
    {
        // Float up and down
        Vector3 tempPos = startPos;
        tempPos.y += Mathf.Sin(Time.time * floatFrequency * Mathf.PI * 2) * floatAmplitude;
        transform.position = tempPos;

        // Rotate around Y axis
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
    }
}
