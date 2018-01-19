using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Chickoggo {

	public class FluffController : AIController {

		protected Fluff _myFluff;

		public Fluff MyFluff {
			get {
				if (_myFluff == null) {
					_myFluff = (Fluff)pawn;
				}
				return _myFluff;
			}
		}

		protected override void SetAIStates() {
			base.SetAIStates();

			stateMachine.AddState(new AIState_Following(this));
		}

		protected override void CallInitialState() {
			stateMachine.StartAIMachine(AIStateNames.Following);
		}
	}
}
