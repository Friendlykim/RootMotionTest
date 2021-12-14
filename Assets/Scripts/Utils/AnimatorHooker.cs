using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SA
{
    public class AnimatorHooker : MonoBehaviour
    {
        Animator anim;
        StateManager states;

        public void Init(StateManager st)
        {
            states = st;
            anim = states.anim;
        }

        void OnAnimatorMove()
        {
            if (states.CanMove)
                return;
            //transform.position = anim.rootPosition;

            states.rigid.drag = 0;
            float multipler = 1;

            Vector3 delta = anim.deltaPosition;
            delta.y = 0;
            Vector3 v = (delta * multipler) / states.delta;
            states.rigid.velocity = v;
        }

        public void LateTick()
        {
           // if(states)
        }

    }
}