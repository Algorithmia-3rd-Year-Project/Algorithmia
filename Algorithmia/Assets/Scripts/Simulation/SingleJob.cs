using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class SingleJob : MonoBehaviour
{

    public TMP_Text jobNameText;

    public TMP_Text fieldNameText;

    public TMP_Text salaryText;

    public Image jobIconImage;

    public string jobName;
    public string fieldName;
    public string salaryAmount;
    public string companyName;
    public string[] requirements;
    public bool occupied;
}
