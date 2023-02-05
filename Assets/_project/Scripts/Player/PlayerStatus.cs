using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerStatus : MonoBehaviour
{
    private PlayerMovement _playerMovement;
    private PlayerCombat _playerCombat;
    private SpriteRenderer _sprite;

    public int Lives;
    public bool Dead = false;

    private void Awake()
    {
        SetUpComponents();
    }

    private void SetUpComponents()
    {
        _playerCombat = GetComponent<PlayerCombat>();
        _playerMovement = GetComponent<PlayerMovement>();
        _sprite = GetComponentInChildren<SpriteRenderer>();
    }

    public void ChangeLives(int change)
    {
        Lives -= change;
        if (Lives <= 0) Dead = true;
    }

    public void Death()
    {
        SoundManager.Instance.PlaySFX(SoundManager.AudioClipID.PLAYER_DEATH_1);
        SoundManager.Instance.PlaySFX(SoundManager.AudioClipID.PLAYER_DEATH_2);

        _sprite.enabled = false;
        FeelManager.Instance.HitVfxActive(transform, FeelManager.Instance.burstVfx);
        LevelManager.Instance.ActiveReloadLevel();
        this.gameObject.SetActive(false);
    }

    
}
