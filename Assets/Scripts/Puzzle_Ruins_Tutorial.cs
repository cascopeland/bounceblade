using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puzzle_Ruins_Tutorial : MonoBehaviour
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
            RuinsPuzzleManager.instance.CheckSolution();
        }
    }
}
