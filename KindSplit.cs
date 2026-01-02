using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using MelonLoader;
using UnityEngine.SceneManagement;
using JetBrains.Annotations;
using System.Net.Sockets;

namespace KindSplit
{
    public class LiveSplit
    {
        public static void Start()
        {
            TcpClient client = new TcpClient("127.0.0.1", 16834);
            var stream = client.GetStream();
            byte[] data = Encoding.ASCII.GetBytes("starttimer\r\n");
            stream.Write(data, 0, data.Length);
        }
        public static void Pause()
        {
            TcpClient client = new TcpClient("127.0.0.1", 16834);
            var stream = client.GetStream();
            byte[] data = Encoding.ASCII.GetBytes("pause\r\n");
            stream.Write(data, 0, data.Length);
        }
        public static void Reset()
        {
            TcpClient client = new TcpClient("127.0.0.1", 16834);
            var stream = client.GetStream();
            byte[] data = Encoding.ASCII.GetBytes("reset\r\n");
            stream.Write(data, 0, data.Length);
        }
        public static void Split()
        {
            TcpClient client = new TcpClient("127.0.0.1", 16834);
            var stream = client.GetStream();
            byte[] data = Encoding.ASCII.GetBytes("split\r\n");
            stream.Write(data, 0, data.Length);
        }


    }
    public class Mod : MelonMod
    {
        string activeScene;

        public override void OnInitializeMelon()
        {
            LoggerInstance.Msg("KindSplit - autosplitting for MiSide:Zero");
        }
        public override void OnSceneWasLoaded(int buildIndex, string sceneName)
        {
            activeScene = SceneManager.GetActiveScene().name;
            if (activeScene != "Version 1.9 POST")
            {
                LiveSplit.Split();
            }

            if (activeScene == "Version 1.9 POST")
            {
                LiveSplit.Reset();
                LiveSplit.Start();
            }
        }
    }
}