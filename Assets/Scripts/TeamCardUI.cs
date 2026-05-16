using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TeamCardUI : MonoBehaviour
{
    [Header("Data Connetion")]
    public TeamRecipe recipe;

    [Header("UI Components")]
    public TextMeshProUGUI genreNameText;
    public Button plusButton;
    public Button minusButton;
    // public Button maxPlusButton;
    // public Button maxMinusButton;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (recipe != null && genreNameText != null)
        {
            genreNameText.text = recipe.genreName;
        }

        if(plusButton != null) plusButton.onClick.AddListener(OnPlusClicked);
        if(minusButton != null) minusButton.onClick.AddListener(OnMinusClicked);
    }

    void OnPlusClicked()
    {
        if (recipe == null) return;
        bool isSuccess = TeamManager.Instance.TryFormTeam(recipe);

        if(isSuccess)
        {
            // when team form successed
        }
        else
        {
            // when team form failed
        }
    }

    void OnMinusClicked()
    {
        
    }
}
