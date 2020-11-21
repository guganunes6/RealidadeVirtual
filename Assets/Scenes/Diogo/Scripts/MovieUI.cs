using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class MovieUI : MonoBehaviour
{
    public GameObject Text;

    private void Start()
    {
        this.gameObject.SetActive(false);
    }
    public void ShowUI(DecodedNode movie)
    {
        //title - genres - voteAverage - releaseDate - budget - revenue
        this.gameObject.SetActive(true);
        Text.GetComponent<TextMeshProUGUI>().SetText("<b>Title:</b> " + movie.getTitle() + "\n" + "<b>Genres:</b> " + getStringsFromList(movie.getGenres()) + "\n" + "<b>IMDB:</b> " + movie.getVoteAverage() + "\n" + "<b>Release Date:</b> " + movie.getReleaseDate()/* + "\n" + "Budget: " + movie.getBudget() + "\n" + "Revenue: " + movie.getRevenue()*/);
    }
    public void HideUI()
    {
        this.gameObject.SetActive(false);
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
