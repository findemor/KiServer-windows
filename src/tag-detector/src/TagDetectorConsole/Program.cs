using OpenCvSharp;
using OpenCvSharp.Extensions;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using TagDetector;
using TagDetector.Models;

namespace TagDetectorConsole
{
    class Program
    {
        static void Main(string[] args)
        {            
            string filename = "qrTest2.jpg";
            Image input = Image.FromFile(filename);
            List<Tag> result = Detector.detectTags(input, showProcessedInput: false);

            Mat dst = BitmapConverter.ToMat((Bitmap)input);
            foreach(Tag tag in result)
            {
                System.Drawing.Point[] _points = tag.Polygon;
                List<OpenCvSharp.Point> points = new List<OpenCvSharp.Point>();
                // Convertimos a array de puntos compatible con OpenCV
                foreach (System.Drawing.Point _point in _points)
                {
                    OpenCvSharp.Point point = new OpenCvSharp.Point(_point.X, _point.Y);
                    points.Add(point);
                }
                List<OpenCvSharp.Point> hull = new List<OpenCvSharp.Point>();

                // Si los puntos no forman un cuadrado hayamos la envolvente convexa            
                if (points.Count > 4)
                    Cv2.ConvexHull(InputArray.Create(points), OutputArray.Create(hull));
                else
                    hull = points;

                // La pintamos sobre la imagen
                int n = hull.Count;
                for (int j = 0; j < n; j++)
                {
                    Cv2.Line(dst, hull[j], hull[(j + 1) % n], new Scalar(0, 0, 255), 6);
                }
                string text = Encoding.ASCII.GetString(tag.Data);
                Cv2.PutText(dst, text, points[2], HersheyFonts.HersheyDuplex, 2, new Scalar(0, 0, 255), 4);
            }

            // Redimensionamos y mostramos el resultado
            Mat resizedDst = new Mat();
            Cv2.Resize(dst, resizedDst, new OpenCvSharp.Size(1080, 720));
            using (new Window("Result", WindowMode.FreeRatio, resizedDst))
            {
                Cv2.WaitKey();
            }
        }
    }
}
