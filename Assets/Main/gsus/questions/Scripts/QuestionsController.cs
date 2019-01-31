using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[System.Serializable]
public struct questionsManager
{
    public string questionText;
    public string answer1Text;
    public string answer2Text;
    public string answer1Dialog;
    public string answer2Dialog;
    public Sprite backgroundImg;
    public Sprite imgAnswer1;
}

public class QuestionsController : MonoBehaviour
{

    public questionsManager [] questions;
    public Button btnAnswer1;
    public Button btnAnswer2;
    public Text lbQuestion;
    public Image currentBackground;
    public Sprite[] firstScenes;
    public Sprite afternoon;
    public Text dialogParents;

    public Sprite closeImage;

    public CanvasGroup groupAlpha;
    public CanvasGroup dialog;
    public Image curtain;

    private bool currentAnswer1 = false;
    private bool currentAnswer2 = false;

    public float curtainSeed = 0.1f;
    
    private Sprite nextBackground;
    private static int countQuestion = 0;
    private static int countIndexImgs = 2;


    private void Awake()
    {
        if (countIndexImgs == 0)
        {
            hideAllObj(true);
            dialog.alpha = 0;
            currentBackground.sprite = firstScenes[countIndexImgs];
            startCurtainIn();
            countIndexImgs++;
            StartCoroutine(countFistImgs());
        } else {
            asignValuesDefault();
        }
    }


    void startCurtainIn()
    {
        StartCoroutine(countCurtainIn());
    }

    void startCurtainOut()
    {
        StartCoroutine(countCurtainOut());
    }

    IEnumerator countCurtainIn()
    {
        Image compCurtain = curtain.GetComponent<Image>();
        while (true)
        {
            float alpha = compCurtain.color.a - curtainSeed * Time.deltaTime;
            compCurtain.color = new Color(0.0f, 0.0f, 0.0f, alpha);
            yield return null;
            if (alpha <= 0)
            {
                break;
            }
        }
    }

    IEnumerator countCurtainOut()
    {
        Image compCurtain = curtain.GetComponent<Image>();
        while (true)
        {
            float alpha = compCurtain.color.a + curtainSeed * Time.deltaTime;
            compCurtain.color = new Color(0.0f, 0.0f, 0.0f, alpha);
            yield return null;
            if (alpha >= 1)
            {
                break;
            }
        }
    }


    void startCountDialog(bool answer)
    {
        StartCoroutine(countDialog(answer));
    }

    IEnumerator countDialog(bool answer)
    {
        yield return countCurtainOut();
        if (answer)
        {
            currentBackground.sprite = questions[countQuestion].imgAnswer1;
        }
        else
        {
            currentBackground.GetComponent<Image>().color = Color.black;
        }
        hideAllObj(true);
        yield return countCurtainIn();
        yield return new WaitForSeconds(5.0f);
        yield return countCurtainOut();
        if (countQuestion % 2 != 0)
        {
            currentBackground.GetComponent<Image>().color = Color.white;
            currentBackground.sprite = questions[countQuestion].backgroundImg;
            asignValuesDefault();
            hideAllObj(false);
            yield return countCurtainIn();
        } else {
            currentBackground.GetComponent<Image>().color = Color.white;
            dialog.alpha = 0;
            resultFinal();
            currentBackground.sprite = closeImage;
            yield return countCurtainOut();
            SceneManager.LoadSceneAsync("WinPesadilla");
        }
    }

    IEnumerator countFistImgs()
    {
        yield return new WaitForSeconds(4.0f);
        yield return countCurtainOut();
        currentBackground.sprite = firstScenes[countIndexImgs];
        startCurtainIn();
        countIndexImgs++;
        yield return new WaitForSeconds(4.0f);
        yield return countCurtainOut();
        currentBackground.sprite = firstScenes[countIndexImgs];
        startCurtainIn();
        yield return new WaitForSeconds(4.0f);
        yield return countCurtainOut();
        resultFinal();
        SceneManager.LoadSceneAsync("WinPesadilla");
    }

    private void resultFinal ()
    {
        if (countQuestion == 0)
        {
            ManagerLevelsNight.buffers = ManagerLevelsNight.Buffs.nada;
        } else {
            if (currentAnswer1 == false && currentAnswer2 == false)
            {
                ManagerLevelsNight.buffers = ManagerLevelsNight.Buffs.negativo;
            }
            else if ((currentAnswer1 == false && currentAnswer2 == true) || (currentAnswer1 == true && currentAnswer2 == false))
            {
                ManagerLevelsNight.buffers = ManagerLevelsNight.Buffs.balanceado;
                countQuestion++;
            }
            else if (currentAnswer1 == true && currentAnswer2 == true)
            {
                ManagerLevelsNight.buffers = ManagerLevelsNight.Buffs.perfecto;
                countQuestion++;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void clickAnswer (bool answer)
    {
        if (answer)
        {
            currentAnswer1 = answer;            
            dialogParents.text = questions[countQuestion].answer1Dialog;
        }
        else
        {
            currentAnswer2 = answer;
            dialogParents.text = questions[countQuestion].answer2Dialog;
        }

        countQuestion++;
        startCountDialog(answer);
    }


    private void hideAllObj (bool hide)
    {
        if (hide)
        {
            groupAlpha.alpha = 0;
            dialog.alpha = 1;
        } else
        {
            groupAlpha.alpha = 1;
            dialog.alpha = 0;
        }
    }


    private void asignValuesDefault ()
    {
        if (countQuestion % 2 != 0)
        {
            currentBackground.sprite = afternoon;
            startCurtainIn();
        } else
        {
            currentBackground.sprite = questions[countQuestion].backgroundImg;
            startCurtainIn();
        }
       
        lbQuestion.text = questions[countQuestion].questionText;
        btnAnswer1.GetComponentInChildren<Text>().text = questions[countQuestion].answer1Text;
        btnAnswer2.GetComponentInChildren<Text>().text = questions[countQuestion].answer2Text;
    }
}
