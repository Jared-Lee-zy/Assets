using UnityEngine;

public class LockedDoorBehaviour : MonoBehaviour
{
    void OpenDoor()
    {
        Destroy(gameObject);
    }
    
    public void Unlock(PlayerBehaviour player)
    {
        if (player.Haskeycard())
        {
            Debug.Log("Door unlcked!");
            OpenDoor();
        }
        else
        {
            Debug.Log("Door is locked. You need a keycard.");
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
