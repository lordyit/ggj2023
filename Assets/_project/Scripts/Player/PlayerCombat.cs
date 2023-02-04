using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    private PlayerInputManager _inputManager;
    private PlayerAnimations _playerAnimations;
    private PlayerMovement _playerMovement;
    private PlayerStatus _playerStatus;

    public bool CanAttack = true;

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
    }

    private void Attack()
    {
        if (!_inputManager._inputAttack.triggered) return;
        if (!CanAttack) return;

        _playerAnimations.AttackAnimation();
        CanAttack = false;
        _playerMovement.CanMove = false;
    }

    public void TakeDamage(int damage)
    {
        _playerStatus.ChangeLives(damage);
    }

    // Update is called once per frame
    void Update()
    {
        Attack();
    }
}
