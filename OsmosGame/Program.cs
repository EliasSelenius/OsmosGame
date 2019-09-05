using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;

using Glow;
using OpenTK;
using OpenTK.Graphics.OpenGL4;

namespace OsmosGame {
    class Program {

        public static GameWindow window;

        public static ShaderProgram shader;

        public static readonly List<GameObject> objects = new List<GameObject>();

        public static readonly Random random = new Random();


        static void Main(string[] args) {
            window = new GameWindow(1600, 900);

            // setup events:
            window.UpdateFrame += Window_UpdateFrame;
            window.RenderFrame += Window_RenderFrame;
            window.Load += Window_Load;
            window.Resize += Window_Resize;


            window.Run();
        }

        private static void Window_Resize(object sender, EventArgs e) {
            GL.Viewport(0, 0, window.Width, window.Height);

        }

        private static void Window_Load(object sender, EventArgs e) {

            GL.ClearColor(0, 0, 0, 0);

            // setup shaderProgram:
            var f = new Shader(ShaderType.FragmentShader, File.ReadAllText("data/frag.glsl"));
            var v = new Shader(ShaderType.VertexShader, File.ReadAllText("data/vert.glsl"));
            shader = new ShaderProgram(f, v);
            f.Dispose();
            v.Dispose();

            var p = new Player();
            objects.Add(p);
            objects.Add(new Camera());
            Camera.MainCamera.Target = p;

            for (int i = 0; i < 10; i++) {
                var s = new Sphere();
                s.position.X = (float)(random.NextDouble() * 10f) - 5f;
                s.position.Y = (float)(random.NextDouble() * 10f) - 5f;
                objects.Add(s);
            }
        }

        public static float TotalRenderTime;
        public static float DeltaTime;
        private static void Window_RenderFrame(object sender, FrameEventArgs e) {
            GL.Clear(ClearBufferMask.ColorBufferBit);

            // update timevalues
            DeltaTime = (float)e.Time;
            TotalRenderTime += DeltaTime;
            shader.SetFloat("time", TotalRenderTime);

            // update camera 
            Camera.MainCamera.UpdateTransform();
            Camera.MainCamera.UpdateProjection();

            // render objects
            shader.Use();
            foreach (var item in from o in objects
                                 orderby o is Sphere s ? s.Radius : float.MaxValue descending
                                 select o) {
                item.Render();
            }

            GL.Flush();
            window.SwapBuffers();
        }

        private static void Window_UpdateFrame(object sender, FrameEventArgs e) {
            Input.Update();
            Collisions.Update();
            for (int i = 0; i < objects.Count; i++) {
                objects[i].Update();
            }
        }
    }
}
