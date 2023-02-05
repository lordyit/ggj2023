using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEffects : MonoBehaviour
{
    public static PlayerEffects Instance;
    private bool _freezed = false;

    public float FreezeEffectTime;
    private void Awake()
    {
        Instance = this;
    }

    public void FreezeEffect()
    {
        if (_freezed) return;

        //Time.timeScale = 0f;
        //StartCoroutine(ApplyFreezeEffect(FreezeEffectTime));
    }

    private IEnumerator ApplyFreezeEffect(float time)
    {
        _freezed = true;
        yield return new WaitForSecondsRealtime(time);
        Time.timeScale = 1f;
        _freezed = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
