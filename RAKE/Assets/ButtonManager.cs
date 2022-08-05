using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{

    public void Menu() 
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }

    public void Hub() 
    {
        StopAllCoroutines();
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Mushroom");
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        Destroy(player);
        foreach (GameObject enemy in enemies)
            GameObject.Destroy(enemy);
        Time.timeScale = 1;
        SceneManager.LoadScene(1);
    }

    public void Jungle() 
    {
        StopAllCoroutines();
        SceneManager.LoadScene(2);
    }

    public void Boss() 
    {
        StopAllCoroutines();
        SceneManager.LoadScene(3);
    }

    public void ResetGame()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.SetInt("Blood", 0);
        PlayerPrefs.SetInt("Price", 300);
        PlayerPrefs.SetInt("Hp", 500);
        PlayerPrefs.SetInt("Str", 30);
        PlayerPrefs.SetFloat("Bar", 3.8f);
        PlayerPrefs.SetInt("Door", 0);
        PlayerPrefs.SetInt("GameWon", 0);
        PlayerPrefs.SetInt("DNumb", 0);
    }

    public void ExitGame() 
    {
        Application.Quit();
    }
}
