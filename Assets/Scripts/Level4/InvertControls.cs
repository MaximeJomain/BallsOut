using UnityEngine;

public class InvertControls : MonoBehaviour
{
    public PlayerController player;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Inverted Controls");
            player.invertControls = !player.invertControls;
            Destroy(gameObject);
        }
    }
}
