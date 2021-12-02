using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SA
{
    public class Helpers : MonoBehaviour
    {
        [Range(0, 1)]
        public float vertical;



        public bool PlayAnim;
        public bool twohanded;
        public bool EnableRM;

        public string[] oh_attacks;
        public string[] th_attacks;
        Animator anim;
        // Start is called before the first frame update
        void Start()
        {
            anim = GetComponent<Animator>();
        }

        // Update is called once per frame
        void Update()
        {
            EnableRM = !anim.GetBool("canMove");
            anim.applyRootMotion = EnableRM;

            if (EnableRM)
                return;


            anim.SetBool("isTwoHand", twohanded);

            if(PlayAnim)
            {
                string targetAnim;

                if(twohanded)
                {
                    int r = Random.Range(0, th_attacks.Length);
                    targetAnim = th_attacks[r];
                }
                else
                {
                    int r = Random.Range(0, oh_attacks.Length);
                    targetAnim = oh_attacks[r];
                }

                vertical = 0;
                Debug.Log("애니메이션 실행");
                anim.CrossFade(targetAnim, 0.4f);
                anim.SetBool("canMove", false);
                PlayAnim = false;
            }
            anim.SetFloat("vertical", vertical);


        }
    }
}

