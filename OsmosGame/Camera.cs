using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OpenTK;
using OpenTK.Input;

namespace OsmosGame {
    class Camera : GameObject {

        public static Camera MainCamera;

        public Camera(bool overrideLastCam = false) {
            if (MainCamera == null || overrideLastCam) {
                MainCamera = this;
            } else {
                throw new Exception("There is already a Camera object in use");
            }

            

        }

        public GameObject Target;
        public float Zoom = 10;

        public override Matrix4 matrix {
            get => Matrix4.CreateFromAxisAngle(Vector3.UnitZ, rotation) * Matrix4.CreateScale(scale.X, scale.Y, 1) * Matrix4.CreateTranslation(-position.X, -position.Y, 0);
        }

        public override void Update() {
            Zoom -= Input.WheelDelta;

            position = Target.position;
        }


        public void UpdateTransform() {
            Program.shader.SetMat4("camera", matrix);
        }

        public void UpdateProjection() {
            var rez = (float)Program.window.Height / (float)Program.window.Width;
            var m = Matrix4.CreateOrthographic(Zoom, rez * Zoom, -1, 100);
            Program.shader.SetMat4("projection", m);
        }

    }
}
