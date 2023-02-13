using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpadeMasterHpBar : MonoBehaviour
{
    public GameObject hpBackground;
    public GameObject hpValue;
    GameObject spadeMaster = null;

    void Start()
    {
        hpBackground.SetActive(false);
    }

    void FixedUpdate()
    {
        if (spadeMaster != null)
        {
            if(spadeMaster.transform.position.y < 370f)
            {
                hpBackground.SetActive(true);
                hpValue.GetComponent<Image>().fillAmount = spadeMaster.GetComponent<SpadeMaster>().CurHp / spadeMaster.GetComponent<SpadeMaster>().MaxHp;
            }
        }
        else
        {
            hpBackground.SetActive(false);
            spadeMaster = GameObject.FindGameObjectWithTag("Enemy");
        }
    }
}