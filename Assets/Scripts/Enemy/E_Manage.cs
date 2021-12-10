using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E_Manage : MonoBehaviour
{
    E_Anim Eanim;
    E_Movement Emove;

    void Start()
    {
        Eanim = GetComponent<E_Anim>();
        Eanim.Init();
    }

    void Update()
    {
        
    }
}
