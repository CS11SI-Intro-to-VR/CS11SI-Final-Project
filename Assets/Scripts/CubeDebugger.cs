using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CubeDebugger : MonoBehaviour
{

    public static CubeDebugger Instance;

    [SerializeField] private TextMeshProUGUI _textOnCube;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        Instance = this;
    }

    public void ChangeColor(Color c)
    {
        GetComponent<Renderer>().material.color = c;
    }

    public void ChangeText(string s)
    {
        _textOnCube.text = s;
    }

}
