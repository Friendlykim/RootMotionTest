using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SA
{
    public class Helpers : MonoBehaviour
    {
        [Range(-1, 1)]
        public float vertical;
        [Range(-1, 1)]
        public float horizontal;

        public GameObject Shield;

        public bool PlayAnim;
        public bool twohanded;
        public bool EnableRM;
        public bool Parry;
        public bool UseItem;
        public bool Interacting;
        public bool LockOn;

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

            Interacting = anim.GetBool("interacting");

            if (LockOn == false)
            {
                horizontal = 0;
                vertical = Mathf.Clamp01(vertical);
            }

            anim.SetBool("lockon", LockOn);

            if (EnableRM)
                return;

            if(UseItem)
            {
                anim.Play("Item_Soul");
                UseItem = false;
            }

            if(Interacting)
            {
                PlayAnim = false;
                vertical = Mathf.Clamp(vertical, 0, 0.5f);
            }

            if(Parry)
            {
                anim.Play("parry");
                Parry = false;
            }

            anim.SetBool("isTwoHand", twohanded);

            if(PlayAnim)
            {
                string targetAnim;

                if(twohanded)
                {
                    int r = Random.Range(0, th_attacks.Length);
                    targetAnim = th_attacks[r];
                    Shield.SetActive(false);
                }
                else
                {
                    int r = Random.Range(0, oh_attacks.Length);
                    targetAnim = oh_attacks[r];
                    Shield.SetActive(true);

                    if (vertical > 0.5f)
                        targetAnim = oh_attacks[1];
                }

                vertical = 0;
                //Debug.Log("애니메이션 실행");
                anim.CrossFade(targetAnim, 0.4f);
             //   anim.SetBool("canMove", false);
             //   EnableRM = true;
                PlayAnim = false;
            }
            anim.SetFloat("vertical", vertical);

            anim.SetFloat("horizontal", horizontal);
        }
    }
}

