using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SA
{
    public class InputHandler : MonoBehaviour
    {
        float vertical;
        float horizontal;
        bool b_input; //´Þ¸®±â
        bool a_input;
        bool x_input;
        bool y_input;

        bool rb_input;
        bool lb_input;
        float rt_axis;
        bool rt_input;
        float lt_axis;
        bool lt_input;

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
            b_input = Input.GetButton("b_input");

            rt_input = Input.GetButton("RT");
            rt_axis = Input.GetAxis("RT");
            if (rt_axis != 0)
                rt_input = true;

            lt_input = Input.GetButton("LT");
            lt_axis = Input.GetAxis("LT");
            if (lt_axis != 0)
                lt_input = true;

            //Debug.Log(rt_input);
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

            if(b_input)
            {
                states.IsRun = (states.MoveAmount > 0);
              //  if (states.MoveAmount > 0)
              //      states.IsRun = true;
            }
            else
            {
                states.IsRun = false;
            }

        }
    }
}

