using System;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField]
    private float time;

    private float m_timer;

    public event Action OnTimerEnd; 

    private void OnEnable() 
    {
        m_timer = time;
    }

    private void Update() 
    {
        m_timer -= Time.deltaTime;

        if (m_timer < 0f)
        {
            OnTimerEnd?.Invoke();
        }
    }
}
