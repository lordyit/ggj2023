using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerStatus : MonoBehaviour
{
    [SerializeField]
    private Transform deadPlayer = null;

    private PlayerMovement _playerMovement;
    private PlayerCombat _playerCombat;
    private SpriteRenderer _sprite;
    private Image _hp;

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
        _hp = GameObject.Find("HP").GetComponent<Image>();
    }

    public void ChangeLives(int change)
    {
        Lives -= change;
        UpdateHPBar(Lives);
        if (Lives <= 0) Dead = true;
    }

    private void UpdateHPBar(int life)
    {
        switch (life)
        {
            case 0:
                _hp.fillAmount = 0;
                break;
            case 1:
                _hp.fillAmount = 0.33f;
                break;
            case 2:
                _hp.fillAmount = 0.66f;
                break;
            case 3:
                _hp.fillAmount = 1;
                break;
        }
    }

    public void Death()
    {
        SoundManager.Instance.PlaySFX(SoundManager.AudioClipID.PLAYER_DEATH_1);
        SoundManager.Instance.PlaySFX(SoundManager.AudioClipID.PLAYER_DEATH_2);

        Instantiate(deadPlayer, transform.position, Quaternion.LookRotation(forward: -Vector3.up));

        _sprite.enabled = false;
        FeelManager.Instance.HitVfxActive(transform, FeelManager.Instance.burstVfx);
        LevelManager.Instance.ActiveReloadLevel();
        this.gameObject.SetActive(false);
    }

    
}
