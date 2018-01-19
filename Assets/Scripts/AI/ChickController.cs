using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Chickoggo {

	public class ChickController : FluffController {

		protected Chick _myChick;

		public Chick MyChick {
			get {
				if (_myChick == null) {
					_myChick = (Chick)pawn;
				}
				return _myChick;
			}
		}
	}
}
