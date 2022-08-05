using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{

    public void Menu() 
    {
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

}
