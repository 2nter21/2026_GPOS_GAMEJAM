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
    public Button maxPlusButton;
    public Button maxMinusButton;
    public TextMeshProUGUI reqPlannerCntText;
    public TextMeshProUGUI reqProgrammerCntText;
    public TextMeshProUGUI reqArtCntText;
    public TextMeshProUGUI teamCntText;
    public TextMeshProUGUI teamCPSText;
    public TextMeshProUGUI earningPopupText;

    private float earningPopupTimer = 0f;
    private const float earningPopupInterval = 1f;

    void Start()
    {
        if (recipe != null && genreNameText != null)
        {
            genreNameText.text = recipe.genreName;
        }

        updateTexts();

        if(plusButton != null) plusButton.onClick.AddListener(OnPlusClicked);
        if(minusButton != null) minusButton.onClick.AddListener(OnMinusClicked);
        if(maxPlusButton != null) maxPlusButton.onClick.AddListener(OnMaxPlusClicked);
        if(maxMinusButton != null) maxMinusButton.onClick.AddListener(OnMaxMinusClicked);
    }

    void Update()
    {
        if (recipe == null) return;

        if (recipe.teamCount > 0)
        {
            earningPopupTimer += Time.deltaTime;
            if (earningPopupTimer >= earningPopupInterval)
            {
                earningPopupTimer = 0f;
                EarnMoney();
            }
        }
        else
        {
            earningPopupTimer = 0f;
        }
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

    void OnMaxPlusClicked()
    {
        if (recipe == null) return;
        bool isSuccess = TeamManager.Instance.TryFormMaxTeam(recipe);

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

    void OnMaxMinusClicked()
    {
        if (recipe == null) return;
        bool isSuccess = TeamManager.Instance.TryDisbandMaxTeam(recipe);

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
        teamCntText.text = $"x{recipe.teamCount}";
        teamCPSText.text = $"CPS: {recipe.cpsReward}";
    }

    void EarnMoney()
    {
        if (recipe.teamCount > 0)
        {
            // Implement money earning Logic
            ShowEarningPopup();
        }
    }

    void ShowEarningPopup()
    {
        if(earningPopupText != null)
        {
            earningPopupText.text = $"+{recipe.cpsReward * recipe.teamCount}$";
            earningPopupText.gameObject.SetActive(true);
            Invoke("HideEarningPopup", 0.5f); // Hide after 0.5 second
        }
    }

    void HideEarningPopup()
    {
        if(earningPopupText != null)
        {
            earningPopupText.gameObject.SetActive(false);
        }
    }
}
