using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OpenTK;
using OpenTK.Input;

namespace OsmosGame {
    class Player : Sphere {

        public override void Update() {
            base.Update();

            var keystate = Keyboard.GetState();

            var force = Vector2.Zero;

            if (keystate.IsKeyDown(Key.W)) {
                force += Vector2.UnitY;
            }

            if (keystate.IsKeyDown(Key.S)) {
                force -= Vector2.UnitY;
            }

            if (keystate.IsKeyDown(Key.D)) {
                force += Vector2.UnitX;
            }

            if (keystate.IsKeyDown(Key.A)) {
                force -= Vector2.UnitX;
            }

            this.AddForce(force);
        }
    }
}
