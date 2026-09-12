using UnityEngine;

public class CombatManager : MonoBehaviour
{
    public int knifeDamage = 25;
    public int swordDamage = 40;
    public int poisonDamage = 20;

    public void Attack(GameObject target, string weapon)
    {
        if (target == null) return;

        Enemy enemy = target.GetComponent<Enemy>();
        if (enemy == null)
        {
            Debug.Log("Target is not an enemy.");
            return;
        }

        enemy.TakeDamage(GetWeaponDamage(weapon));
    }

    private int GetWeaponDamage(string weapon)
    {
        switch (weapon.ToLower())
        {
            case "knife": return knifeDamage;
            case "sword": return swordDamage;
            case "poison": return poisonDamage;
            default: return 10;
        }
    }
}
