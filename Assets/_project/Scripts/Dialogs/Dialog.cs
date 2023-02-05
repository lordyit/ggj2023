using UnityEngine;

public class Dialog : MonoBehaviour
{
    [SerializeField]
    private Dialog nextDialog = null;

    private void OnEnable()
    {
        SoundManager.Instance.PlaySFX(SoundManager.AudioClipID.MYSTERIOUS_VOICES);
    }

    public void GoToNextDialog()
    {
        gameObject.SetActive(false);
        
        if (nextDialog != null)
        {
            nextDialog.gameObject.SetActive(true);   
        }
    }
}
