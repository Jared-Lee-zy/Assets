using UnityEngine;

public class giftBox : MonoBehaviour
{
    [SerializeField]
    GameObject matchaPrefab;

    [SerializeField]
    Transform spawnPoint;

    private bool isBroken = false;
    void OnCollisionEnter(Collision collision)
    {
        if (isBroken) return;

        if (collision.gameObject.CompareTag("Projectile"))
        {
            isBroken = true;
            Instantiate(matchaPrefab, spawnPoint.position, spawnPoint.rotation);
            Destroy(collision.gameObject);
            Destroy(gameObject);

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
