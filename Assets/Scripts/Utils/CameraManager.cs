using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SA
{
    public class CameraManager : MonoBehaviour
    {
        public bool LockOn;

        public float followSpeed = 9;
        public float mouseSpeed = 2;
        public float controllerSpeed = 7;

        public Transform Target;
        [HideInInspector]
        public Transform pivot;
        [HideInInspector]
        public Transform camTrans;

        float TurnSmooth = .1f;
        public float minAngle = -35;
        public float maxAngle = 35;

        float SmoothX;
        float SmoothY;
        float SmoothXVelocity;
        float SmoothYVelocity;
        public float Lookangle;
        public float TiltAngle;
        public void Init(Transform t)
        {
            Target = t;

            camTrans = Camera.main.transform;

            pivot = camTrans.parent;
        }
        public void Tick(float d)
        {
            float v = Input.GetAxis("Mouse Y");
            float h = Input.GetAxis("Mouse X");

            float c_v = Input.GetAxis("RightAxis Y");
            float c_h = Input.GetAxis("RightAxis X");

            float targetSpeed = mouseSpeed;

            if(c_h != 0||c_v != 0)
            {
                v = c_v;
                h = c_h;
                targetSpeed = controllerSpeed;
            }
            FollowTarget(d);
            HandleRotation(d, v, h, targetSpeed);
        }

        void FollowTarget(float d)
        {
            float speed = d * followSpeed;
            Vector3 targetPos = Vector3.Lerp(transform.position, Target.position, speed);
            transform.position = targetPos;
        }

        void HandleRotation(float d,float v,float h,float targetSpeed)
        {
            if(TurnSmooth > 0)
            {
                SmoothX = Mathf.SmoothDamp(SmoothX, h,ref SmoothXVelocity, TurnSmooth);
                SmoothY = Mathf.SmoothDamp(SmoothY, v, ref SmoothYVelocity, TurnSmooth);
            }
            else
            {
                SmoothX = h;
                SmoothY = v;
            }

            if(LockOn)
            {

            }
            Lookangle += SmoothX * targetSpeed;
            transform.rotation = Quaternion.Euler(0, Lookangle, 0);
            TiltAngle -= SmoothY * targetSpeed;
            TiltAngle = Mathf.Clamp(TiltAngle, minAngle, maxAngle);
            pivot.localRotation = Quaternion.Euler(TiltAngle, 0, 0);
        }

        public static CameraManager Singleton;

        private void Awake()
        {
            Singleton = this;
                
        }

        private void FixedUpdate()
        {
            
        }
    }
}
