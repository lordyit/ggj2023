using UnityEngine;

public class Dialog : MonoBehaviour
{
    [SerializeField]
    private Dialog nextDialog = null;

    public void GoToNextDialog()
    {
        gameObject.SetActive(false);
        
        if (nextDialog != null)
        {
            nextDialog.gameObject.SetActive(true);
        }
    }
}
