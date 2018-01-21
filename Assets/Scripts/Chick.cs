using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Chickoggo {

	/// <summary>
	/// Baby chickens that the player can order around.
	/// </summary>
	public class Chick : Fluff {

		protected override void SetDefaultValues() {
			base.SetDefaultValues();

			speed = 5f;
		}
	}
}
