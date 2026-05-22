using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private Coroutine buttonCoroutine;
    public AudioSource buttonSFX;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame()
    {
        buttonCoroutine = StartCoroutine(StartLevel());
    }


    IEnumerator StartLevel()
    {
        buttonSFX.Play();

        yield return new WaitForSeconds(1f);

        SceneManager.LoadScene(1);


    }

    public void Exit()
    {
        Application.Quit();
    }
}
