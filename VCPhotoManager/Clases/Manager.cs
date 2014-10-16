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
        /// Función que recibe un Bitmap y lo convierte en escala de grises.
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
        
        /// <summary>
        /// Función que recibe un BitMap y calcula su entropía
        /// </summary>
        /// <param name="image">Imagen de la cual se calculará su entropía</param>
        /// <returns>Double con el cálculo matemático de la entropía</returns>
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


        //public Int32[] getNormalizeHistogram(Int32[] histograma)
        //{
        //    //Decimal acumulado = 0;
        //    //Int32[] aux = new Int32[256];
        //    Decimal acumulado = 0;
        //    Int32[] result = new Int32[256];
        //    Decimal[] histNormal = new Decimal[256];
        //    for(int i = 0; i < 256; i++)
        //    {
        //        result[i] = histograma[i];
        //        histNormal[i] = 0;
        //        acumulado += result[i];
        //    }

        //    // Normalizar el vector
        //    for(int i = 0; i < 256; i++)
        //    {
        //        histNormal[i] = histograma[i] /acumulado;
        //    }



        //    for(int i = 0; i < 256; i++)
        //    {
        //        result[i] = (Int32)(histNormal[i] *300);
        //    }
        // /*   for(int i = 0; i < 256; i++)
        //    {
        //        acumulado += values[i];
        //    }

        //    for(int i = 0; i < 256; i++)
        //    {
        //        aux[i] = (Int32)Math.Round((Decimal)(values[i] / acumulado),2) * 300;
        //    }*/
        //    return result;
        //}


        /// <summary>
        /// Función que se le pasará el vector del histograma para normalizarlo a la hora de dibujar
        /// </summary>
        /// <param name="histograma"> Vector que contiene el histograma de frecuencias de una imagen</param>
        /// <returns>Devuelve un vector de histograma normalizado para su altura de 300 pixels</returns>
        public Int32[] getNormalizeHistogram(Int32[] histograma)
        {

            Decimal acumulado = 0;
            Int32[] result = new Int32[256];
            
            for (int i = 0; i < 256; i++)
            {
                if(acumulado < histograma[i])
                {
                    acumulado = histograma[i];
                }
            }
            
            for (int i = 0; i < 256; i++)
            {
                result[i] = (Int32)((histograma[i]*299)/acumulado);
            }

            return result;
        }

        /// <summary>
        /// Función que recibe un BitMap y devolverá el cálculo matemático de su Histograma
        /// </summary>
        /// <param name="mapa"> Imagen de la cual calcularemos su Histograma</param>
        /// <returns>Vector de enteros con el cálculo del Histograma</returns>
        public Int32[] getHistogram(Bitmap mapa)
        {
            Int32[] result = new Int32[256];

            for(int i = 0; i < 256; i++)
            {
                result[i] = 0;
            }

            for(int i = 0; i < mapa.Width; i++)
            {
                for(int j = 0; j < mapa.Height; j++)
                {
                    Color pixel = mapa.GetPixel(i, j);
                    result[pixel.R] = result[pixel.R] + 1;
                }
            }

            return result;
        }

        /// <summary>
        /// Función que devuelve el Histograma Acumulativo
        /// </summary>
        /// <param name="histograma">Vector con el histograma para calcular su acumulativo</param>
        /// <returns>Vector con el cálculo del Histograma acumulativo</returns>
        public Int32[] getCumulativeHistogram(Int32[] histograma)
        {
            Decimal acumulado = 0;
            Int32[] result = new Int32[256];
            Decimal[] histogramaAcumulado = new Decimal[256];

            for(int i = 0; i < 256; i++)
            {
                result[i] = histograma[i];
                histogramaAcumulado[i] = 0;
                acumulado += result[i];
            }

            // Normalizar el vector
            for(int i = 0; i < 256; i++)
            {
                histogramaAcumulado[i] = histograma[i] / acumulado;
            }

            acumulado = 0;
            for(int i = 0; i < 256; i++)
            {
                acumulado += histogramaAcumulado[i] * 299;
                result[i] = (Int32)acumulado;
            }
                
            return result;
        }

        /// <summary>
        /// Copia en el portapapeles del sistema la Imagen que recibe.
        /// </summary>
        /// <param name="mapa">Imagen que se copiará en el portapapeles</param>
        public void copy(Bitmap mapa)
        {
            System.Windows.Forms.Clipboard.SetImage(mapa);
        }
    }   
}
