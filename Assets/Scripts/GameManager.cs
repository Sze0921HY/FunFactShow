using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;

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

    [Header("Time stats")]
    public float questionTime;
    public float answeringTime;
    public float resultTime;
    public float finishedTime;
    public bool answered = false;
    public bool isCorrectAnswer;
    public int currentQuestionIndex;
    private Coroutine stateRoutine;
    public GameState OldState;

    [Header("Time stats --- More detail")]
    public float answeringTime_1;
    public float finishedTime_1;

    public List<int> questionOrder = new List<int>();

    [Header("GameOver")]
    public GameObject EndGamePanel;
    public GameObject WinText;
    public GameObject LoseText;
    public GameObject PerfectText;


    public TextMeshProUGUI totalQuestionAnsweredText;

    bool perfectEnding;

    //Referecne
    public QuestionScript questionScript;
    public QuestionManager questionManager;
    public ButtonManager buttonManager;
    public AudioManager audioManager;
    public Transition transition;

    private void Awake()
    {
        answeringTime_1 = 3;
        finishedTime_1 = 1f;
        answeringTime -= answeringTime_1;
        finishedTime -= finishedTime_1;
        perfectEnding = false;
        isCorrectAnswer = false;
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

    void CheckQuestionIndex()
    {
        if (!(currentQuestionIndex < questionOrder.Count))
        {
            perfectEnding = true;
            ChangeState(GameState.GameOver);
        }
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
                EndGame(currentQuestionIndex==0, perfectEnding);
                break;
        }


    }

    IEnumerator StartQuestionFlow()
    {
        questionManager.showQuestion(currentQuestionIndex);

        transition.transitionGoUp();

        //Debug.Log("Show Question");
        buttonManager.ButtonReset();

        yield return new WaitForSeconds(questionTime);
        ChangeState(GameState.Answering);
    }

    IEnumerator AnsweringFlow()
    {
        questionManager.CountDown();

        yield return new WaitForSeconds(answeringTime);

        audioManager.PlayCountdownSFX();

        //Debug.Log("Player Answering");
        yield return new WaitForSeconds(answeringTime_1);
        ChangeState(GameState.Result);
    }

    IEnumerator ResultFlow()
    {
        //Debug.Log("Showing Result");

        yield return new WaitForSeconds(resultTime);

        if (answered && isCorrectAnswer)
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


        transition.transitionGoDown();
        
        yield return new WaitForSeconds(finishedTime);

        currentQuestionIndex++;

        CheckQuestionIndex();

        buttonManager.UpdateButton(currentQuestionIndex);
        questionManager.ResetCounting();

        yield return new WaitForSeconds(finishedTime_1);
        ChangeState(GameState.StartQuestion);
    }

    public void OnAnswerSubmitted(bool isCorrect)
    {
        answered = true;
        isCorrectAnswer = isCorrect;
        ChangeState(GameState.Result);
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

    public void EndGame(bool isLosing, bool isPerfect)
    {
        audioManager.AllaudioStop();
        totalQuestionAnsweredText.text = currentQuestionIndex.ToString();

        if (!isLosing)
        {
            audioManager.TriggerCheer(true);
        }
        else
        {
            audioManager.playSFX_Disappoint();
        }

        if (isPerfect)
        {
            PerfectText.SetActive(true);
            WinText.SetActive(false);
            LoseText.SetActive(false);
            
        }
        else
        {
            if (isLosing)
            {
                LoseText.SetActive(true);
                WinText.SetActive(false);
                PerfectText.SetActive(false);

            }
            else
            {
                WinText.SetActive(true);
                LoseText.SetActive(false);
                PerfectText.SetActive(false);
            }
        }
        EndGamePanel.SetActive(true);
    }

}