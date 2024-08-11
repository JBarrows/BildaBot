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
        var scale = CurrentModel.transform.localScale;
        scale.x = GetComponent<Character>().IsFacingRight ? 1f : -1f;
        CurrentModel.transform.localScale = scale;
        

        GetComponent<CharacterStairs>().AbilityPermitted =  (stage >= UpgradeStage.Legs);
        GetComponent<CharacterJump>().AbilityPermitted = (stage >= UpgradeStage.Jetpack);

        var moveAbility = GetComponent<CharacterHorizontalMovement>();
        moveAbility.MovementSpeedMultiplier = stage switch {
            UpgradeStage.Hand => 1.0f,
            UpgradeStage.Legs => 1.2f,
            UpgradeStage.Jetpack => 1.2f,
            UpgradeStage.Ribbon => 1.6f,
            _ => 1.2f
        };
    }


    public void Upgrade()
    {
        if (stage == UpgradeStage.Ribbon)
            return;

        stage = (UpgradeStage)((int)stage + 1);
        SelectModel();
    }
    float jumpTime = float.MaxValue;
    public void StartJump()
    {
        CurrentModel.GetComponent<Animator>().SetTrigger("Jumping");
        jumpTime = Time.time;
    }

    public void EndJump()
    {
        if (Time.time - jumpTime < 0.5f || CurrentModel.GetComponent<Animator>().GetBool("Jumping"))
            return;

        CurrentModel.GetComponent<Animator>().SetTrigger("HitTheGround");
    }
}
