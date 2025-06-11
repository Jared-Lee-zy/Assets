using UnityEngine;

public class keycardBehaviour : MonoBehaviour
{
    [SerializeField]
    AudioSource keycardAudioSource;

    [SerializeField] float floatAmplitude = 0.25f;  // How much it floats up/down

    [SerializeField] float floatFrequency = 1f;     // How fast it floats
    
    [SerializeField] float rotationSpeed = 50f;     // Degrees per second

    Vector3 startPos;
    public void Collect(PlayerBehaviour player)
    {

        Debug.Log("Collect() called on keycard");

        player.PickupKeycard();

        if (keycardAudioSource != null)
        {
            keycardAudioSource.Play();
            Destroy(gameObject, keycardAudioSource.clip.length);
        }

    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
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
