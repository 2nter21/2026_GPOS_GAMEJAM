using System.Collections.Generic;
using UnityEditor.U2D.Aseprite;
using UnityEngine;

public class TeamManager : MonoBehaviour
{
    public static TeamManager Instance;

    public List<TeamRecipe> teamRecipes;

    [Header("Current Members")]
    public int allPlanner;
    public int allProgrammer;
    public int allArt;

    public int remainingPlanner;
    public int remainingProgrammer;
    public int remainingArt;

    [Header("total CPS")]
    public float totalCPS;

    private Dictionary<string, int> formedTeams = new Dictionary<string, int>();

    void Awake()
    {
        if(Instance == null) Instance = this;
        else Destroy(gameObject);
    }
    
    public void CheckAvailableTeams()
    {
        Debug.Log($"기획자 : {remainingPlanner} / 프로그래머 : {remainingProgrammer} / 아트 : {remainingArt}");
        foreach(TeamRecipe recipe in teamRecipes)
        {
            if(allPlanner >= recipe.reqPlanner / 2f && allProgrammer >= recipe.reqProgrammer / 2f && allArt >= recipe.reqArt / 2f) {
                Debug.Log($"{recipe.genreName} 팀 결성 가능! ({recipe.reqPlanner}, {recipe.reqProgrammer}, {recipe.reqArt} 필요 / 예상 수익: {recipe.cpsReward} CPS)");
            }
            else
            {
                Debug.Log($"{recipe.genreName} 팀 결성 불가 (부원 부족)");
            }
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Initialize members
        remainingPlanner = allPlanner = 0;
        remainingProgrammer = allProgrammer = 0;
        remainingArt = allArt = 0;
    }

    // Update is called once per frame
    void Update()
    {
    
    }

    public bool TryFormTeam(TeamRecipe recipe)
    {
        if(allPlanner >= recipe.reqPlanner / 2f 
            && allProgrammer >= recipe.reqProgrammer / 2f && allArt >= recipe.reqArt / 2f)
        {
            allPlanner -= recipe.reqPlanner;
            allProgrammer -= recipe.reqProgrammer;
            allArt -= recipe.reqArt;

            recipe.teamCount++;

            totalCPS += recipe.cpsReward;

            Debug.Log($"{recipe.genreName} 팀 결성 성공! 현재 총 CPS: {totalCPS}");
            return true;
        }

        Debug.LogWarning($"{recipe.genreName} 팀 결성 실패: 부원이 부족합니다.");
        return false;
    }
    
}
