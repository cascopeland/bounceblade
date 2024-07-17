using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowText : MonoBehaviour
{
    public GameObject text;
    // Start is called before the first frame update
    void Start()
    {
        text.SetActive(false);
    }

    private void OnTriggerEnter(Collider player)
    {
        if (player.gameObject.tag == "Player")
        {
            text.SetActive(true);
            Invoke("DestroyText", 7f);
        }
    }

    private void DestroyText()
    {
        Destroy(text);
        Destroy(gameObject);
    }
}
