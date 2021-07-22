using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverDisplay : MonoBehaviour
{
    public Text lostText;

    public void Setup()
    {
        if (!gameObject.activeSelf)
        {
            gameObject.SetActive(true);
        }
    }

}
