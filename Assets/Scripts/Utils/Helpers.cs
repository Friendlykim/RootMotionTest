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

        //public GameObject Shield;
        P_Upgrade shield;

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
            if (Input.GetButtonDown("RB"))
                PlayAnim = true;

            if (Input.GetKeyDown(KeyCode.G))
                UseItem = true;

            if (Input.GetKeyDown(KeyCode.Tab))
                Parry = true;

            if (Input.GetKeyDown(KeyCode.F))
                twohanded = (twohanded==true) ? false : true;

            EnableRM = !anim.GetBool("canMove");
            //anim.applyRootMotion = EnableRM;

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

            if(Interacting==true)
            {
                PlayAnim = false;
                vertical = Mathf.Clamp(vertical, 0, 0.5f);
                shield.Rhand[shield.curShield].SetActive(false);
            }
            else
            {
                shield.Rhand[shield.curShield].SetActive(true);
            }

            if (twohanded)
                shield.Rhand[shield.curShield].SetActive(false);
            else
                shield.Rhand[shield.curShield].SetActive(true);

            if (Parry)
            {
                //anim.Play("parry");
                anim.CrossFade("parry", 0.25f);
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
                }
                else
                {
                    int r = Random.Range(0, oh_attacks.Length);
                    targetAnim = oh_attacks[r];

                    if (vertical >= 1f)
                        targetAnim = oh_attacks[0];
                }

                vertical = 0;
                //Debug.Log("?????????? ????");
                anim.CrossFade(targetAnim, 0.4f);
             //   anim.SetBool("canMove", false);
             //   EnableRM = true;
                PlayAnim = false;
            }
           // anim.SetFloat("vertical", vertical);

          //  anim.SetFloat("horizontal", horizontal);

            
        }
    }
}

