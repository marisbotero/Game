using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public CanvasGroup textCredits;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && textCredits.alpha == 1)
        {
            textCredits.alpha = 0;
        }
    }

    public void clickOption (int id)
    {
        if (id == 0)
        {
            SceneManager.LoadSceneAsync("QuestionsGame");
        } else if (id == 2)
        {
            Application.Quit();
        } else if (id == 1)
        {
            textCredits.alpha = 1;
        }
    }
}
