using UnityEngine;

/*
 * Author : Jared Lee
 * Date of Creation : 30th April 2025
 * Script Function : Controls the behaviour of the gift box in the game that spawns a matcha collectible when broken by a projectile.
 */
public class giftBox : MonoBehaviour
{
    // Prefab of the matcha collectible that spawns when the gift box is broken by the projectile.
    [SerializeField]
    GameObject matchaPrefab;

    // Location where the matcha collectible will spawn when the gift box is broken.
    [SerializeField]
    Transform spawnPoint;

    //Flag to prevent multiple matcha collectibles from spawning when the gift box is broken.
    private bool isBroken = false;
    // Called when the gift box collides with a projectile.
    void OnCollisionEnter(Collision collision)
    {
        // If the gift box is already broken, do nothing.
        if (isBroken) return;

        // Check if the colliding object is a projectile.
        if (collision.gameObject.CompareTag("Projectile"))
        {
            isBroken = true;
            // Spawns a matcha collectible at the specified spawn point and rotation, then destroys the projectile and the gift box itself.
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
