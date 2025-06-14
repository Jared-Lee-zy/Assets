using UnityEngine;

// Controls the behaviour of matcha collectible in the game.
public class MatchaBehaviour : MonoBehaviour
{
    // Value added to the player's score when they collect this matcha.
    [SerializeField]
    int matchaValue = 1;

    // Reference to the MeshRenderer component for changing materials.
    MeshRenderer myMeshRenderer;

    // Sound played when the matcha is collected.
    [SerializeField]
    AudioClip collectSound;

    // Material used to highlight the matcha when hovered over.
    [SerializeField]
    Material highlightMat;

    // Original material of the matcha, used to revert back after highlighting.
    Material originalMat;

    // Controls the floating and rotating behaviour of the matcha collectible.
    [SerializeField] float floatAmplitude = 0.25f;  // How much it floats up/down

    [SerializeField] float floatFrequency = 1f;     // How fast it floats

    [SerializeField] float rotationSpeed = 50f;     // Degrees per second

    // Stores the initial position of the matcha collectible for floating effect.
    Vector3 startPos;

    // Called when the player collects the matcha collectible.
    public void Collect(PlayerBehaviour player)
    {
        AudioSource.PlayClipAtPoint(collectSound, transform.position);
        Debug.Log("Matcha Collected");
        // Modifies the player's score by the matcha value.
        player.ModifyScore(matchaValue);
        // Removes the matcha collectible from the scene.
        Destroy(gameObject);
    }



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Save the initial position for floating effect.
        startPos = transform.position;  // Save the initial position

        // Get the MeshRenderer component attached to the matcha collectible.
        myMeshRenderer = GetComponent<MeshRenderer>();

        // Store the ogirinal material of the matcha collectible.
        originalMat = myMeshRenderer.material;

    }

    // Highlights the matcha collectible by changing its material.
    public void Highlight()
    {
        myMeshRenderer.material = highlightMat;
    }

    // Reverts the highlight by changing the material back to the original.
    public void Unhighlight()
    {
        myMeshRenderer.material = originalMat;
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
