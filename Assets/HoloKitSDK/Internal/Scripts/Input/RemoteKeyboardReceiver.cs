using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net.Sockets;
using System;
using System.Net;
using System.Text;
using System.Runtime.InteropServices;

namespace HoloKit
{
    public class RemoteKeyboardReceiver : Singleton<RemoteKeyboardReceiver>
    {
        public int Port = 5555;

        private UdpClient client;

        private bool isDestroyed;

        private struct KeyStroke
        {
            public char key;
            public int frame;
        }
        private List<KeyStroke> keyStrokes = new List<KeyStroke>();
        private int frameCount = 0;

        public bool GetKeyDown(char key)
        {
            lock (((ICollection)keyStrokes).SyncRoot)
            {
                for (int i = 0; i < keyStrokes.Count; i++)
                {
                    if (keyStrokes[i].key == key && keyStrokes[i].frame == frameCount)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        void Start()
        {
            client = new UdpClient(Port);
            try
            {
                client.BeginReceive(new AsyncCallback(udpReceive), null);
                Debug.Log("UDP begin");
            }
            catch (Exception)
            {
                Debug.LogError("Failed to start UDP receiver.");
            }

            InstallKeyCommandHandler();
        }

        void Update()
        {
            lock (((ICollection)keyStrokes).SyncRoot)
            {
                for (int i = 0; i < keyStrokes.Count; i++)
                {
                    if (keyStrokes[i].frame == frameCount)
                    {
                        keyStrokes.RemoveAt(i);
                        i--;
                    }
                }
                frameCount++;
            }
        }

        public override void OnDestroy()
        {
            if (client != null)
            {
                client.Close();
            }

            isDestroyed = true;

            base.OnDestroy();
        }

        public void OnKeyCommand(string msg) {
            Debug.Log("KeyCommand: " + msg);

            lock (((ICollection)keyStrokes).SyncRoot)
            {
                foreach (char key in msg)
                {
                    keyStrokes.Add(new KeyStroke
                    {
                        key = key,
                        frame = frameCount + 1
                    });
                }
            }
        }

        private void udpReceive(IAsyncResult res)
        {
            IPEndPoint RemoteIpEndPoint = new IPEndPoint(IPAddress.Any, 5555);
            byte[] received = client.EndReceive(res, ref RemoteIpEndPoint);

            string str = Encoding.ASCII.GetString(received);

            Debug.Log("Received: " + str);

            lock (((ICollection)keyStrokes).SyncRoot)
            {
                foreach (char key in str)
                {
                    keyStrokes.Add(new KeyStroke
                    {
                        key = key,
                        frame = frameCount + 1
                    });
                }
            }

            if (!isDestroyed)
            {
                client.BeginReceive(new AsyncCallback(udpReceive), null);
            }
        }

        #if UNITY_EDITOR
        private void InstallKeyCommandHandler() 
        {
            Debug.Log("KeyCommandHandler not supported in editor.");
        }          
        #elif UNITY_IOS
        [DllImport ("__Internal")]
        private static extern void InstallKeyCommandHandler();
        #else
        private void InstallKeyCommandHandler() 
        {
            
        }          
        #endif
    }
}