using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
    private Animator _animator;
    private SpriteRenderer _sprite;
    private PlayerCombat _playerCombat;
    private PlayerStatus _playerStatus;

    WaitForSeconds _waitTakeDamage;

    private string _movementKey = "move";
    private string _attackKey = "attack";

    private void Awake()
    {
        SetUpComponents();
    }
    // Start is called before the first frame update
    void Start()
    {
        _waitTakeDamage = new WaitForSeconds(0.1f);
    }

    public SpriteRenderer GetSprite()
    {
        return _sprite;
    }

    private void SetUpComponents()
    {
        _animator = GetComponentInChildren<Animator>();
        _sprite = GetComponentInChildren<SpriteRenderer>();
        _playerCombat = GetComponent<PlayerCombat>();
        _playerStatus = GetComponent<PlayerStatus>();
    }

    public void WalkAnimation(bool invert)
    {
        _animator.SetInteger(_movementKey, 1);

        if (invert) _sprite.flipX = true;
        else _sprite.flipX = false;
    }

    public void IdleAnimation()
    {
        _animator.SetInteger(_movementKey, 0);
    }

    public void AttackAnimation()
    {
        _animator.SetTrigger(_attackKey);
    }

    public void TakeDamageAnimation()
    {
        StartCoroutine(TakeDamageCr());
    }
    IEnumerator TakeDamageCr()
    {
        if (_playerStatus.Lives <= 0) _playerStatus.Death();
        for (int i = 0; i < 15; i++)
        {
            _sprite.enabled = !_sprite.enabled;
            yield return _waitTakeDamage;
        }
        _sprite.enabled = true;
        _playerCombat.CanTakeDamage = true;
        
    }
}
