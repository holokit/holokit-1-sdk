using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net.Sockets;
using System;
using System.Net;
using System.Text;


namespace HoloKit
{
    public class RemoteControllerReceiver : Singleton<RemoteControllerReceiver>
    {

        public int Port = 6666;

        private UdpClient client;

        private bool isDestroyed;

        private struct KeyStroke
        {
            public char key;
            public int frame;
        }
        private List<KeyStroke> keyStrokes = new List<KeyStroke>();

        private int frameCount = 0;

        public Quaternion Orientation { get; private set; }

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
            client.Close();
            isDestroyed = true;

            base.OnDestroy();
        }

        private void udpReceive(IAsyncResult res)
        {
            IPEndPoint RemoteIpEndPoint = new IPEndPoint(IPAddress.Any, Port);
            byte[] received = client.EndReceive(res, ref RemoteIpEndPoint);

            string str = Encoding.ASCII.GetString(received);

            Debug.Log("Received: " + str);

            string tapKeyword = "tap ";
            if (str.StartsWith(tapKeyword))
            {
                str = str.Substring(tapKeyword.Length);

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
            }
            else
            {
                updateOrientation(str);
            }

            if (!isDestroyed)
            {
                client.BeginReceive(new AsyncCallback(udpReceive), null);
            }
        }

        private void updateOrientation(string message)
        {
            string[] components = message.Trim().Split(',');
            if (components.Length != 4)
            {
                Debug.LogError("Remote controller: Invalid message received: " + message);
                return;
            }

            Quaternion q = Orientation;
            try
            {
                q = new Quaternion(
                    float.Parse(components[0]),
                    float.Parse(components[1]),
                    float.Parse(components[2]),
                    float.Parse(components[3])
                );
            }
            catch (Exception e)
            {
                Debug.LogError("Remote controller: Failed to parse message: " + message + ", error: " + e.Message);
                return;
            }

            Orientation = Quaternion.Euler(90, 180, 0) * q * Quaternion.Euler(0, 0, 180);
        }
    }
}
