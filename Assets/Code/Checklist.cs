using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using UnityEngine.UI;

public class Checklist : MonoBehaviour
{
    public Image[] checkboxes;
    public Sprite check;

    void Start()
    {
        gameObject.SetActive(false);
    }

    // Checks off the item based on the given index
    // 0: toolbox
    // 1: fuel
    // 2: engine
    // 3: propeller
    // 4: gear
    public void checkItem(int num) {
        checkboxes[num].sprite = check;
    }
}
