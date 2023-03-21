using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameController : MonoBehaviour
{
    [SerializeField] private Transform monsterSlotA;
    [SerializeField] private Transform monsterSlotB;
    
    [SerializeField] private List<Monster> monsterPrefabs;

     private Monster monsterA;
     private Monster monsterB;

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
           StartNewBattle();
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

    private void StartNewBattle()
    {
        int challengerAIndex = Random.Range(0, monsterPrefabs.Count);
        int challengerBIndex = Random.Range(0, monsterPrefabs.Count);
        // TODO Generate new challengerBIndex if the same as challengerAIndex
        // Tip: use a while loop

        Monster challengerA = monsterPrefabs[challengerAIndex];
        Monster challengerB = monsterPrefabs[challengerBIndex];

        monsterA = RegisterNewMonster(challengerA, monsterSlotA, monsterAUi);
        monsterB = RegisterNewMonster(challengerB, monsterSlotB, monsterBUi);
        
        commentaryText.SetText($"{monsterA.GetTitle()} trifft auf {monsterB.GetTitle()}!");
    }
    

    private void Start()
    {
        StartNewBattle();

    }

    private Monster RegisterNewMonster(Monster monsterPrefab,Transform monsterSlot, MonsterUi newMonsterUi)
    {
        // TODO Remove old monster.

       Monster newSpawned = Instantiate(monsterPrefab, monsterSlot);

        UpdateTitle(newSpawned, newMonsterUi);
        UpdateHealth(newSpawned, newMonsterUi);

        return newSpawned;
    }

    private void ClearSlot(Transform slot)
    {
        for (int i = 0; i < slot.childCount; i++)
        {
            Transform child = slot.GetChild(i);
            Destroy(child.gameObject);
        }
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

