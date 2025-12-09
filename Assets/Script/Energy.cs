using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Energy : MonoBehaviour
{
 
    [Header("Energy Settings")]
    public float maxEnergy = 100f;        
    public float currentEnergy = 100f;     
    public float recoverRate = 5f;       
    public float sprintDrainTime = 5f;    

    [Header("UI")]
    public Image energyBar;                
    private bool isSprinting = false;      
    private float drainSpeed;           

    void Start()
    {
        drainSpeed = maxEnergy / sprintDrainTime;
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.LeftShift) && currentEnergy > 0)
        {
            isSprinting = true;
        }
        else
        {
            isSprinting = false;
        }

        HandleEnergy();
        UpdateUI();
    }

    void HandleEnergy()
    {
        if (isSprinting)
        {
            currentEnergy -= drainSpeed * Time.deltaTime;
        }
        else
        {
            currentEnergy += recoverRate * Time.deltaTime;
        }

        currentEnergy = Mathf.Clamp(currentEnergy, 0, maxEnergy);
    }

    void UpdateUI()
    {
        if (energyBar != null)
            energyBar.fillAmount = currentEnergy / maxEnergy;
    }

    public bool CanSprint()
    {
        return currentEnergy > 0;
    }
}
