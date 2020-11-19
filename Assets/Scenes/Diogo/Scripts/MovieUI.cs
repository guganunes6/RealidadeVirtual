using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class MovieUI : MonoBehaviour
{
    public GameObject Title;

    private void Start()
    {

        this.gameObject.SetActive(false);
    }
    public void ShowUI(DecodedNode movie)
    {
        this.gameObject.SetActive(true);
        Title.GetComponent<TextMeshProUGUI>().SetText("Title: " + movie.getTitle());
    }
    public void HideUI()
    {
        this.gameObject.SetActive(false);
    }
}
