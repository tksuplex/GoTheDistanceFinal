using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Unless denoted by a commented out link, TK wrote literally everything here

public class BGMScript : MonoBehaviour
{
    public static BGMScript bgmInstance;

    void Awake()
    {
        if (bgmInstance == null)
        {
            bgmInstance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(this.gameObject);
    }
}
