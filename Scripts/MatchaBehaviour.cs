using UnityEngine;

public class MatchaBehaviour : MonoBehaviour
{
    [SerializeField]
    int matchaValue = 1;

    MeshRenderer myMeshRenderer;

    [SerializeField]
    AudioClip collectSound;

    [SerializeField]
    Material highlightMat;

    Material originalMat;

    [SerializeField] float floatAmplitude = 0.25f;  // How much it floats up/down

    [SerializeField] float floatFrequency = 1f;     // How fast it floats
    
    [SerializeField] float rotationSpeed = 50f;     // Degrees per second

    Vector3 startPos;


    public void Collect(PlayerBehaviour player)
    {
        AudioSource.PlayClipAtPoint(collectSound, transform.position);
        Debug.Log("Matcha Collected");
        player.ModifyScore(matchaValue);
        Destroy(gameObject);
    }



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        startPos = transform.position;  // Save the initial position

        myMeshRenderer = GetComponent<MeshRenderer>();

        originalMat = myMeshRenderer.material;

    }

        public void Highlight()
        {
            myMeshRenderer.material = highlightMat;
        }

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
