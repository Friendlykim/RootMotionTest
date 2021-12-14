using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GoScene : MonoBehaviour
{
    public void GoDungeon()
    {
        SceneManager.LoadScene("Dungeon");
    }
    public void GoExit()
    {
        Application.Quit();
    }
}
