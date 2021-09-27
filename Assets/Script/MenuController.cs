using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    [SerializeField] private GameObject gameController;
    [SerializeField] private GameObject character;
    public bool MainMenu = true;
    [SerializeField] private Image skins;
    private int numberOfSkins;
    [SerializeField] private Text price;
    private int skin_0;
    private int skin_1;
    private int skin_2;
    private int skin_3;
    private int skin_4;
    [SerializeField] private List<int> Skins;

    [Header("Button")]
    [SerializeField] private Button buy;
    [SerializeField] private Button equipped;
    [SerializeField] private GameObject star;
    [SerializeField] private GameObject shop;
    [SerializeField] private GameObject main_menu;
   




    void Start()
    {
        LoadSkins();
        LoadEquippedSkin();
        Skins.Add(skin_0 = 1);
        Skins.Add(skin_1);
        Skins.Add(skin_2);
        Skins.Add(skin_3);
        Skins.Add(skin_4);

        character.GetComponent<SpriteRenderer>().sprite = gameController.GetComponent<GameController>().skins[numberOfSkins];
        skins.GetComponent<Image>().sprite = gameController.GetComponent<GameController>().skins[numberOfSkins];
        SaveEquippedSkin();
        SaveSkins();
    }


    void Update()
    {

        if (character.tag == "Death" && MainMenu == true)
        {
            main_menu.gameObject.SetActive(true);
            MainMenu = false;
        }

        if (character.tag == "Player")
        {
            MainMenu = true;
        }


    }

    public void GameStart()
    {
        gameController.GetComponent<GameController>().StartGame();
        main_menu.gameObject.SetActive(false);
    }
    public void GameExit()
    {
        Application.Quit();
    }

    public void Shop()
    {
        LoadEquippedSkin();
        skins.GetComponent<Image>().sprite = gameController.GetComponent<GameController>().skins[numberOfSkins];
        main_menu.gameObject.SetActive(false);
        shop.gameObject.SetActive(true);
        buy.gameObject.SetActive(false);
        equipped.gameObject.SetActive(true);
        price.text = "Сollected";
        star.gameObject.SetActive(false);
    }

    public void Back()
    {
        shop.gameObject.SetActive(false);
        main_menu.gameObject.SetActive(true);

    }
    public void ButtonLeft()
    {
        if (numberOfSkins == 0)
        {
            numberOfSkins = 4;
        }
        else
        {
            numberOfSkins -= 1;
        }

        skins.GetComponent<Image>().sprite = gameController.GetComponent<GameController>().skins[numberOfSkins];

        for (int i = 0; i <= 4; i++)
        {
            if (i == numberOfSkins)
            {
                for (int j = 0; j <= Skins.Count; j++)
                {
                    if (i == j)
                    {
                        if (Skins[i] == 1)
                        {
                            price.text = "Сollected";
                            buy.gameObject.SetActive(false);
                            equipped.gameObject.SetActive(true);
                            star.gameObject.SetActive(false);
                        }
                        else
                        {
                            price.text = "    20";
                            equipped.gameObject.SetActive(false);
                            buy.gameObject.SetActive(true);
                            star.gameObject.SetActive(true);

                        }
                    }
                }
            }
        }
    }

    public void ButtonRight()
    {
        if (numberOfSkins == 4)
        {
            numberOfSkins = 0;
        }
        else
        {
            numberOfSkins += 1;
        }

        skins.GetComponent<Image>().sprite = gameController.GetComponent<GameController>().skins[numberOfSkins];

        for (int i = 0; i <= 4; i++)
        {
            if (i == numberOfSkins)
            {
                for (int j = 0; j <= Skins.Count; j++)
                {
                    if (i == j)
                    {
                        if (Skins[i] == 1)
                        {
                            price.text = "Сollected";
                            buy.gameObject.SetActive(false);
                            equipped.gameObject.SetActive(true);
                            star.gameObject.SetActive(false);
                        }
                        else
                        {
                            price.text = "    20";
                            equipped.gameObject.SetActive(false);
                            buy.gameObject.SetActive(true);
                            star.gameObject.SetActive(true);

                        }
                    }
                }
            }
        }


    }

    public void Equipped()
    {
        character.GetComponent<SpriteRenderer>().sprite = gameController.GetComponent<GameController>().skins[numberOfSkins];
        SaveEquippedSkin();
    }

        
    public void Buy()
    {
        if (gameController.GetComponent<GameController>().numberOfStars >= 20)
        {
            gameController.GetComponent<GameController>().Buy();
            for (int i = 0; i <= 4; i++)
            {
                if (i == numberOfSkins)
                {
                    for (int j = 0; j <= Skins.Count; j++)
                    {
                        if (i == j)
                        {
                            Skins[i] = 1;
                        }
                    }
                }
            }
            skin_1 = Skins[1];
            skin_2 = Skins[2];
            skin_3 = Skins[3];
            skin_4 = Skins[4];


            buy.gameObject.SetActive(false);
            equipped.gameObject.SetActive(true);
            price.text = "Сollected";
            star.gameObject.SetActive(false);

            SaveSkins();
        }
    }


    void SaveSkins()
    {
        PlayerPrefs.SetInt("Skins_1", skin_1);
        PlayerPrefs.SetInt("Skins_2", skin_2);
        PlayerPrefs.SetInt("Skins_3", skin_3);
        PlayerPrefs.SetInt("Skins_4", skin_4);
    }
    void LoadSkins()
    {
        if (PlayerPrefs.HasKey("Skins_1"))
        {
            skin_1 = PlayerPrefs.GetInt("Skins_1");
            skin_2 = PlayerPrefs.GetInt("Skins_2");
            skin_3 = PlayerPrefs.GetInt("Skins_3");
            skin_4 = PlayerPrefs.GetInt("Skins_4");
        }
    }

    void SaveEquippedSkin()
    {
        PlayerPrefs.SetInt("Skins_equipped", numberOfSkins);
    }

    void LoadEquippedSkin()
    {
        if (PlayerPrefs.HasKey("Skins_equipped"))
        {
            numberOfSkins = PlayerPrefs.GetInt("Skins_equipped");
        }
    }

}
