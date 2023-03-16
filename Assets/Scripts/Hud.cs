using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hud : MonoBehaviour
{

    public Sprite[] sprites;
    public Image lifeBar;

    public static Hud instance;

    // Start is called before the first frame update
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(instance);
        }
    }

    public void RefreshLifeBar(int playerHealth)
    {
        lifeBar.sprite = sprites[playerHealth];
    }
}
