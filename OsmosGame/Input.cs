using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OpenTK.Input;

namespace OsmosGame {
    static class Input {

        private static int _lastWheel;
        private static int _wheel;

        public static int WheelDelta => _wheel - _lastWheel; 

        public static void Update() {
            var mstate = Mouse.GetState();

            _lastWheel = _wheel;
            _wheel = mstate.Wheel;
        }

    }
}
