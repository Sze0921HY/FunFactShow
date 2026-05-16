using NUnit.Framework;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;


public class QuestionManager : MonoBehaviour
{
    public Image QuestionImage;

    public QuestionScript questionScript;
    public ButtonManager buttonManager;
    public GameManager gameManager;
    public int currentQuestionIndex;
    public TextMeshProUGUI Question_Text;

    public List<TextMeshProUGUI> Options_Text;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {

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
}
