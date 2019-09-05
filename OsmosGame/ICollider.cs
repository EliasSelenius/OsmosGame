using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OsmosGame {
    static class Collisions {

        private static readonly List<ICollider> colliders = new List<ICollider>();

        public static void ColliderEnable(this ICollider collider, bool enable) {
            if (enable) {
                colliders.Add(collider);
            } else {
                colliders.Remove(collider);
            }
        }

        public static bool IsColliderEnabled(this ICollider collider) => colliders.Contains(collider);

        public static void Update() {
            for (int i = 0; i < colliders.Count; i++) {
                var e = colliders[i];
                for (int j = 0; j < colliders.Count; j++) {
                    var e2 = colliders[j];
                    if (e != e2) {
                        if (e.Overlaps(e2)) {
                            e.OnCollide(e2);
                        }
                    }
                }
            }
        }
    }

    interface ICollider {

        bool Overlaps(ICollider other);
        void OnCollide(ICollider other);
    }
}
