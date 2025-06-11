using UnityEngine;

public class FridgeBehaviour : MonoBehaviour
{
    [SerializeField]
    AudioSource fridgeSound;

    public void Interact()
    {
        fridgeSound.Play();
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
