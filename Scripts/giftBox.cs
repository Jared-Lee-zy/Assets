using UnityEngine;

public class giftBox : MonoBehaviour
{
    [SerializeField]
    GameObject matchaPrefab;

    [SerializeField]
    Transform spawnPoint;
    void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.CompareTag("Projectile"))
        {
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
