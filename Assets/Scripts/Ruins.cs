using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ruins : MonoBehaviour
{
    private bool changeColor = false;

    public void ChangeColor()
    {
        if (!changeColor)
        {
            // Change color logic here
            GetComponent<Renderer>().material.color = Color.blue;
            changeColor = true;

            // Check if all puzzle objects have changed color
            if (Puzzle2Manager.instance != null)
            {
                Puzzle2Manager.instance.CheckSolution();
            }

            if (WitchesMoatPuzzleManager.instance != null)
            {
                WitchesMoatPuzzleManager.instance.CheckSolution();
            }
            
            if (FlyingPuzzleRuins.instance != null)
            {
                FlyingPuzzleRuins.instance.CheckSolution();
            }
        }
    }
}
