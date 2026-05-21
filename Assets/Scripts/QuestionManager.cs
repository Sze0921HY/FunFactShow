using NUnit.Framework;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;
using static GameManager;


public class QuestionManager : MonoBehaviour
{
    public Image QuestionImage;

    public QuestionScript questionScript;
    public ButtonManager buttonManager;
    public GameManager gameManager;
    public int currentQuestionIndex;
    public TextMeshProUGUI Question_Text;
    public TextMeshProUGUI Timer;
    public List<TextMeshProUGUI> Options_Text;
    public int timer;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        timer = (int)gameManager.answeringTime;
    }

    private void Start()
    {

    }

    public void showQuestion(int QuestionIndex)
    {
        currentQuestionIndex = QuestionIndex;

        var question = questionScript.questionList[gameManager.questionOrder[currentQuestionIndex]];


        QuestionImage.sprite = question.image;

        Question_Text.text = question.question;

        for (int i = 0; i < 4; i++)
        {
            Options_Text[i].text = question.options[i];
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CountDown()
    {
        StartCoroutine(counting());
    }

    IEnumerator counting()
    {
        for (int i = timer; i > 0; i--)
        {
            if (gameManager.currentState != GameState.Answering)
                yield break;

            Timer.text = i.ToString();
            yield return new WaitForSeconds(1f);
        }

        Timer.text = "0";
    }
}
