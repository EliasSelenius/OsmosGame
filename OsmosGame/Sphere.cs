using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Glow;
using OpenTK;

using static System.Math;

namespace OsmosGame {
    class Sphere : PhysicsBody, ICollider {

        static readonly VertexArray vao;
        static readonly Buffer<float> vbo;
        static readonly Buffer<uint> ebo;


        static Sphere() {


            var verts = new List<float>();
            var indces = new List<uint>();
            var delta = (PI * 2) / 12;
            for (int i = 0; i < 12; i++) {
                verts.Add((float)Cos(i * delta));
                verts.Add((float)Sin(i * delta));
                indces.Add(12);
                indces.Add((uint)i);
                if (i == 11) {
                    indces.Add(0);
                } else {
                    indces.Add((uint)i + 1);
                }
            }
            verts.Add(0); verts.Add(0);


            vbo = new Buffer<float>();
            vbo.Initialize(verts.ToArray(), OpenTK.Graphics.OpenGL4.BufferUsageHint.StaticDraw);
            ebo = new Buffer<uint>();
            ebo.Initialize(indces.ToArray(), OpenTK.Graphics.OpenGL4.BufferUsageHint.StaticDraw);

            vao = new VertexArray();
            vao.SetBuffer(OpenTK.Graphics.OpenGL4.BufferTarget.ArrayBuffer, vbo);
            vao.SetBuffer(OpenTK.Graphics.OpenGL4.BufferTarget.ElementArrayBuffer, ebo);

            vao.AttribPointer(Program.shader.GetAttribLocation("pos"), 2, OpenTK.Graphics.OpenGL4.VertexAttribPointerType.Float, false, sizeof(float) * 2, 0);
        }

        public Sphere() : base(Program.random.Next(100)) {
            this.ColliderEnable(true);
        }

        public bool Overlaps(ICollider other) {
            if (other is Sphere s) {
                return (position - s.position).Length - (Radius + s.Radius) < 0;
            }
            return false;
        }

        public void OnCollide(ICollider other) {
            if (other is Sphere s) {
                if (Radius < s.Radius) {
                    Mass -= 0.1f;
                } else {
                    Mass += 0.1f;
                }
            }
        }

        public override void Draw() {
            vao.DrawElements(OpenTK.Graphics.OpenGL4.PrimitiveType.Triangles, 12 * 3, OpenTK.Graphics.OpenGL4.DrawElementsType.UnsignedInt);
        }

        public float Radius => Mass / 100f;

        public override void Update() {
            base.Update();

            scale = Vector2.One * Radius;

            if (Mass < 10) {
                Destroy();
            }

        }

    }
}
