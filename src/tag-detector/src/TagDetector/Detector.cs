using OpenCvSharp;
using OpenCvSharp.Extensions;
using System.Collections.Generic;
using System.Drawing;
using TagDetector.Models;
using ZBar;

namespace TagDetector
{
    public class Detector
    {

        private static Bitmap prepareInput(System.Drawing.Image image, bool showProcessedInput = false)
        {            
            Mat src = BitmapConverter.ToMat((Bitmap)image);

            // Si no es jpg la convertimos
            if (!image.RawFormat.Equals(System.Drawing.Imaging.ImageFormat.Jpeg))
            {
                byte[] buffer;
                Cv2.ImEncode(".jpg", src, out buffer);
                src = Cv2.ImDecode(buffer, ImreadModes.AnyColor);
            }

            // Prueba con canny
            //Mat prepared = new Mat();
            //Cv2.Canny(converted, prepared, 300, 900);

            // Aplicamos filtro para descartar ruido
            Cv2.InRange(src, new Scalar(0, 0, 0), new Scalar(128, 128, 128), src);
            Cv2.BitwiseNot(src, src);

            // Mostramos el resultado, según el parámetro especificado
            if (showProcessedInput)
            {
                using (new Window("Source", WindowMode.FreeRatio, src))
                {
                    Cv2.WaitKey();
                }
            }
            return BitmapConverter.ToBitmap(src);
        }
        
        /// <summary>
        /// Detecta múltiples tags en una imagen
        /// </summary>
        /// <param name="image">Imagen a procesar</param>
        /// <param name="showProcessedInput">Si se fija a true se muestra el resultado del procesamiento previo que
        /// se realiza antes del proceso de detección de tags</param>
        /// <returns>Lista de tags</returns>
        public static List<Tag> detectTags(System.Drawing.Image image, bool showProcessedInput = false)
        {
            List<Tag> result = new List<Tag>();
            // Procesamos la imagen de entrada
            using (System.Drawing.Image img = image)//prepareInput(image, showProcessedInput))
            {
                using (ImageScanner scanner = new ImageScanner())
                {
                    List<Symbol> symbols = scanner.Scan(img);
                    if (symbols != null && symbols.Count > 0)
                    {
                        // Iteramos los resultados y los transformamos a objetos Tag
                        foreach (Symbol symbol in symbols)
                        {
                            result.Add(new Tag(symbol));
                        }
                    }
                }
            }            
            return result;
        }

    }
}
