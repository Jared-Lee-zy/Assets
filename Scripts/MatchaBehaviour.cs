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

    }
}
