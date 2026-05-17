using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.U2D.Aseprite;
using UnityEngine;
using UnityEngine.UI;

public class TeamManager : MonoBehaviour
{
    public static TeamManager Instance;

    public List<TeamCardUI> teamRecipeUIs;


    [Header("Current Members")]
    public int allPlanner;
    public int allProgrammer;
    public int allArt;

    public int remainingPlanner;
    public int remainingProgrammer;
    public int remainingArt;

    [Header("total CPS")]
    public float totalCPS;

    [Header("Member Count UI")]
    public StatusCounter plannerCountText;
    public StatusCounter programmerCountText;
    public StatusCounter artCountText;
    public TextMeshProUGUI totalCPSText;

    [Header("Upgrade Buttons")]
    public Button upgradeClick;
    public Button addCrewButton;

    private int currentMemberPrice;
    private int memberPriceIncrement = 2;

    [Header("Text Bubble")]
    public TextMeshProUGUI clickUpgradeText;
    public TextMeshProUGUI recuitingMemberText;

    [Header("Alert")]
    public GameObject MoneyAlert;
    public Canvas canvas;

    private int moneyPerClick = 100;
    private int clickUpgradePrice = 5000;
    private int clickUpgradeIncrement = 2;

    void Awake()
    {
        if(Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    void Start()
    {
        // Initialize members
        remainingPlanner = allPlanner = 0;
        remainingProgrammer = allProgrammer = 0;
        remainingArt = allArt = 0;
        currentMemberPrice = 10;
        UpdateMemberCountUI();

        if(addCrewButton != null) addCrewButton.onClick.AddListener(OnAddCrewClicked);
        if (upgradeClick != null) upgradeClick.onClick.AddListener(OnUpgradeClick);
    }

    public bool TryFormTeam(TeamRecipe recipe)
    {
        if(remainingPlanner >= recipe.reqPlanner && remainingProgrammer >= recipe.reqProgrammer && remainingArt >= recipe.reqArt)
        {
            remainingPlanner -= recipe.reqPlanner;
            remainingProgrammer -= recipe.reqProgrammer;
            remainingArt -= recipe.reqArt;

            recipe.teamCount++;

            totalCPS += recipe.cpsReward;

            UpdateMemberCountUI();
            return true;
        }

        return false;
    }

    public void CheckTeamsToUnlock()
    {
        foreach(TeamCardUI teamCard in teamRecipeUIs)
        {
            if(!teamCard.recipe.isUnlocked)
            {
                if(allPlanner >= teamCard.recipe.reqPlanner/2f && allProgrammer >= teamCard.recipe.reqProgrammer/2f && allArt >= teamCard.recipe.reqArt/2f)
                {
                    teamCard.UnLockRecipe();
                }
            }
        }
    }

    public bool TryDisbandTeam(TeamRecipe recipe)
    {
        if(recipe.teamCount > 0)
        {
            remainingPlanner += recipe.reqPlanner;
            remainingProgrammer += recipe.reqProgrammer;
            remainingArt += recipe.reqArt;

            recipe.teamCount--;

            totalCPS -= recipe.cpsReward;

            UpdateMemberCountUI();
            return true;
        }

        return false;
    }

    public bool TryFormMaxTeam(TeamRecipe recipe)
    {
        List<int> maxTeamsValues = new List<int>();
        
        if(recipe.reqPlanner > 0) maxTeamsValues.Add(remainingPlanner / recipe.reqPlanner);
        if(recipe.reqProgrammer > 0) maxTeamsValues.Add(remainingProgrammer / recipe.reqProgrammer);
        if(recipe.reqArt > 0) maxTeamsValues.Add(remainingArt / recipe.reqArt);

        int maxTeamsPossible = (maxTeamsValues.Count > 0) ? Mathf.Min(maxTeamsValues.ToArray()) : 0;

        if(maxTeamsPossible > 0)
        {
            remainingPlanner -= maxTeamsPossible * recipe.reqPlanner;
            remainingProgrammer -= maxTeamsPossible * recipe.reqProgrammer;
            remainingArt -= maxTeamsPossible * recipe.reqArt;

            recipe.teamCount += maxTeamsPossible;

            totalCPS += maxTeamsPossible * recipe.cpsReward;

            UpdateMemberCountUI();
            return true;
        }

        return false;
    }

    public bool TryDisbandMaxTeam(TeamRecipe recipe)
    {
        if(recipe.teamCount > 0)
        {
            int teamsToDisband = recipe.teamCount;

            remainingPlanner += teamsToDisband * recipe.reqPlanner;
            remainingProgrammer += teamsToDisband * recipe.reqProgrammer;
            remainingArt += teamsToDisband * recipe.reqArt;

            recipe.teamCount = 0;

            totalCPS -= teamsToDisband * recipe.cpsReward;

            UpdateMemberCountUI();
            return true;
        }

        return false;
    }

    public void UpdateMemberCountUI()
    {
        if(plannerCountText != null) plannerCountText.setCount(remainingPlanner);
        if(programmerCountText != null) programmerCountText.setCount(remainingProgrammer);
        if(artCountText != null) artCountText.setCount(remainingArt);
        if(totalCPSText != null) totalCPSText.text = totalCPS.ToString();
    }

    void OnAddCrewClicked()
    {
        if (!GameManager.Instance.SpendMoney(currentMemberPrice))
        {
            OnMoneyAlert();
            return;
        }

            int crewType = Random.Range(0, 3);

        switch (crewType)
        {
            case 0: 
                allPlanner++;
                remainingPlanner++;
                break;
            case 1:
                allProgrammer++;
                remainingProgrammer++;
                break;
            case 2:
                allArt++;
                remainingArt++;
                break;
        }

        currentMemberPrice *= memberPriceIncrement;
        recuitingMemberText.text = $"Recruiting member\nCost : {currentMemberPrice}";

        CheckTeamsToUnlock();

        UpdateMemberCountUI();
    }

    void OnUpgradeClick()
    {
        if (!GameManager.Instance.SpendMoney(clickUpgradePrice))
        {
            OnMoneyAlert();
            return;
        }

        moneyPerClick *= clickUpgradeIncrement;
        clickUpgradePrice *= clickUpgradeIncrement;
        clickUpgradeText.text = $"Upgrade the money earned per click" +
            $"\n{moneyPerClick} --> {moneyPerClick*clickUpgradeIncrement}" +
            $"\nCost : {clickUpgradePrice}";
    }

    void OnMoneyAlert()
    {
        GameObject obj = Instantiate(MoneyAlert, canvas.transform);

        RectTransform rect = obj.GetComponent<RectTransform>();

        Vector2 localPos;

        RectTransform canvasRect = canvas.GetComponent<RectTransform>();

        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            canvasRect,
            Input.mousePosition,
            canvas.renderMode == RenderMode.ScreenSpaceOverlay ? null : canvas.worldCamera,
            out localPos
        );

        rect.anchoredPosition = localPos;
    }
}

