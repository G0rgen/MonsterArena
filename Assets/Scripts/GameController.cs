using System;
using TMPro;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private Monster monsterA;
    [SerializeField] private Monster monsterB;

    [SerializeField] private MonsterUi monsterAUi;
    [SerializeField] private MonsterUi monsterBUi;

    [SerializeField] private TextMeshProUGUI commentaryText;

    private void Start()
    {
        RegisterNewMonster(monsterA, monsterAUi);
        RegisterNewMonster(monsterB, monsterBUi);

        commentaryText.SetText($"{monsterA.GetTitle()} trifft auf {monsterB.GetTitle()}!");
    }

    private void RegisterNewMonster(Monster newMonster, MonsterUi newMonsterUi)
    {
        UpdateTitle(newMonster, newMonsterUi);
        UpdateHealth(newMonster, newMonsterUi);
    } 
    private void UpdateTitle(Monster newMonster, MonsterUi newMonsterUi)
    {
        newMonsterUi.UpdateTitle(newMonster.GetTitle());
    } 
    private void UpdateHealth(Monster newMonster, MonsterUi newMonsterUi)
    {
        newMonsterUi.UpdateHealth(newMonster.GetCurrentHealth(), newMonster.GetMaxHealth());
        
    }
    
}

