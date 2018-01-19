using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Chickoggo {

	public enum FluffType { Chicken, Duck };

	/// <summary>
	/// Eggs that the Doggo can hatch.
	/// </summary>
	public class Egg : Actor {

		public FluffType type;

		protected bool bWarming;
		protected float progress = 0f;
		protected float warmingSpeed = 20f;
		protected float coolingSpeed = 5f;

		public float Progress { get { return progress; } }

		private void Update() {
			if (bWarming) {
				progress += warmingSpeed * Time.deltaTime;
			} else {
				progress -= coolingSpeed * Time.deltaTime;
			}

			if (progress < 0) {
				progress = 0;
			}

			if (progress >= 100) {
				progress = 100;

				Hatch();
			}
		}

		public void StartWarming() {
			bWarming = true;
		}

		public void StopWarming() {
			bWarming = false;
		}

		public void Hatch() {
			ObjectPoolHandler.instance.Unspawn(gameObject);

			GameObject newFluff = ObjectPoolHandler.instance.Spawn(GameManager.instance.chick);
			newFluff.transform.position = transform.position;
		}
	}
}