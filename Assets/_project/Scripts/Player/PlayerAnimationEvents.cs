using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationEvents : MonoBehaviour
{
    private PlayerCombat _playerCombat;
    private PlayerMovement _playerMovement;

    private void Awake()
    {
        SetUpComponents();
    }

    private void SetUpComponents()
    {
        _playerCombat = GetComponentInParent<PlayerCombat>();
        _playerMovement = GetComponentInParent<PlayerMovement>();
    }

    private void CanAttackAgain()
    {
        _playerCombat.CanAttack = true;
    }

    private void CanMoveAgain()
    {
        _playerMovement.CanMove = true;
    }
}
