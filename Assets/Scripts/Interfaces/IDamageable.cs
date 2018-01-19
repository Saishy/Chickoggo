using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Chickoggo {

	/// <summary>
	/// Implements ways to target and damage an Actor.
	/// </summary>
	public interface IDamageable {

		bool CanBeDamaged();
	}
}
