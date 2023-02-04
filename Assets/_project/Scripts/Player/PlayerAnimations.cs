using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
    private Animator _animator;
    private SpriteRenderer _sprite;

    private string _movementKey = "move";
    private string _attackKey = "attack";

    private void Awake()
    {
        SetUpComponents();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public SpriteRenderer GetSprite()
    {
        return _sprite;
    }

    private void SetUpComponents()
    {
        _animator = GetComponentInChildren<Animator>();
        _sprite = GetComponentInChildren<SpriteRenderer>();
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

    // Update is called once per frame
    void Update()
    {
        
    }
}
