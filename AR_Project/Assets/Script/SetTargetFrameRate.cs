using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetTargetFrameRate : MonoBehaviour
{
    [SerializeField]
    [Tooltip("Sets the application's target frame rate.")]
    private int m_TargetFrameRate = 60;

    /// <summary>
    /// Get or set the application's target frame rate.
    /// </summary>
    public int targetFrameRate
    {
        get { return this.m_TargetFrameRate; }
        set
        {
            this.m_TargetFrameRate = value;
            this.SetFrameRate();
        }
    }

    void SetFrameRate()
    {
        Application.targetFrameRate = this.targetFrameRate;
    }

    void Start()
    {
        this.SetFrameRate();
    }
}
