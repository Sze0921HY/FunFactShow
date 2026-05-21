using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public enum GameState
    {
        StartQuestion,
        Answering,
        Result,
        Finished,
        GameOver,
        Paused,
    }

    public GameState currentState;

    public float questionTime;
    public float answeringTime;
    public float resultTime;
    public float finishedTime;
    public bool answered = false;
    public int currentQuestionIndex;
    private Coroutine stateRoutine;
    public GameState OldState;

    public List<int> questionOrder = new List<int>();

    //Referecne
    public QuestionScript questionScript;
    public QuestionManager questionManager;
    public ButtonManager buttonManager;
    public AudioManager audioManager;
    public PauseMenu pauseMenu;

    private void Awake()
    {

    }

    void Start()
    {
        for (int i = 0; i < questionScript.questionList.Count; i++)
        {
            questionOrder.Add(i);
        }
        Shuffle(questionOrder);
        currentQuestionIndex = 0;
        ChangeState(GameState.StartQuestion);
    }

    public void ChangeState(GameState newState)
    {
        if (stateRoutine != null)
        {
            StopCoroutine(stateRoutine);
        }

        currentState = newState;

        if (newState == GameState.Answering) 
        {
            answered = false;
        }


        switch (currentState)
        {
            case GameState.StartQuestion:
                stateRoutine = StartCoroutine(StartQuestionFlow());
                break;

            case GameState.Answering:
                stateRoutine = StartCoroutine(AnsweringFlow());
                break;

            case GameState.Result:
                stateRoutine = StartCoroutine(ResultFlow());
                break;

            case GameState.Finished:
                stateRoutine = StartCoroutine(FinishedFlow());
                break;

            case GameState.GameOver:
                audioManager.AllaudioStop();
                break;
        }


    }

    IEnumerator StartQuestionFlow()
    {
        //Debug.Log("Show Question");
        buttonManager.ButtonReset();

        questionManager.showQuestion(currentQuestionIndex);

        yield return new WaitForSeconds(questionTime);
        ChangeState(GameState.Answering);
    }

    IEnumerator AnsweringFlow()
    {
        answeringTime -= 3;

        yield return new WaitForSeconds(answeringTime);

        audioManager.PlayCountdownSFX();


        //Debug.Log("Player Answering");
        yield return new WaitForSeconds(3);
        ChangeState(GameState.Result);
    }

    IEnumerator ResultFlow()
    {
        //Debug.Log("Showing Result");
        yield return new WaitForSeconds(resultTime);

        if (answered)
        {
            ChangeState(GameState.Finished);
        }
        else 
        {
            ChangeState(GameState.GameOver);
        }
    }

    IEnumerator FinishedFlow()
    {
        currentQuestionIndex++;
        buttonManager.UpdateButton(currentQuestionIndex);
        //Debug.Log("questionList.Count = " + questionScript.questionList.Count);
        yield return new WaitForSeconds(finishedTime);


        ChangeState(GameState.StartQuestion);
    }

    public void OnAnswerSubmitted(bool isCorrect)
    {
        answered = true;

        if (isCorrect)
        {
            ChangeState(GameState.Result);
        }
        else
        {
            ChangeState(GameState.GameOver);

        }
    }

    public void OnPuase()
    {
        OldState = currentState;
        ChangeState(GameState.Paused);
    }

    public void unPaused()
    {
        ChangeState(OldState);
    }

    void Shuffle(List<int> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            int rand = Random.Range(i, list.Count);

            int temp = list[i];
            list[i] = list[rand];
            list[rand] = temp;
        }
    }
}