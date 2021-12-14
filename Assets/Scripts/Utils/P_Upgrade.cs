using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class P_Upgrade : MonoBehaviour
{
    public bool GoTier2;
    public bool GoTier3;

    public int curShield = 0;

    public Text GearText,GearText2;
    public GameObject[] gearbut;

    public Transform EquipGroup;

    List<GameObject> GearSetList = new List<GameObject>();

    public GameObject[] Rhand;
    public GameObject[] Lhand;

    // Start is called before the first frame update
    void Start()
    {
        EquipSet(EquipGroup, GearSetList);

        GearText.text = "Level 1";
        gearbut[1].SetActive(false);
    }

    public void EquipSet(Transform group,List<GameObject> list)
    {
        foreach(Transform GearSet in group)
        {
            GearSet.gameObject.SetActive(false);
            list.Add(GearSet.gameObject);
        }
        for (int i = 0; i < list.Count; i++)
        {
            Rhand[i].SetActive(false);
            Lhand[i].SetActive(false);
        }

        list[0].SetActive(true);
        Rhand[curShield].SetActive(true);
        Lhand[0].SetActive(true);
    }

    public void P_TierUp(List<GameObject> list,int n,int s)
    {
        for (int i = 0; i < list.Count; i++)
        {
            list[i].SetActive(false);
            Rhand[i].SetActive(false);
            Lhand[i].SetActive(false);
        }
        curShield = s;

        list[n].SetActive(true);
        Rhand[curShield].SetActive(true);
        Lhand[n].SetActive(true);
    }

    public void UpGrade()
    {
        if(GoTier2==false&&GoTier3==false)
        {
            P_TierUp(GearSetList, 1, 1);
            GoTier2 = true;
            GearText.text = "Level 2";
        }


        gearbut[0].SetActive(false);
        gearbut[1].SetActive(true);
    }

    public void UpGrade2()
    {
        if (GoTier2 == true && GoTier3 == false)
        {
            P_TierUp(GearSetList, 2, 2);
            GoTier3 = true;
            GearText2.text = "Level Max";
        }
    }


}
