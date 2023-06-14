using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EnemyCounter : MonoBehaviour
{
    public TextMeshProUGUI enemyCountText;
    public int enemyCount;

    void Start()
    {
        // Z�hlen Sie die anf�ngliche Anzahl der Gegner in der Szene
        enemyCount = GameObject.FindGameObjectsWithTag("Enemy").Length;
        UpdateEnemyCountText();
    }

    public void EnemyKilled()
    {
        // Reduzieren Sie die Anzahl der Gegner, wenn einer get�tet wird
        enemyCount--;
        UpdateEnemyCountText();
        Debug.Log("EnemyKilled called");
    }

    void UpdateEnemyCountText()
    {
        // Aktualisieren Sie die Anzeige auf dem Bildschirm
        enemyCountText.text = enemyCount + " Enemys remaining";
    }
}
