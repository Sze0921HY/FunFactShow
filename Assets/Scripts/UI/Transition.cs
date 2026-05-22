using UnityEngine;

public class Transition : MonoBehaviour
{
    public GameObject transition;
    public Animation anim;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        transitionGoUp();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void transitionGoUp()
    {
        anim["TransitionGo"].speed = 1f;
        anim["TransitionGo"].time = 0f;
        anim.Play();
    }

    public void transitionGoDown()
    {
        anim["TransitionGo"].speed = -1f;
        anim["TransitionGo"].time = anim["TransitionGo"].length;
        anim.Play();
    }
}
