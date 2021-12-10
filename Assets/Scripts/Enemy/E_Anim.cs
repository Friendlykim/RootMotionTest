using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E_Anim : MonoBehaviour
{
    [Header("Init")]
    public GameObject ActiveModel;

    Animator anim;

    public void Init()
    {
        SetAnimator();
    }

    void SetAnimator()
    {
        if (ActiveModel == null)
        {
            anim.GetComponentInChildren<Animator>();
            if (anim == null)
                Debug.Log("ActiveModel Null");
            else
                ActiveModel = anim.gameObject;
        }
        if (anim == null)
            anim = ActiveModel.GetComponent<Animator>();
    }


}
