using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class FeelManager : MonoBehaviour
{
    public static FeelManager Instance;

    private CinemachineFreeLook _cinemachineVirtual;
    private float _shakeTimer;
    private void Awake()
    {
        Instance = this;
        _cinemachineVirtual = GetComponent<CinemachineFreeLook>();
    }
    
    public void ShakeCamera(float intensity, float timer)
    {
        CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlin =
            _cinemachineVirtual.GetComponentInChildren<CinemachineBasicMultiChannelPerlin>();

        cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = intensity;
        _shakeTimer = timer;
    }

    void ShakeTimer()
    {
        if (_shakeTimer > 0)
        {
            _shakeTimer -= Time.deltaTime;
        }
        if (_shakeTimer <= 0)
        {
            CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlin =
            _cinemachineVirtual.GetComponentInChildren<CinemachineBasicMultiChannelPerlin>();

            cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = 0;
        }
    }

    // Update is called once per frame
    void Update()
    {
        ShakeTimer();
    }
}
