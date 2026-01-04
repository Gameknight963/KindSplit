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
        private const string IP = "127.0.0.1";
        private const int PORT = 16834;

        public static void Start() => PostCommand("starttimer");
        public static void Pause() => PostCommand("pause");
        public static void Reset() => PostCommand("reset");
        public static void Split() => PostCommand("split");

        private static void PostCommand(string command) {
            TcpClient client = new TcpClient(IP, PORT);
            var stream = client.GetStream();
            byte[] data = Encoding.ASCII.GetBytes($"{command}\r\n");
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