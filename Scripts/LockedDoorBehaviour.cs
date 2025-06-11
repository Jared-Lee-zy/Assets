using UnityEngine;

public class LockedDoorBehaviour : MonoBehaviour
{
    [SerializeField]
    AudioSource lockedDoorAudioSource;
    
    public void Unlock(PlayerBehaviour player)
    {
        if (player.Haskeycard())
        {
            Debug.Log("Door unlcked!");
            lockedDoorAudioSource.Play();
            Destroy(gameObject, lockedDoorAudioSource.clip.length);
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
