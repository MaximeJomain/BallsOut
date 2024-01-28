using UnityEngine;

public class InvertControls : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerController player = other.GetComponent<PlayerController>();
            if (player)
            {
                Debug.Log("Inverted Controls");
                player.invertControls = !player.invertControls;
                Destroy(gameObject);
            }
        }
    }
}
