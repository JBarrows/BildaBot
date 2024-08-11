using MoreMountains.CorgiEngine;
using MoreMountains.InventoryEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrokenMachine : ButtonActivated
{
	public BaseItem PartNeeded;

    private const int PARTS_REQUIRED = 3;

    public override void TriggerButtonAction(GameObject instigator)
	{
		if (!CheckNumberOfUses()) {
			return;
		}

		Collider2D coll = instigator.GetComponent<Collider2D>();
		if (coll != null) {
			base.TriggerButtonAction(instigator);
			
			// Retrieve parts from inventory
			var inv = instigator.GetComponent<CharacterInventory>().MainInventory;
			
			// Check if we have enough parts (Player inventory)
			int partsCount = inv.GetQuantity(PartNeeded.ItemID);
			if (partsCount >= PARTS_REQUIRED) {
				// TODO: Fix this machine
				Debug.Log($"Fix!");
				GetComponent<Animator>().SetTrigger("fixed");
			} else {
				// TODO: Say something about not having enough parts
				Debug.Log($"Not enough pieces! ({partsCount}/{PARTS_REQUIRED})");
			}
		}
	}
}
