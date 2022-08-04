using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerUps : MonoBehaviour
{
    //0 == strenght, 1 == health
    [SerializeField]
    private Sprite strength, health;

    [SerializeField]
    private Text price, buyFor, balance;

    [SerializeField]
    private AudioSource audioSource, buySound, noMoney;

    [SerializeField]
    private Image image;

    [SerializeField]
    private GameObject buyButton, HealthBar;
    
    private GameObject player;

    private int currentIndex = 0;

    private SpriteRenderer spriteRenderer;

    bool isDone = false;
    Vector2 hpSize;

    private void Awake()
    {
        PlayerPrefs.SetInt("Blood", 1000000);
        PlayerPrefs.SetInt("Price", 300);
        PlayerPrefs.SetInt("Hp", 500);
        PlayerPrefs.SetInt("Str", 20);
        PlayerPrefs.SetFloat("Bar", 3.8f);
        player = GameObject.FindGameObjectWithTag("Player");
        spriteRenderer = GetComponent<SpriteRenderer>();

    }

    private void Update()
    {
        hpSize = HealthBar.transform.localScale;
        balance.text = PlayerPrefs.GetInt("Blood").ToString();
        currentIndex = PlayerPrefs.GetInt("PowerUp");
        switch (currentIndex)
        {
            case 0:
                spriteRenderer.sprite = strength;
                break;
            case 1:
                spriteRenderer.sprite = health;
                break;
        }
        if (Mathf.Abs(transform.position.x - player.transform.position.x) < 2)
        {
            price.text = PlayerPrefs.GetInt("Price").ToString();
            buyButton.SetActive(true);
            if (!isDone) 
            {
                StartCoroutine(BuyFor());
            }
            isDone = true;

            if (Input.GetKeyDown(KeyCode.E)) 
            {
                if (PlayerPrefs.GetInt("Blood") >= PlayerPrefs.GetInt("Price"))
                {
                    buySound.Play();
                    PlayerPrefs.SetInt("Blood", PlayerPrefs.GetInt("Blood") - PlayerPrefs.GetInt("Price"));
                    if (PlayerPrefs.GetInt("PowerUp") == 0)
                    {
                        PlayerPrefs.SetInt("Str", PlayerPrefs.GetInt("Str") + 5);
                        PlayerPrefs.SetInt("PowerUp", 1);
                    }
                    else
                    {
                        image.fillAmount = 1;
                        HealthBar.transform.localScale = new Vector3(hpSize.x + 0.6f, hpSize.y);
                        PlayerPrefs.SetFloat("Bar", HealthBar.transform.localScale.x);
                        PlayerPrefs.SetInt("Hp", PlayerPrefs.GetInt("Hp") + 50);
                        PlayerPrefs.SetInt("PowerUp", 0);
                    }

                    PlayerPrefs.SetInt("Price", PlayerPrefs.GetInt("Price") + PlayerPrefs.GetInt("Price") - 100);

                }
                else 
                {
                    noMoney.Play();
                }
            }
        }
        else
        {
            isDone = false;
            buyButton.SetActive(false);
            buyFor.text = " ";
            price.text = " ";
        }


    }

    IEnumerator BuyFor() 
    {
        string buy = "Buy For";
        for (int i = 0; i < buy.Length; i++) 
        {
            if (buyButton.activeInHierarchy)
            {
                audioSource.pitch = Random.Range(1f, 1.2f);
                audioSource.Play();
                buyFor.text += buy[i];
                yield return new WaitForSeconds(0.05f);
            }
            else 
            {
                break;
            }
        }
    }
}
