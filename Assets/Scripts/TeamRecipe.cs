using UnityEngine;

[CreateAssetMenu(fileName = "TeamRecipe", menuName = "Scriptable Objects/TeamRecipe")]
public class TeamRecipe : ScriptableObject
{
    [Header("Team Info")]
    public Sprite Icon;
    public string genreName;
    
    [Header("Required Team")]
    public int reqPlanner;
    public int reqProgrammer;
    public int reqArt;
    
    [Header("CPS")]
    public float cpsReward;

    public int teamCount;
    public bool isUnlocked;

    void OnEnable()
    {
        teamCount = 0;
        isUnlocked = false;
    }
}
