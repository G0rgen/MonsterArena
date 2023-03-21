using System;
using UnityEngine;

public class Monster : MonoBehaviour
{
    [SerializeField] private string title = "Monster";
    [SerializeField] private float maxHealth = 10f;

    [SerializeField] private string attackName = "ATTACKNNAME";
    [SerializeField] private float attackStrengh = 2f;

    private float currentHealth;

    private void Awake()
    {
        currentHealth = maxHealth;
    }

    public void Attack(Monster target)
    {
        float damage = attackStrengh;
        // TODO Add multiplier 0.5x or 2.0x depending on elements of attacker and defender
        target.UpdateHealth(-damage);
    }

    public bool HasFainted()
    {
        return currentHealth == 0;
    }

    private void UpdateHealth(float change)
    {
        currentHealth += change;
        currentHealth = Mathf.Clamp(currentHealth, min: 0, maxHealth);
    }

    public string GetTitle()
    {
        return title;
    }

    public float GetMaxHealth()
    {
        return maxHealth;
    }

    public float GetCurrentHealth()
    {
        return currentHealth;
    }
}