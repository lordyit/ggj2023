using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private CharacterController _characterController;

    private PlayerAnimations _playerAnimations;
    private PlayerInputManager _inputManager;

    private Vector2 _movementValue;
    private Vector3 _finalMove;

    [SerializeField] private float _moveSpeed;

    public bool CanMove = true;

    private void Awake()
    {
        SetUpComponents();
    }

    private void SetUpComponents()
    {
        _characterController = GetComponent<CharacterController>();
        _playerAnimations = GetComponent<PlayerAnimations>();
        _inputManager = GetComponent<PlayerInputManager>();
    }

    private void Movement()
    {
        if (!CanMove) return;

        _movementValue = _inputManager._inputMovement.ReadValue<Vector2>();
        _finalMove = new Vector3(_movementValue.x, 0, _movementValue.y);
        _characterController.Move(_finalMove * _moveSpeed * Time.deltaTime);

        ApplyWalkAnimation();
    }

    private void ApplyWalkAnimation()
    {
        bool turn = false;
        if (_finalMove.x < 0) turn = true;
        if (_finalMove.x == 0) turn = _playerAnimations.GetSprite().flipX;

        if (_finalMove.x != 0 || _finalMove.z != 0) _playerAnimations.WalkAnimation(turn);
        else _playerAnimations.IdleAnimation();
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
    }
}
