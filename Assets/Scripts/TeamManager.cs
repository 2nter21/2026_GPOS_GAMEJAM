using System.Collections.Generic;
using UnityEditor.U2D.Aseprite;
using UnityEngine;

public class TeamManager : MonoBehaviour
{
    public List<TeamRecipe> teamRecipes;

    public int allPlanner;
    public int allProgrammer;
    public int allArt;

    public int remainingPlanner;
    public int remainingProgrammer;
    public int remainingArt;

    public void CheckAvailableTeams()
    {
        Debug.Log($"기획자 : {remainingPlanner} / 프로그래머 : {remainingProgrammer} / 아트 : {remainingArt}");
        foreach(TeamRecipe recipe in teamRecipes)
        {
            if(allPlanner >= recipe.reqPlanner / 2 && allProgrammer >= recipe.reqProgrammer / 2 && allArt >= recipe.reqArt / 2) {
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
        // Initialize remaining #s
        remainingPlanner = allPlanner;
        remainingProgrammer = allProgrammer;
        remainingArt = allArt;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.D))
        {
            CheckAvailableTeams();
        }
    }

    
}
