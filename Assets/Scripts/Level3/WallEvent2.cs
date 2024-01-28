using UnityEngine;

public class WallEvent2 : MonoBehaviour
{
    public Transform wall;
    public Level3 level3;
    
    private void OnTriggerEnter(Collider other)
    {
        wall.Translate(0f, 5f, 0f);
        GetComponent<Collider>().enabled = false;
        level3.PlayAudioCoroutine(3);
    }
}
