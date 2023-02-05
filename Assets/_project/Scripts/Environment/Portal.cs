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
        if (_collider != null)
        {
            _collider.isTrigger = true;
        }
        else
        {
            Debug.LogError("[Portal] Collider is null (already destroyed)", this);
        }
        if (_animator != null)
        {
            _animator.enabled = true;
        }
        else
        {
            Debug.LogError("[Portal] _animator is null (already destroyed)", this);
        }
    }

    public void EnterGate()
    {
        SceneManager.LoadScene(NextSceneIndex);
    }
}
