using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BattleSystem : MonoBehaviour {
    public string playerName;
    public int playerHP;
    public int playerMP;
    public int enemyHP;
    public int enemyMP;
    public int attackPower;
    public int enemyAttackPower;

    public int ToolPotionEffectToMyHP;
    public int ToolPotionNum;
    public int ToolCarrotEffectToMyMP;
    public int ToolCarrotNum;
    public int SpellRecoverEffectToMyHP;
    public int SpellRecoverCost;
    public int SpellAttackEffectToEnemyHP;
    public int SpellAttackCost;
    public int enemyToolPotionCount;
    public int enemyToolCarrotCount;

    public GameObject toolPanelObject;
    public GameObject spellPanelObject;
    public GameObject playerHPObject;
    public GameObject playerMPObject;
    public GameObject sentenceObject;
    public GameObject playerNameObject;
    public GameObject toolPotionNumObject;
    public GameObject toolCarrotNumObject;
    public GameObject enemyHPObject;
    public GameObject enemyMPObject;


    public string escapeScene;

    bool waitingInput;

    // Use this for initialization
    void Start () {
        playerNameObject.GetComponent<Text>().text = playerName;
        playerHPObject.GetComponent<Text>().text = playerHP.ToString();
        playerMPObject.GetComponent<Text>().text = playerMP.ToString();

        toolPotionNumObject.GetComponent<Text>().text = ToolPotionNum.ToString();
        toolCarrotNumObject.GetComponent<Text>().text = ToolCarrotNum.ToString();

        enemyHPObject.GetComponent<Text>().text = enemyHP.ToString();
        enemyMPObject.GetComponent<Text>().text = enemyMP.ToString();

        waitingInput = true;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    IEnumerator showPlayerTurn()
    {
        yield return new WaitForSeconds(2.5f);
        sentenceObject.GetComponent<Text>().text = playerName + "のターン";
    }

    public void toolPotionPushed()
    {
        if (waitingInput == false)
        {
            return;
        }

        if (ToolPotionNum == 0)
        {
            sentenceObject.GetComponent<Text>().text = "もってないよ";
        }
        else
        {
            sentenceObject.GetComponent<Text>().text = playerName + "はポーションを使った" + "HPが" + ToolPotionEffectToMyHP.ToString() + "あがった";
            playerHP += ToolPotionEffectToMyHP;
            playerHPObject.GetComponent<Text>().text = playerHP.ToString();

            ToolPotionNum--;
            toolPotionNumObject.GetComponent<Text>().text = ToolPotionNum.ToString();

        }

        toolPanelObject.SetActive(false);

        StartCoroutine("showPlayerTurn");
    }
    public void toolCarrotPushed()
    {
        if (waitingInput == false)
        {
            return;
        }

        if (ToolCarrotNum == 0)
        {
            sentenceObject.GetComponent<Text>().text = "もってないよ";
        }
        else
        {
            sentenceObject.GetComponent<Text>().text = playerName + "はにんじんを使った" + "MPが" + ToolCarrotEffectToMyMP.ToString() + "あがった";
            playerMP += ToolCarrotEffectToMyMP;
            playerMPObject.GetComponent<Text>().text = playerMP.ToString();

            ToolCarrotNum--;
            toolCarrotNumObject.GetComponent<Text>().text = ToolCarrotNum.ToString();

        }
        toolPanelObject.SetActive(false);

        StartCoroutine("showPlayerTurn");
    }

    public void spellRecoverPushed()
    {
        if (waitingInput == false)
        {
            return;
        }

        if (playerMP - SpellRecoverCost < 0 )
        {
            sentenceObject.GetComponent<Text>().text = "MP足りないよ";
        }
        else
        {
            sentenceObject.GetComponent<Text>().text = playerName + "はかいふくのじゅもんを使った" + "HPが" + SpellRecoverEffectToMyHP.ToString() + "あがった";
            playerHP += SpellRecoverEffectToMyHP;
            playerHPObject.GetComponent<Text>().text = playerHP.ToString();

            playerMP -= SpellRecoverCost ;
            playerMPObject.GetComponent<Text>().text = playerMP.ToString();

        }

        spellPanelObject.SetActive(false);
    }
    public void spellAttackPushed()
    {
        if (waitingInput == false)
        {
            return;
        }

        if (playerMP - SpellAttackCost < 0)
        {
            sentenceObject.GetComponent<Text>().text = "MP足りないよ";
        }
        else
        {
            sentenceObject.GetComponent<Text>().text = playerName + "はこうげきのじゅもんを使った" + "てきのHP" + SpellAttackEffectToEnemyHP.ToString() + "さがった";
            enemyHP -= SpellAttackEffectToEnemyHP;
            enemyHPObject.GetComponent<Text>().text = enemyHP.ToString();

            playerMP -= SpellAttackCost;
            playerMPObject.GetComponent<Text>().text = playerMP.ToString();

            waitingInput = false;
            StartCoroutine("attackCheck");

        }

        spellPanelObject.SetActive(false);
    }

    public void escapePushded()
    {
        SceneManager.LoadScene(escapeScene);
    }

    IEnumerator playerLose()
    {
        Debug.Log("playerLose called");

        sentenceObject.GetComponent<Text>().text = playerName + "のまけ";

        yield return new WaitForSeconds(2.5f);

        SceneManager.LoadScene(escapeScene);
    }

    void enemyAttack()
    {
        playerHP -= enemyAttackPower;
        if (playerHP <= 0)
        {
            Debug.Log("calling playerLose");
            StartCoroutine("playerLose");
        }
        else
        {
            sentenceObject.GetComponent<Text>().text = playerName + "のHP" + playerHP.ToString() + "になった";
            playerHPObject.GetComponent<Text>().text = playerHP.ToString();
        }
    }

    void enemyTool()
    {
        Debug.Log("enemyTool");
        int acton = Random.Range(0, 2);
        Debug.Log(acton);
        switch ( acton)
        {
            case 0:
                if (enemyToolPotionCount == 0)
                {
                    enemyAttack();
                }
                else
                {
                    sentenceObject.GetComponent<Text>().text = "てきはポーションを使った" + "HPが" + ToolPotionEffectToMyHP.ToString() + "あがった";
                    enemyHP += ToolPotionEffectToMyHP;
                    enemyHPObject.GetComponent<Text>().text = enemyHP.ToString();
                    enemyToolPotionCount--;
                }
                break;
            case 1:
                if (enemyToolCarrotCount == 0)
                {
                    enemyAttack();
                }
                else
                {
                    sentenceObject.GetComponent<Text>().text = "てきはにんじんを使った" + "MPが" + ToolCarrotEffectToMyMP.ToString() + "あがった";
                    enemyMP += ToolCarrotEffectToMyMP;
                    enemyMPObject.GetComponent<Text>().text = enemyMP.ToString();
                    playerMPObject.GetComponent<Text>().text = playerMP.ToString();

                    enemyToolCarrotCount--;
                }
                break;
        }
    }

void enemySpell()
    {
        Debug.Log("enemySpell");
        if ( enemyMP <= 0)
        {
            enemyAttack();
        }
        else
        {
            int acton = Random.Range(0, 2);
            switch ( acton)
            {
                case 0:
                    sentenceObject.GetComponent<Text>().text = "てきはかいふくのじゅもんを使った" + "HPが" + SpellRecoverEffectToMyHP.ToString() + "あがった";
                    enemyHP += SpellRecoverEffectToMyHP;
                    enemyHPObject.GetComponent<Text>().text = enemyHP.ToString();

                    break;
                case 1:
                    sentenceObject.GetComponent<Text>().text = "てきはこうげきのじゅもんを使った" + playerName + "のHP" + SpellAttackEffectToEnemyHP.ToString() + "さがった";
                    playerHP -= SpellAttackEffectToEnemyHP;
                    playerHPObject.GetComponent<Text>().text = playerHP.ToString();

                    if ( playerHP <= 0)
                    {
                        StartCoroutine("playerLose");
                    }
                    break;
            }
        }
    }

    IEnumerator enemyTurn()
    {
        sentenceObject.GetComponent<Text>().text =  "てきのこうげき" ;
        yield return new WaitForSeconds(2.5f);

        int action = Random.Range(0, 3);
        switch ( action)
        {
            case 0:
                enemyAttack();
                break;
            case 1:
                enemyTool();
                break;
            case 2:
                enemySpell();
                break;
        }
        if ( playerHP > 0)
        {
            yield return new WaitForSeconds(2.5f);
            sentenceObject.GetComponent<Text>().text = playerName + "のターン";

        }
    }

    IEnumerator attackCheck()
    {
        yield return new WaitForSeconds(2.5f);

        if (enemyHP < 0)
        {
            sentenceObject.GetComponent<Text>().text = playerName + "の勝利！";

            yield return new WaitForSeconds(2.5f);

            SceneManager.LoadScene(escapeScene);
        }
        else
        {
            StartCoroutine("enemyTurn");
        }

        waitingInput = true;
    }

    public void attackPushed()
    {
        if ( waitingInput == false)
        {
            return;
        }
        enemyHP -= attackPower;
        enemyHPObject.GetComponent<Text>().text = enemyHP.ToString();
        sentenceObject.GetComponent<Text>().text = playerName + "のこうげき" + "てきのHP" + attackPower.ToString() + "さがった";

        waitingInput = false;
        StartCoroutine("attackCheck");
    }
}
