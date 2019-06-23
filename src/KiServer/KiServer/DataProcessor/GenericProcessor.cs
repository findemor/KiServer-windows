using KiServer.Kinect;
using KiServer.TCP;
using Microsoft.Kinect;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KiServer.DataProcessor
{
    public class GenericProcessor : IDataProcessor
    {
        public const string KEY_DEPTH_ARRAY = "DepthArray";
        public const string KEY_DEPTH_WIDTH = "DepthWidth";
        public const string KEY_DEPTH_HEIGHT = "DepthHeight";
        public const string KEY_DEPTH_MIN = "MinDepth";
        public const string KEY_DEPTH_MAX = "MaxDepth";
        public const string KEY_OBJ_PREFIX = "Obj";


        public TCPData GetProcessedData(KinectData kinectData)
        {
            TCPData pd = new TCPData()
            {
                Metadata = new Dictionary<string, string>()
            };

            int max = kinectData.MaxDepth;
            int min = kinectData.MinDepth;

            if (kinectData.DepthArray != null)
            {
                //enviamos tambien el array de profundidades
                char[] depthsChar = kinectData.DepthArray.Select(p => Convert.ToChar(p)).ToArray();
                string depthsArray = new string(depthsChar);
                string b64 = Base64Encode(depthsArray);

                //Metemos los atributos que queremos enviar
                pd.Metadata.Add(KEY_DEPTH_WIDTH, kinectData.Width.ToString());
                pd.Metadata.Add(KEY_DEPTH_HEIGHT, kinectData.Height.ToString());
                pd.Metadata.Add(KEY_DEPTH_MIN, min.ToString());
                pd.Metadata.Add(KEY_DEPTH_MAX, max.ToString());
                pd.Metadata.Add(KEY_DEPTH_ARRAY, b64);
            }

            if (kinectData.DetectedObjects != null && kinectData.DetectedObjects.Count > 0)
            {
                //enviamos los objetos que se han detectado
                foreach(Kinect.ObjectsDetection.DetectedObject o in kinectData.DetectedObjects)
                {
                    pd.Metadata.Add(KEY_OBJ_PREFIX + o.Data, o.RelCenter.X + "," + o.RelCenter.Y);
                }
            }

            return pd;
        }

        public static string Base64Encode(string plainText)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return System.Convert.ToBase64String(plainTextBytes);
        }
    }
}
