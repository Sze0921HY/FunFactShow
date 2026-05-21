using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static GameManager;

public class ButtonManager : MonoBehaviour
{
    //References
    public GameManager gameManager;


    public List<Button> buttonList;
    public QuestionScript questionScript;
    public int currentQuestionIndex;
    public bool isClicked = false;

    public List<Sprite> sprites;
    public AudioManager audioManager;

    private void Awake()
    {
        for (int i = 0; i < buttonList.Count; i++)
        {
            int index = i;
            buttonList[i].onClick.AddListener(() => OnButtonClicked(index));
        }

    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    public void ButtonCheck(int index)
    {


        var question = questionScript.questionList[gameManager.questionOrder[currentQuestionIndex]];

        bool isCorrect = (index == question.correctAnswerIndex);



        if (isCorrect)
        {
            audioManager.playCorrectSFX();
            buttonList[index].image.sprite = sprites[0];

        }
        else
        {
            audioManager.playWrongSFX();
            buttonList[index].image.sprite = sprites[1];

        }

        gameManager.OnAnswerSubmitted(isCorrect);

    }

    public void ButtonReset()
    {
        for(int i = 0; i < buttonList.Count; i++)
        {
            buttonList[i].image.sprite = sprites[2];
        } 
    }


    public void OnButtonClicked(int index)
    {
        if (!isClicked && gameManager.currentState == GameState.Answering)
        {
            isClicked = true;
            ButtonCheck(index);
        }
        else if(isClicked)
        {
            Debug.Log("already clicked");
        }
        else
        {
            Debug.Log("Wrong State");
        }
    }


    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateButton(int QuestionIndex)
    {
        currentQuestionIndex = QuestionIndex;
        isClicked = false;
    }
}
