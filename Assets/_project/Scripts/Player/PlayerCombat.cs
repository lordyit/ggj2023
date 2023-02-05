using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    private PlayerInputManager _inputManager;
    private PlayerAnimations _playerAnimations;
    private PlayerMovement _playerMovement;
    private PlayerStatus _playerStatus;
    private SpriteRenderer _sprite;

    [SerializeField] Transform[] _swordPositions;
    public GameObject _sword;

    public bool CanAttack = true;
    public bool CanTakeDamage = true;

    private void Awake()
    {
        SetUpComponents();
    }

    private void SetUpComponents()
    {
        _inputManager = GetComponent<PlayerInputManager>();
        _playerAnimations = GetComponent<PlayerAnimations>();
        _playerMovement = GetComponent<PlayerMovement>();
        _playerStatus = GetComponent<PlayerStatus>();
        _sprite = GetComponentInChildren<SpriteRenderer>();
    }

    private void Attack()
    {
        if (!_inputManager._inputAttack.triggered) return;
        if (!CanAttack) return;
        if (_playerStatus.Dead) return;

        _playerAnimations.AttackAnimation();
        CanAttack = false;
        _playerMovement.CanMove = false;
    }

    public void TakeDamage(int damage)
    {
        if (!CanTakeDamage) return;
        
        FeelManager.Instance.ShakeCamera(5, 0.1f);
        SoundManager.Instance.PlaySFX(SoundManager.AudioClipID.PLAYER_DAMAGED_3);

        CanTakeDamage = false;
        _playerStatus.ChangeLives(damage);
        _playerAnimations.TakeDamageAnimation();
    }

    private void PositionSword()
    {
        if (!_sprite.flipX) _sword.transform.position = _swordPositions[0].position;
        else _sword.transform.position = _swordPositions[1].position;
    }

    void Update()
    {
        Attack();
        PositionSword();
    }
}
