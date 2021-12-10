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
        // 패드 인풋
        public bool rt, rb, lt, lb, b, a, x, y; 

        [Header("Stats")]
        public float moveSpeed;
        public float runSpeed;
        public float rotateSpeed;
        public float toGround;
        public bool CanMove;
        [Header("States")]
        public bool onGround;
        public bool IsRun;
        public bool lockOn;
        public bool inAction;

        [HideInInspector]
        public Animator anim;
        [HideInInspector]
        public Rigidbody rigid;
        [HideInInspector]
        public float delta;
        [HideInInspector]
        public LayerMask ignoreLayers;

        float _actionDelay;
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

        void SetAnimator() // 애니메이터 받아오기
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

        public void FixedTick(float d) //매프레임마다 적용(물리)
        {
            delta = d;

 

            DetectAction();

            if (inAction)
            {
                _actionDelay += delta;
                if(_actionDelay >0.3f)
                {
                    inAction = false;
                    _actionDelay = 0;
                }
                else
                {
                    return;
                }

            }
  

            CanMove = anim.GetBool("canMove");

            if (!CanMove)
                return;

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

        public void DetectAction() //루트모션 애니메이션
        {
            if (CanMove == false)
                return;

            if (rb == false && rt == false && lb == false && lt == false)
                return;
            Debug.Log("DetectAction");
            string targetAnim = null;
            if (rb)
                targetAnim = "OH_Sword_Attack1";
            if (rt)
                targetAnim = "OH_Sword_Attack2";
            if (lb)
                targetAnim = "OH_Sword_Attack3";
            if (lt)
                targetAnim = "OH_Sword_HeavyAttack1";

            if (string.IsNullOrEmpty(targetAnim))
                return;

            CanMove = false;
            inAction = true;
            anim.CrossFade(targetAnim, 0.2f);
            rigid.velocity = Vector3.zero;

        }

        public void Tick(float d)
        {
            delta = d;
            onGround = OnGround();

            anim.SetBool("onGround", onGround);


        } //매프레임별

        void HandleMovementAnimation()
        {
            anim.SetBool("run", IsRun);
            anim.SetFloat("vertical", MoveAmount, 0.4f, delta);
            
        }//움직임 애니메이션

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
        }//중력,추락
    }
}

