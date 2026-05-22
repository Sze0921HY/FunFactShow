using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.IO;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject game;
    public GameObject pauseMenu;
    public Image pauseUpperImage;
    public Image pauseLowerImage;

    public AudioManager audioManager;
    public GameManager gameManager;

    public Animation upAnim;
    public Animation downAnim;

    bool isProcessing = false;

    public void StartPause()
    {
        if (isProcessing) return;

        gameManager.OnPuase();
        audioManager.StopSFX();

        StartCoroutine(PauseFlow());
    }


/*    IEnumerator startAnimation()
    {
        downAnim["GoDown"].speed = 1f;
        upAnim["GoUp"].speed = 1f;

        upAnim["GoUp"].time = 0f;
        downAnim["GoDown"].time = 0f;

        yield return new WaitForSeconds(1f);
        audioManager.StopSFX();

        upAnim.Play();
        downAnim.Play();
        audioManager.switchPitchforPause();

    }*/

    IEnumerator resumeAnimation()
    {
        upAnim["GoUp"].speed = -1f;
        upAnim["GoUp"].time = upAnim["GoUp"].length;

        downAnim["GoDown"].speed = -1f;
        downAnim["GoDown"].time = downAnim["GoDown"].length;

        upAnim.Play();
        downAnim.Play();

        yield return new WaitForSeconds(0.12f);
        audioManager.switchPitchforPause();

        audioManager.continueSFX();
        gameManager.unPaused();
        game.SetActive(true);
        pauseMenu.SetActive(false);
    }

    IEnumerator PauseFlow()
    {
        isProcessing = true;

        // Let game fully update pause state
        yield return null;

        // Wait until end of frame AFTER state change
        yield return new WaitForEndOfFrame();

        int width = Screen.width;
        int height = Screen.height / 2;

        // ---------- TOP ----------
        Texture2D topTex = new Texture2D(width, height, TextureFormat.RGB24, false);

        topTex.ReadPixels(
            new Rect(0, Screen.height / 2, width, height),
            0, 0
        );
        topTex.Apply();

        pauseUpperImage.sprite = Sprite.Create(
            topTex,
            new Rect(0, 0, width, height),
            new Vector2(0.5f, 0.5f)
        );

        // ---------- BOTTOM ----------
        Texture2D bottomTex = new Texture2D(width, height, TextureFormat.RGB24, false);

        bottomTex.ReadPixels(
            new Rect(0, 0, width, height),
            0, 0
        );
        bottomTex.Apply();

        pauseLowerImage.sprite = Sprite.Create(
            bottomTex,
            new Rect(0, 0, width, height),
            new Vector2(0.5f, 0.5f)
        );

        // NOW show pause UI
        game.SetActive(false);
        pauseMenu.SetActive(true);

        // NOW start animation (NOT before capture)
        startAnimationImmediate();

        isProcessing = false;
    }

    public void Resume()
    {
        StartCoroutine(resumeAnimation());
    }

    void startAnimationImmediate()
    {
        downAnim["GoDown"].speed = 1f;
        upAnim["GoUp"].speed = 1f;

        upAnim["GoUp"].time = 0f;
        downAnim["GoDown"].time = 0f;

        upAnim.Play();
        downAnim.Play();

        audioManager.switchPitchforPause();
    }


    public void backMainMenu()
    {
        SceneManager.LoadScene(0);
    }

}