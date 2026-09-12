using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int maxHP = 100;
    private int currentHP;

    private void Start() { currentHP = maxHP; }

    public void TakeDamage(int damage)
    {
        currentHP -= damage;
        Debug.Log("Enemy HP: " + currentHP);

        if (currentHP <= 0) Die();
    }

    private void Die()
    {
        currentHP = 0;
        Debug.Log("Enemy defeated!");
        Destroy(gameObject);

        GameManager gameManager = FindObjectOfType<GameManager>();
        if (gameManager != null) gameManager.PlayerWon();
    }

    public int GetHP() => currentHP;
}
