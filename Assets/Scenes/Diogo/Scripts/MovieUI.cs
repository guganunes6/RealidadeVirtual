using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class MovieUI : MonoBehaviour
{
    public GameObject Text;
    private bool isFadeIn = false;
    private bool isFadeIn2 = false;
    private bool isFadeOut = false;
    private bool isFadeOut2 = false;

    private void Start()
    {
        this.gameObject.SetActive(false);
        this.transform.localScale = new Vector3(0.0f, 0.0f, 1.0f);
    }
    public void ShowUI(DecodedNode movie)
    {
        isFadeIn = true;

        //title - genres - voteAverage - releaseDate - budget - revenue
        this.gameObject.SetActive(true);
        Text.GetComponent<TextMeshProUGUI>().SetText("<b>Title:</b> " + movie.getTitle() + "\n" + "<b>Genres:</b> " + getStringsFromList(movie.getGenres()) + "\n" + "<b>IMDB:</b> " + movie.getVoteAverage() + "\n" + "<b>Release Date:</b> " + movie.getReleaseDate()/* + "\n" + "Budget: " + movie.getBudget() + "\n" + "Revenue: " + movie.getRevenue()*/);
    }
    public void HideUI()
    {
        isFadeIn = false;
        isFadeOut = true;
    }

    private string getStringsFromList(List<string> strings)
    {
        string decodedString = null;
        for (int i = 0; i < strings.Count; i++)
        {
            if (i == strings.Count - 1)
            {
                decodedString += strings[i];
            }
            else
            {
                decodedString += strings[i] + ", ";
            }
        }
        return decodedString;
    }

    private void animationUI()
    {
        if (isFadeIn)
        {
            if (this.transform.localScale.x < 1.1f)
                this.transform.localScale += new Vector3(0.05f, 0.05f, 0.0f);
            else
            {
                isFadeIn = false;
                isFadeIn2 = true;
            }
        }

        if (isFadeIn2)
        {
            if (this.transform.localScale.x > 1.0f)
                this.transform.localScale -= new Vector3(0.005f, 0.005f, 0.0f);
            else
                isFadeIn2 = false;
        }

        if (isFadeOut)
        {
            if (this.transform.localScale.x < 1.1f)
                this.transform.localScale += new Vector3(0.005f, 0.005f, 0.0f);
            else
            {
                isFadeOut = false;
                isFadeOut2 = true;
            }

        }

        if (isFadeOut2)
        {
            if (this.transform.localScale.x > 0.0f)
                this.transform.localScale -= new Vector3(0.05f, 0.05f, 0.0f);
            else
            {
                isFadeOut2 = false;
                this.gameObject.SetActive(false);
            }
        }
    }

    private void Update()
    {
        //animationUI();
    }
}
