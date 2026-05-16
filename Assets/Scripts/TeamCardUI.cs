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
    public TextMeshProUGUI reqPlannerCntText;
    public TextMeshProUGUI reqProgrammerCntText;
    public TextMeshProUGUI reqArtCntText;
    public TextMeshProUGUI teamCntText;
    public TextMeshProUGUI teamCPSText;

    void Start()
    {
        if (recipe != null && genreNameText != null)
        {
            genreNameText.text = recipe.genreName;
        }

        updateTexts();

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
            updateTexts();
        }
        else
        {
            // when team form failed
        }
    }

    void OnMinusClicked()
    {
        if (recipe == null) return;
        bool isSuccess = TeamManager.Instance.TryDisbandTeam(recipe);

        if(isSuccess)
        {
            // when team dissolve successed
            updateTexts();
        }
        else
        {
            // when team dissolve failed
        }
    }

    void updateTexts()
    {
        reqPlannerCntText.text = recipe.reqPlanner.ToString();
        reqProgrammerCntText.text = recipe.reqProgrammer.ToString();
        reqArtCntText.text = recipe.reqArt.ToString();
        teamCntText.text = recipe.teamCount.ToString();
        teamCPSText.text = $"CPS: {recipe.cpsReward}";
    }
}
