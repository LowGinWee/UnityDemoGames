using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UserInterface : MonoBehaviour
{
    public GameObject textBox;
    public GameObject key;
    public Player player;
    public Text keyNum;
    public Text level;
    public GameController gameController;

    public GameObject HpTemplate;

    private GameObject[] hpBar;
    public Transform hpBarContainer;
    public Sprite[] spriteHp;

    // Start is called before the first frame update
    void Start()
    {
    }

    public void CreateHpBar()
    {
        hpBar = new GameObject[5];
        foreach (Transform i in hpBarContainer)
        {
            Destroy(i.gameObject);
        }

        for (int i = 0; i < 5; i++)
        {
            hpBar[i] = Instantiate(HpTemplate);
            hpBar[i].transform.SetParent(hpBarContainer, false);
            if (i > 0) hpBar[i].transform.position = hpBar[i - 1].transform.position + new Vector3(0.5f, 0, 0);
            hpBar[i].SetActive(true);
        }
    }

    public void KeyPickedEnableBox()
    {
        textBox.SetActive(true);
        textBox.GetComponentInChildren<Text>().text = "You have found the key!\nPress any key to continue...";
        key.SetActive(true);
        Debug.Log("im here");
        StartCoroutine(DisableBox(false));
    }

    // Update is called once per frame
    void Update()
    {
        keyNum.text = "0" + player.keyNum;
        level.text = "Level: " + gameController.level;

    }

    public IEnumerator DisableBox(bool isExited)
    {
        yield return new WaitForSeconds(0.1f);
        while (!Input.anyKeyDown)
        {
            yield return null;
        }
        textBox.SetActive(false);
        key.SetActive(false);

        if (isExited)
        {
            GameController.state = GameController.STATE.WIN;
        }
    }

    public void ExitBox(bool hasKeys)
    {
        textBox.SetActive(true);
        if (hasKeys)
        {
            textBox.GetComponentInChildren<Text>().text = "The Door has opened\n Press any key to continue...";
        } else
        {
            textBox.GetComponentInChildren<Text>().text = "The Door is locked...\nFind the keys!\n Press any key to continue...";
        }
        Debug.Log("im here");
        StartCoroutine(DisableBox(hasKeys));
    }

    public void MinusHealth()
    {
        for (int i = 4; i >= 0; i--)
        {
            if (hpBar[i].GetComponent<Image>().sprite == spriteHp[1])
            {
                hpBar[i].GetComponent<Image>().sprite = spriteHp[2];
                break;
            }
            else if (hpBar[i].GetComponent<Image>().sprite == spriteHp[0])
            {
                hpBar[i].GetComponent<Image>().sprite = spriteHp[1];
                break;
            }
        }
    }

    public void PlusHealth()
    {
        for (int i = 0; i < 5; i++)
        {
            if (hpBar[i].GetComponent<Image>().sprite == spriteHp[1])
            {
                hpBar[i].GetComponent<Image>().sprite = spriteHp[0];
                if (i < 4) hpBar[i + 1].GetComponent<Image>().sprite = spriteHp[1];
                break;
            }
            else if (hpBar[i].GetComponent<Image>().sprite == spriteHp[2])
            {
                hpBar[i].GetComponent<Image>().sprite = spriteHp[0];
                break;
            }
        }
    }

}
