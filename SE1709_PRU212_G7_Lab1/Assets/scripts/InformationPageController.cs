using UnityEngine;
using UnityEngine.UI;

public class InformationPageController : MonoBehaviour
{
    public GameObject[] pages;
    public Button nextButton, prevButton;
    int currentPage = 0;

    void Start()
    {
        ShowPage(currentPage);
        nextButton.onClick.AddListener(() => ChangePage(1));
        prevButton.onClick.AddListener(() => ChangePage(-1));
    }

    void ChangePage(int direction)
    {
        currentPage = Mathf.Clamp(currentPage + direction, 0, pages.Length - 1);
        ShowPage(currentPage);
    }

    void ShowPage(int index)
    {
        for (int i = 0; i < pages.Length; i++)
            pages[i].SetActive(i == index);

        prevButton.interactable = index > 0;
        nextButton.interactable = index < pages.Length - 1;
    }
}