using UnityEngine;

public class DialogSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject dialog = null;

    private void OnTriggerEnter(Collider other)
    {
        bool isPlayer = other.gameObject.name == "Player";
        if (isPlayer)
        {
            gameObject.SetActive(false);
            dialog.SetActive(true);
        }
    }
}
