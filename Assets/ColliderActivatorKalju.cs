using UnityEngine;

public class ColliderActivatorKalju : MonoBehaviour
{
    public GameObject objectToActivate;
    public GameObject objectToDeactivate;
    public LayerMask playerLayer;

    private void OnTriggerEnter(Collider other)
    {
        if (IsPlayer(other))
        {
            objectToActivate.SetActive(true);
            objectToDeactivate.SetActive(false);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (IsPlayer(other))
        {
            objectToActivate.SetActive(false);
            objectToDeactivate.SetActive(false);
        }
    }

    private bool IsPlayer(Collider other)
    {
        return playerLayer == (playerLayer | (1 << other.gameObject.layer));
    }
}



