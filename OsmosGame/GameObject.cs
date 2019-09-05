using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OpenTK;

namespace OsmosGame {
    abstract class GameObject {
        public virtual Matrix4 matrix {
            get => Matrix4.CreateFromAxisAngle(Vector3.UnitZ, rotation) * Matrix4.CreateScale(scale.X, scale.Y, 1) * Matrix4.CreateTranslation(position.X, position.Y, 0);
        }
        //public Vector2 position {
        //    get => transform.ExtractTranslation().Xy;
        //    set => transform.Row3.Xy = value;
        //}
        //public Vector2 scale {
        //    get => transform.ExtractScale().Xy;
        //    set 
        //}
        public Vector2 position = Vector2.Zero;
        public Vector2 scale = Vector2.One;
        public float rotation = 0;

        public void Render() {
            Program.shader.SetMat4("transform", matrix);
            this.Draw();
        }
        public virtual void Draw() { }
        public virtual void Update() { }
    }
}
