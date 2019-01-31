using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ManagerWinScene : MonoBehaviour
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
        ManagerLevelsNight.ganaPesadilla = true;
        SceneManager.LoadScene("QuestionsGame");
        ManagerLevelsNight.currentLevelNight++;
    }
}
