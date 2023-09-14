using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class oxygenTankManager : MonoBehaviour
{
    [SerializeField] Slider oxygenBar;
    [SerializeField] TextMeshProUGUI noOxygenText;
    [SerializeField] float oxygenMaxAmount;
    [SerializeField] float oxygenTankIncreaseAmount;
    bool isOxygen = true;
    bool hasSuffocated = false;

    void Start()
    {
        oxygenBar.maxValue = oxygenMaxAmount;
        oxygenBar.value = oxygenMaxAmount;
        oxygenBar.minValue = 0f;

    }


    void Update()
    {
        OxygenDecrease();
        Suffocate();

    }
    

    public void OxygenTankCollect()
    {
        oxygenBar.value += oxygenTankIncreaseAmount;
    }
    void OxygenDecrease()
    {
        if (oxygenBar.value != 0f)
        {
            oxygenBar.value -= Time.deltaTime;
        }
        else if (oxygenBar.value == 0f)
        {
            isOxygen = false;
        }

    }

    void Suffocate()
    {
        if (!isOxygen & !hasSuffocated)
        {
            FindObjectOfType<playerMovement>().Die();
            hasSuffocated = true;
            noOxygenText.gameObject.SetActive(true);
        }
    }


}
