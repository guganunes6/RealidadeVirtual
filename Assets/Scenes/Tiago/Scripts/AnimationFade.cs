using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationFade : MonoBehaviour
{
    private bool isFadeIn = false;
    private bool isFadeIn2 = false;

    GameObject MovieUI;

    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void startAnimation(GameObject movie)
    {
        isFadeIn = true;
        MovieUI = movie;
        MovieUI.transform.localScale = new Vector3(0.0f, 0.0f, 1.0f);
    }
    public void doAnimation()
    {
        if (isFadeIn)
        {
            if (MovieUI.transform.localScale.x < 1.1f)
                MovieUI.transform.localScale += new Vector3(0.05f, 0.05f, 0.0f);
            else
            {
                isFadeIn = false;
                isFadeIn2 = true;
            }
        }

        if (isFadeIn2)
        {
            if (MovieUI.transform.localScale.x > 1.0f)
                MovieUI.transform.localScale -= new Vector3(0.005f, 0.005f, 0.0f);
            else
                isFadeIn2 = false;
        }
    }
    // Update is called once per frame
    void Update()
    {
        doAnimation();
    }
}
