using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E_Movement : MonoBehaviour
{
    public Transform Target;

    [Header("Inputs")]
    public float Vertical;
    public float Horizontal;
    public Vector3 MoveDir;

    [Header("Stats")]
    public float MoveSpeed;
    public float RotationSpeed;

    [Header("States")]
    public bool isWalk;
    public bool isAttack;

    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
