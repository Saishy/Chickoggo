using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Chickoggo {
	public class PlayerController : Controller {

		public static PlayerController instance;

		public Pawn testPawn;

		protected Vector3 newVel = new Vector3();

		protected Doggo doggoPawn;

		protected override void AwakeInit() {
			base.AwakeInit();

			instance = this;
		}

		protected override void StartInit() {
			base.StartInit();

			Possess(testPawn);
		}

		public override bool Possess(Pawn newPawn) {
			bool returnVal = base.Possess(newPawn);

			if (returnVal) {
				doggoPawn = newPawn as Doggo;
			}

			return returnVal;
		}

		private void Update() {
			if (pawn == null) {
				return;
			}

			newVel.x = Input.GetAxis("Horizontal");
			newVel.z = Input.GetAxis("Vertical");

			pawn.ManualMovement(newVel);

			if (Input.GetButtonDown("Bork")) {
				doggoPawn.SitDown();
			} else if (Input.GetButtonUp("Bork")) {
				doggoPawn.SitUp();
			}
		}

		public Vector3 GetDoggoPosition() {
			return doggoPawn.transform.position;
		}
	}
}
