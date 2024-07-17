using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public int skillPoints = 5;
    public List<int> unlockedList = new List<int>();
    public int slowBallCollisons = 3;
    public int swordDamage = 1;

    public float savedVolume;

    //player health
    public int playerHealth = 10;

    //ball damage
    public int ballDamage = 10;

    //booleans for slowball collision skills
    private bool slowBallCollision1Added = false;
    private bool slowBallCollision2Added = false;

    //holds booleans for swordDamage skill
    private bool swordDamage1Added = false;
    private bool swordDamage2Added = false;

    //holds booleans for ballDamage skill
    private bool ballDamage1Added = false;
    private bool ballDamage2Added = false;



    private void Awake()
    {
        unlockedList.Add(0);
        // start of new code
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        // end of new code

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void slowBallCollisionSkillCheck()
    {
        Debug.Log("Slow Ball Collision Check");
        if (unlockedList.Contains(5) && !slowBallCollision1Added)
        {
            slowBallCollisons++;
            slowBallCollision1Added = true;
            Debug.Log("Slow Ball Collision 1 Done");
        }

        if (unlockedList.Contains(6) && !slowBallCollision2Added)
        {
            slowBallCollisons++;
            slowBallCollision1Added = true;
            Debug.Log("Slow Ball Collision 2 Done");
        }
    }

    public void swordDamageSkillCheck()
    {
        Debug.Log("Sword Damage Collision Check");
        if (unlockedList.Contains(7) && !swordDamage1Added)
        {
            swordDamage++;
            swordDamage1Added = true;
            Debug.Log("Sword Damage 1 Upgrade!");
        }

        if (unlockedList.Contains(8) && !swordDamage2Added)
        {
            swordDamage++;
            swordDamage2Added = true;
            Debug.Log("Sword Damage 2 Upgrade!");
        }
    }
    public void ballDamageSkillCheck()
    {
        Debug.Log("Sword Damage Collision Check");
        if (unlockedList.Contains(3) && !ballDamage1Added)
        {
            ballDamage = 2 * ballDamage;
            ballDamage1Added = true;
            Debug.Log("Ball Damage 1 Upgrade!");
        }

        if (unlockedList.Contains(4) && !ballDamage2Added)
        {
            ballDamage = 2 * ballDamage;
            ballDamage2Added = true;
            Debug.Log("Ball Damage 2 Upgrade!");
        }
    }

    //sets playerHealth
    public void setPlayerHealth(int newhealth)
    {
        playerHealth = newhealth;
    }
}
