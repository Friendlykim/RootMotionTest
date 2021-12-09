using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SA
{
    public class InputHandler : MonoBehaviour
    {
        float vertical;
        float horizontal;

        StateManager states;
        CameraManager cam;

        float delta;
        // Start is called before the first frame update
        void Start()
        {
            states = GetComponent<StateManager>();
            states.Init();
            cam = CameraManager.Singleton;
            cam.Init(this.transform);
        }
        
        private void Update()
        {
            delta = Time.deltaTime;
            states.Tick(delta);

        }
        


        private void FixedUpdate()
        {
            delta = Time.fixedDeltaTime;
            //delta = Time.smoothDeltaTime;
            GetInput();
            UpdateState();
            cam.Tick(delta);
            states.FixedTick(delta);
        }

        void GetInput()
        {
            vertical = Input.GetAxis("Vertical");
            horizontal = Input.GetAxis("Horizontal");
        }

        void UpdateState()
        {
            states.vertical = vertical;
            states.horizontal = horizontal;

            Vector3 v = states.vertical * cam.transform.forward;
            Vector3 h = states.horizontal * cam.transform.right;
            states.MoveDir = (v + h).normalized;
            float m = Mathf.Abs(horizontal) + Mathf.Abs(vertical);

            states.MoveAmount = Mathf.Clamp01(m);

        }
    }
}

