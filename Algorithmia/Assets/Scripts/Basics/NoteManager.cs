using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteManager : MonoBehaviour, IDataPersistence
{
    public List<Sprite> allBookPages;

    public Sprite lockedPageSprite;
    
    public int unlockedPageCount;

    public void LoadData(GameData data)
    {
        this.unlockedPageCount = data.unlockedPageCount;
    }

    public void SaveData(ref GameData data)
    {
    }
}
