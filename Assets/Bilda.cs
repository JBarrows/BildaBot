using MoreMountains.CorgiEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bilda : MonoBehaviour
{
    public enum UpgradeStage
    {
        Hand = 0,
        Legs = 1,
        Jetpack = 2,
        Ribbon = 3,
    }

    public UpgradeStage stage = 0;
    public GameObject[] models;

    public GameObject CurrentModel => models[(int)stage];

    private void Start()
    {
        SelectModel();
    }

    private void SelectModel()
    {
        for (int i = 0; i < models.Length; i++) {
            models[i].SetActive(i == (int)stage);
        }

        GetComponent<Character>().CharacterModel = CurrentModel;

        GetComponent<CharacterStairs>().AbilityPermitted =  (stage >= UpgradeStage.Legs);
    }


    public void Upgrade()
    {
        if (stage == UpgradeStage.Ribbon)
            return;

        stage = (UpgradeStage)((int)stage + 1);
        SelectModel();
    }
}
