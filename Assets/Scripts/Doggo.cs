using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Chickoggo {

	/// <summary>
	/// The main player character
	/// </summary>
	public class Doggo : Pawn {

		protected Quaternion desiredFacing;

		public GameObject body;

		protected bool bIsSitting;
		protected float timeToCancelSitting = 0.25f;
		protected float sittingTimer = 0f;

		protected List<Egg> eggsWarming = new List<Egg>(10);

		protected SphereCollider bodyCollider;

		protected override void SetDefaultValues() {
			base.SetDefaultValues();

			speed = 4.5f;
		}

		protected override void AwakeInit() {
			base.AwakeInit();

			bodyCollider = GetComponentInChildren<SphereCollider>();
		}

		private void Update() {
			if (bIsSitting) {

				if (sittingTimer > 0) {
					sittingTimer -= Time.deltaTime;

					if (sittingTimer < 0) {
						sittingTimer = 0;
					}
				}
			}
		}

		public override void ManualMovement(Vector3 newInput) {
			if (bIsSitting) {
				if (sittingTimer == 0 && newInput.sqrMagnitude > 0.0225f) {
					SitUp();
				}
				return;
			}

			base.ManualMovement(newInput);
		}

		public void SitDown() {
			if (bIsSitting) {
				return;
			}

			bIsSitting = true;
			sittingTimer = timeToCancelSitting;
			body.transform.Translate(Vector3.down * 0.5f);

			Collider[] colls = Physics.OverlapSphere(body.transform.position, bodyCollider.radius, 1 << 12, QueryTriggerInteraction.Collide);

			for (int i = 0; i < colls.Length; i++) {
				Egg egg = colls[i].gameObject.GetComponentInParent<Egg>();

				if (egg != null) {
					eggsWarming.Add(egg);
					egg.StartWarming();
				}
			}
		}

		public void SitUp() {
			if (!bIsSitting || sittingTimer > 0) {
				return;
			}

			bIsSitting = false;
			sittingTimer = 0f;
			body.transform.Translate(Vector3.up * 0.5f);

			for (int i = 0; i < eggsWarming.Count; i++) {
				eggsWarming[i].StopWarming();
			}
			eggsWarming.Clear();
		}
	}
}