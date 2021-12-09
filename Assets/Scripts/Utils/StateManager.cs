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

        [Header("Stats")]
        public float moveSpeed;
        public float runSpeed;
        public float rotateSpeed;
        public float toGround;

        [Header("States")]
        public bool onGround;
        public bool IsRun;
        public bool lockOn;

        [HideInInspector]
        public Animator anim;
        [HideInInspector]
        public Rigidbody rigid;
        [HideInInspector]
        public float delta;
        [HideInInspector]
        public LayerMask ignoreLayers;
        public void Init()
        {
            SetAnimator();
            rigid = GetComponent<Rigidbody>();
            rigid.angularDrag = 999;
            rigid.drag = 4;
            rigid.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;

            gameObject.layer = 8;
            ignoreLayers = ~(1 << 9);

            anim.SetBool("onGround", true);
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

            rigid.drag = (MoveAmount > 0 || onGround == false) ? 0 : 4;

            if (IsRun)
                lockOn = false;

            if(!lockOn)
            {
                Vector3 targetDir = MoveDir;
                targetDir.y = 0;
                if (targetDir == Vector3.zero)
                    targetDir = transform.forward;
                Quaternion tr = Quaternion.LookRotation(targetDir);
                Quaternion targetRotation = Quaternion.Slerp(transform.rotation, tr, delta * MoveAmount * rotateSpeed);
                transform.rotation = targetRotation;
            }


            float targetspeed = moveSpeed;
            if (IsRun)
                targetspeed = runSpeed;

            if (onGround)
                rigid.velocity = MoveDir * (targetspeed * MoveAmount);

            //rigid.velocity = MoveDir * (targetspeed * MoveAmount);
            HandleMovementAnimation();
        }

        public void Tick(float d)
        {
            delta = d;
            onGround = OnGround();

            anim.SetBool("onGround", onGround);
        }

        void HandleMovementAnimation()
        {
            anim.SetBool("run", IsRun);
            anim.SetFloat("vertical", MoveAmount, 0.4f, delta);
            
        }

        public bool OnGround()
        {
            bool r = false;

            Vector3 origin = transform.position + (Vector3.up * toGround);
            Vector3 dir = -Vector3.up;
            float dis = toGround + 0.3f;
            RaycastHit hit;
            Debug.DrawRay(origin, dir * dis);
            if (Physics.Raycast(origin, dir, out hit, dis,ignoreLayers))
            {
                r = true;
                Vector3 targetPosition = hit.point;
                transform.position = targetPosition;
            }

            return r;
        }
    }
}

