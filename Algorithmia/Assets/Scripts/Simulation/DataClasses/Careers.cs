using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CareerData
{
    public int id;
    public string jobName;
    public string jobIcon;
    public string field;
    public string salary;
    public string employer;
    public string[] requirements;
}

[System.Serializable]
public class CareerList
{
    public List<CareerData> careers;
}
