using UnityEngine;

namespace UrbanFrontline.Server.Core.Networks
{
    public class ServerTester : MonoBehaviour
    {
        private void Awake()
        {
            new TestServer().Start();
        }
    }
}