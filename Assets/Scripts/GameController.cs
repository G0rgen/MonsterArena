using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameController : MonoBehaviour
{
    [SerializeField] private List<Monster> monsterPrefabs;

    [SerializeField] private Monster monsterA;
    [SerializeField] private Monster monsterB;

    [SerializeField] private MonsterUi monsterAUi;
    [SerializeField] private MonsterUi monsterBUi;

    [SerializeField] private TextMeshProUGUI commentaryText;
    
    private GameInput input;

    private bool isMonsterATurn = true;
    
    private void Awake()
    {
        input = new GameInput();

        input.Player.Next.performed += PerformNextAction;
    }

    private void OnEnable()
    {
        input.Enable();
    }

    private void OnDisable()
    {
        input.Disable();
    }

    private void OnDestroy()
    {
               input.Player.Next.performed -= PerformNextAction;
    }

    private void PerformNextAction(InputAction.CallbackContext context)
    {
        if (monsterA.HasFainted() || monsterB.HasFainted())
        {
            // TODO Spawn new monster
            return;
        }
        
        Monster attacker;
        Monster defender;
        MonsterUi defenderUi;

        if (isMonsterATurn) // Monster A greif an;
        {
            attacker = monsterA;
            defender = monsterB;
            defenderUi = monsterBUi;
        }
        else // Monster B greift an
        {
            attacker = monsterB;
            defender = monsterA;
            defenderUi = monsterAUi;
        }
        
        attacker.Attack(defender);
        UpdateHealth(defender, defenderUi);

        isMonsterATurn = !isMonsterATurn;
    }

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

