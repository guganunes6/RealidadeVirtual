using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class MovieUI : MonoBehaviour
{
    public GameObject Text;
    public Animator animator;
    public GameObject ParentUI;


    private void Start()
    {
        animator = ParentUI.GetComponent<Animator>();
        animator.SetTrigger("Normal");
        ParentUI.gameObject.SetActive(false);

    }
    public void ShowUI(DecodedNode movie)
    {
        //title - genres - voteAverage - releaseDate - budget - revenue
        ParentUI.gameObject.SetActive(true);
        animator.SetTrigger("Show");
        Text.GetComponent<TextMeshProUGUI>().SetText("<b>Title:</b> " + movie.getTitle() + "\n" + "<b>Genres:</b> " + getStringsFromList(movie.getGenres()) + "\n" + "<b>IMDB:</b> " + movie.getVoteAverage() + "\n" + "<b>Release Date:</b> " + movie.getReleaseDate()/* + "\n" + "Budget: " + movie.getBudget() + "\n" + "Revenue: " + movie.getRevenue()*/);
    }
    public void HideUI()
    {
        ParentUI.gameObject.SetActive(false);
        ParentUI.transform.localScale = new Vector3(0.0f, 0.0f, 1.0f);
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
}
