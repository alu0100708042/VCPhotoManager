using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
namespace VCPhotoManager.Clases
{
    /// <summary>
    /// 
    /// </summary>
    public class Manager
    {
        /// <summary>
        /// Funcio que recibe un Bitmap y lo convierte en escala de grises.
        /// </summary>
        /// <param name="image">Imagen original que se va a convertir en escala de grises.</param>
        /// <returns>Bitmap convertido en escala de grises</returns>
        public Bitmap changeToGrayScale(Bitmap image)
        {
            
            Bitmap aux = new Bitmap(image.Width, image.Height);
            for (int x = 0; x < image.Width; x++) 
            {
                for (int y = 0; y < image.Height; y++)
                {
                    Color color = image.GetPixel(x, y);

                    int valueRed = color.R;
                    int valueGreen = color.G;
                    int valueBlue = color.B;
                    // Conversión PAL
                    Double Value = (valueRed * 0.222 + valueGreen * 0.707 + valueBlue * 0.071);
                    Color c = Color.FromArgb((int)Value, (int)Value,(int)Value);
                    aux.SetPixel(x, y, c);
                }
            }
            return aux;
            
        }

        public Double Entropia(Bitmap image)
        {
            int[] bitsColor = new int[256];
            Double entropy= 0.0,probability;
            for (int i = 0; i < 256; i++)
            {
                bitsColor[i] = 0;
            }
            Color aux;
            for (int i = 0; i < image.Width; i++)
            {
                for (int j = 0; j < image.Height; j++) 
                {
                    aux = image.GetPixel(i, j);
                    bitsColor[aux.R] += 1;
                }
            }

            for (int i = 0; i < 256; i++)
            {
                //Funcion para calcular entropia  E = -Sum(P(i)*log(P(i))))   
                probability = bitsColor[i] / (Double)(image.Width * image.Height);
                if(probability != 0) // La probabilidad tiene que ser distinta de 0 para poder hacer el logaritmo
                    entropy += probability * Math.Log(probability, 2);
            }
            entropy = -entropy;
            return entropy;
        }
    }   
}
