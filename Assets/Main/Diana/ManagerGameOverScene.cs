using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ManagerGameOverScene : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            
            ClicButtonContinue();

        }
    }

    void ClicButtonContinue()
    {
        ManagerLevelsNight.DetenerCorrutinas();
        if (ManagerLevelsNight.currentLevelNight == 0)
        {
            SceneManager.LoadScene("Pesadilla1");
        }
        else if (ManagerLevelsNight.currentLevelNight == 1)
        {
            SceneManager.LoadScene("Pesadilla2");
        }
        else if (ManagerLevelsNight.currentLevelNight == 2)
        {
            SceneManager.LoadScene("Pesadilla3");
        }
        else if (ManagerLevelsNight.currentLevelNight == 3)
        {
            SceneManager.LoadScene("Pesadilla4");
        }
        ManagerLevelsNight.ganaPesadilla = false;
    }
}
