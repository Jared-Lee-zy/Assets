using UnityEngine;

public class MatchaBehaviour : MonoBehaviour
{
    [SerializeField]
    int matchaValue = 1;


    public void Collect(PlayerBehaviour player)
    {
        Debug.Log("Matcha Collected");
        player.ModifyScore(matchaValue);
        Destroy(gameObject);
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
