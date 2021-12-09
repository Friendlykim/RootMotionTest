using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SA
{
    public class StateManager : MonoBehaviour
    {
        [Header("Init")]
        public GameObject activeModel;

        [Header("Inputs")]
        public float vertical;
        public float horizontal;
        public float MoveAmount;
        public Vector3 MoveDir;

        [Header("States")]
        public float moveSpeed;
        public float runSpeed;
        public bool IsRun;

        [HideInInspector]
        public Animator anim;
        [HideInInspector]
        public Rigidbody rigid;
        [HideInInspector]
        public float delta;
        public void Init()
        {
            SetAnimator();
            rigid = GetComponent<Rigidbody>();


        }

        void SetAnimator()
        {
            if (activeModel == null)
            {
                anim = GetComponentInChildren<Animator>();
                if (anim == null)
                    Debug.Log("No model found");
                else
                    activeModel = anim.gameObject;
            }
            if (anim == null)
                anim = activeModel.GetComponent<Animator>();
        }

        public void FixedTick(float d)
        {
            delta = d;

            float targetspeed = moveSpeed;

            rigid.velocity = MoveDir * moveSpeed;
        }
    }
}

