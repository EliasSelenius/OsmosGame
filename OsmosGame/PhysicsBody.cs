using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OpenTK;

namespace OsmosGame {
    abstract class PhysicsBody : GameObject {

        public Vector2 velocity = Vector2.Zero;
        public float Mass;

        public PhysicsBody(float m) {
            Mass = m;
        }

        public override void Update() {
            position += velocity * Program.DeltaTime;
        }

        public void AddForce(Vector2 force) {
            velocity += force / Mass;
        }
    }
}
