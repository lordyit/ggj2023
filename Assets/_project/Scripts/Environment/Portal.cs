using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{
    public bool Open;
    public int NextSceneIndex;

    private BoxCollider _collider;
    private Animator _animator;

    private void Awake()
    {
        _collider = GetComponentInChildren<BoxCollider>();
        _animator = GetComponent<Animator>();
    }

    public void OpenGate()
    {
        Open = true;
        _collider.isTrigger = true;
        _animator.enabled = true;
    }

    public void EnterGate()
    {
        SceneManager.LoadScene(NextSceneIndex);
    }
}
