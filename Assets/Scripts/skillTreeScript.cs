using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class skillTreeScript : MonoBehaviour
{
    public enum skills
    {
        Base = 0,
        Ball,
        SlowBall,
        BallDamage1,
        BallDamage2,
        SlowBallCollision1,
        SlowBallCollision2,
        SwordDamage1,
        SwordDamage2,
        LastOfList
    }

    //USING PLAYERPREFS FOR SKILLS

    //temporary variable before getting skillPoint variable to player UPDATE THIS VALUE TO MATCH PICKUP

    //list to add which is public to access from player class
    private static GameManager gameManager;

    //color to update button to when unlocked
    private static Color unlockedColor = new Color(1, 1, 1, 1);


    //buttons to change color
    public GameObject unlockButtons;
    private static void unlockSkill(int skillUnlocked, int skillPointsRequirement, int skillUnlockedPrevious, GameObject unlockButton)
    {
        //check if meets skillPoints requirement to add skill
        if (gameManager.skillPoints >= skillPointsRequirement) {
            //checks if the list has the prerequisite skill
            if (gameManager.unlockedList.Contains(skillUnlockedPrevious))
            {
                //checks if the list has has the skill already
                if (!(gameManager.unlockedList.Contains(skillUnlocked)))
                {
                    //CHECK OUT THIS PART TO MAKE SURE IT MAKES SENSE
                    //button color to show unlocked skill

                    unlockButton.GetComponent<Image>().color = unlockedColor;
                    gameManager.unlockedList.Add(skillUnlocked);
                    gameManager.skillPoints = gameManager.skillPoints - skillPointsRequirement;
                    Debug.Log("Added skill " + skillUnlocked);
                } else
                {
                    Debug.Log("Skill number " + skillUnlocked + " has already been obtained!");
                }
            } else
            {
                Debug.Log(gameManager.unlockedList.Count);
                foreach (int skill in gameManager.unlockedList)
                {
                    Debug.Log(skill);
                }
                Debug.Log(gameManager.unlockedList.Contains(skillUnlockedPrevious));
                Debug.Log("Prerequisite skill not unlocked! Need skill " + skillUnlockedPrevious);
            }
        } else 
        {
            Debug.Log("Skill cannot be added due to insufficent skill points! Skill points: " + gameManager.skillPoints + " out of " + skillPointsRequirement);
        }
    }

    public void BallSkill()
    {
        GameObject BallUnlockButton = GameObject.Find("BallUnlockButton");
        unlockSkill((int) skills.Ball, 1, (int) skills.Base, BallUnlockButton);
    }

    public void SlowBallSkill()
    {
        GameObject SlowBallUnlockButton = GameObject.Find("SlowBallUnlockButton");
        unlockSkill((int)skills.SlowBall, 1, (int)skills.Ball, SlowBallUnlockButton);
    }

    public void BallDamage1Skill()
    {
        GameObject BallDamage1Button = GameObject.Find("BallDamage1Button");
        unlockSkill((int)skills.BallDamage1, 1, (int)skills.SlowBall, BallDamage1Button);
    }


    public void BallDamage2Skill()
    {
        GameObject BallDamage2Button = GameObject.Find("BallDamage2Button");
        unlockSkill((int)skills.BallDamage2, 1, (int)skills.BallDamage1, BallDamage2Button);
    }

    public void SlowBallCollision1Skill()
    {
        GameObject SlowBallCollision1Button = GameObject.Find("SlowBallCollision1Button");
        unlockSkill((int)skills.SlowBallCollision1, 1, (int)skills.SlowBall, SlowBallCollision1Button);
    }

    public void SlowBallCollision2Skill()
    {
        GameObject SlowBallCollision2Button = GameObject.Find("SlowBallCollision2Button");
        unlockSkill((int)skills.SlowBallCollision2, 1, (int)skills.SlowBallCollision1, SlowBallCollision2Button);
    }

    public void SwordDamage1Skill()
    {
        GameObject SwordDamage1Button = GameObject.Find("SwordDamage1Button");
        unlockSkill((int)skills.SwordDamage1, 1, (int)skills.SlowBall, SwordDamage1Button);
    }

    public void SwordDamage2Skill()
    {
        GameObject SwordDamage2Button = GameObject.Find("SwordDamage2Button");
        unlockSkill((int)skills.SwordDamage2, 1, (int)skills.SwordDamage1, SwordDamage2Button);
    }

    //check that the skill List is set up right   

    public void Start()
    {
        gameManager = GameManager.Instance;
        skillCheckOnStart();
    }

    //checks if skill is unlocked
    public static bool isSkillUnlocked(skills skill)
    {
        return GameManager.Instance.unlockedList.Contains((int) skill);
    }

    //loops through and updates buttonColor on start if skill is unlocked
    void skillCheckOnStart()
    {
        Image[] unlockImages = unlockButtons.GetComponentsInChildren<Image>();

        for (int i = 1; i < (int) skills.LastOfList - 1; i++)
        {
            if (gameManager.unlockedList.Contains(i))
            {
                unlockImages[i - 1].color = unlockedColor;
            }
        }
    }
}
