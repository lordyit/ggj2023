using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputManager : MonoBehaviour
{
    public Controller _controller;
    [HideInInspector] public InputAction _inputMovement;
    [HideInInspector] public InputAction _inputAttack;
    // Start is called before the first frame update
    void Start()
    {
        StartInputs();
    }

    private void StartInputs()
    {
        _controller = new Controller();
        _controller.Enable();
        _inputMovement = _controller.Player.Move;
        _inputAttack = _controller.Player.Fire;
    }

}
