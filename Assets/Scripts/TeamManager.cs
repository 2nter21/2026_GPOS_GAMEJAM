using System.Collections.Generic;
using UnityEditor.U2D.Aseprite;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

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
    public TextMeshProUGUI plannerCountText;
    public TextMeshProUGUI programmerCountText;
    public TextMeshProUGUI artCountText;
    public TextMeshProUGUI totalCPSText;

    public Button addCrewButton;

    private int currentMemberPrice;
    private int memberPriceIncrement = 2;

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
        if(plannerCountText != null) plannerCountText.text = remainingPlanner.ToString();
        if(programmerCountText != null) programmerCountText.text = remainingProgrammer.ToString();
        if(artCountText != null) artCountText.text = remainingArt.ToString();
        if(totalCPSText != null) totalCPSText.text = totalCPS.ToString();
    }

    void OnAddCrewClicked()
    {
        if(!GameManager.Instance.SpendMoney(currentMemberPrice)) return;
        
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

        CheckTeamsToUnlock();

        UpdateMemberCountUI();
    }
}

