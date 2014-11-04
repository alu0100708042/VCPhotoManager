﻿using System;
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

        public Bitmap linearTransformation(List<Point> coords, Bitmap image)
        {
            Bitmap result = new Bitmap(image.Width, image.Height);
            for (int i = 1; i < coords.Count; i++)
            {
                Int32 a = (coords[i].Y - coords[i-1].Y) / (coords[i].X - coords[i-1].X);
                Int32 b = coords[i].Y - a*coords[i].X;

                for (int x = 0; x < image.Width; x++)
                {
                    for (int y = 0; y < image.Height; y++)
                    {
                        Color aux = image.GetPixel(x, y);
                        if (aux.R >= coords[i - 1].X && aux.R <= coords[i].X)
                        {
                            byte transcolor = (byte)(a * aux.R + b);

                            Color newaux = Color.FromArgb(transcolor, transcolor, transcolor);
                            result.SetPixel(x, y, newaux);
                        }

                    }
                }
            }
            return result;
        }

        public Bitmap noLinearTransformation(Bitmap Image, Double gamma)
        {
            Bitmap result = new Bitmap(Image.Width, Image.Height);
            Int32[] h_aux =getHistogram(Image);
            Double[] h_aux_d = new Double[256];
            Int32 acumulado = 0;
            for (int i = 0; i < 256; i++)
            {
                h_aux_d[i] = h_aux[i];
            }
            // Normalizamos entre 0 y 1
            for (int i = 0; i < 256; i++)
            {
                if (acumulado < h_aux[i])
                {
                    acumulado = h_aux[i];
                }
            }
            for (int i = 0; i < 256; i++)
            {
                h_aux_d[i] = (Double)(i) / (Double)(255);
            }
            // Hacemos la transformación no lineal
            for (int i = 0; i < 256; i++)
            {
                h_aux_d[i]=Math.Pow(h_aux_d[i],gamma);
            }
            for (int x = 0; x < Image.Width; x++)
            {
                for (int y = 0; y < Image.Height; y++)
                {
                    Color aux = Image.GetPixel(x, y);
                    byte transcolor = (byte)aux.R;
                    Color newaux = Color.FromArgb((byte)(h_aux_d[transcolor]*255), (byte)(h_aux_d[transcolor]*255), (byte)(h_aux_d[transcolor]*255));
                    result.SetPixel(x, y, newaux);

                }
            }
        return result;
        }
        /// <summary>
        /// Función que recibe un BitMap y calcula su entropía
        /// </summary>
        /// <param name="image">Imagen de la cual se calculará su entropía</param>
        /// <returns>Double con el cálculo matemático de la entropía</returns>
        public Double Entropia(Bitmap image)
        {
            int[] bitsColor = new int[256];
            Double entropy = 0.0, probability;
            Color aux;

            for (int i = 0; i < 256; i++)
            {
                bitsColor[i] = 0;
            }
            
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
                // La probabilidad tiene que ser distinta de 0 para poder hacer el logaritmo
                probability = bitsColor[i] / (Double)(image.Width * image.Height);
                if(probability != 0)
                {
                    entropy += probability * Math.Log(probability, 2);
                }
            }
            entropy = -entropy;
            return entropy;
        }

        public Int32[] brightnessAndContrast(Bitmap Image) 
        {
            Int32[] bac = new Int32[2];
            bac[0] = 0;
            bac[1] = 1;
            Int32[] n_pixels = getHistogram(Image);
            //Brillo
            for (int i = 0; i < 256; i++) 
            {
                bac[0] += n_pixels[i] * i;
            }
            bac[0] = (Int32)(bac[0]/(Image.Width * Image.Height));
            //Contraste
            for (int i = 0; i < 256; i++) 
            {
                bac[1] += (Int32)(n_pixels[i] * Math.Pow((bac[0]-i),2));
            }
            bac[1] = (Int32)(Math.Sqrt(bac[1]/(Image.Width*Image.Height)));
            return bac;
        }

        public Bitmap changeBrightnessAndContrast(Int32 nBrillo, Int32 nContraste,Bitmap Image)
        {
            Int32[] byc = brightnessAndContrast(Image);
            Bitmap result = new Bitmap(Image.Width, Image.Height);
            Double a, b;
            Int32[] h_aux = new Int32[256];
            a = (Double)(nContraste) / (Double)(byc[1]); // o' = a * o
            b = nBrillo - (a * byc[0]); // u' =a*u + b 
            for (int i = 0; i < 256; i++) 
            {
                h_aux[i] = (Int32)(a * i + b);
                if (h_aux[i] > 255)
                    h_aux[i] = 255;
                if (h_aux[i] < 0)
                    h_aux[i] = 0;
            }
                for (int i = 0; i < Image.Width; i++)
                {
                    for (int j = 0; j < Image.Height; j++)
                    {
                        Color aux = Image.GetPixel(i, j);
                        byte transcolor = (byte)(h_aux[aux.R]);
                        Color newaux = Color.FromArgb(transcolor, transcolor, transcolor);
                        result.SetPixel(i, j, newaux);
                    }
                }


            /*for (int i = 0; i < Image.Width; i++)
            {

                for (int j = 0; j < Image.Height; j++)
                {
                    Color aux = Image.GetPixel(i, j);
                    int n_byte = aux.R + a;
                    if (n_byte > 255)
                        n_byte = 255;
                    if (n_byte < 0)
                        n_byte = 0;

                    byte transcolor = (byte)(n_byte);
                    Color newaux = Color.FromArgb(transcolor, transcolor, transcolor);
                    result.SetPixel(i, j, newaux);
                }
            }*/

          return result;
        }
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
        /// Método que recibe un BitMap y devuelve el cálculo matemático de su Histograma
        /// </summary>
        /// <param name="mapa">Imagen de la cual calcularemos su Histograma</param>
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

        public Rectangle getRectangle(Point p1, Point p2)
        {
            Rectangle rect = new Rectangle(Math.Min(p1.X, p2.X), 
                Math.Min(p1.Y, p2.Y), Math.Abs(p2.X - p1.X), Math.Abs(p2.Y - p1.Y));
            return rect;
        }

        /// <summary>
        /// Método que crea una imagen a partir de una imagen original y un rectangulo
        /// que representa las dimensiones y posición de la imagen que se desea obtener
        /// eto Bitmap con las dimensiones del rectángulo.
        /// </summary>
        /// <param name="imagen">Bitmap con la imagen original</param>
        /// <param name="rect">Rectangle que contendrá la sub imagen</param>
        /// <returns>Bitmap con las dimensiones del rectángulo</returns>
        public Bitmap createSubBitmap(Bitmap imagen, Rectangle rect)
        {
            try
            {
                return imagen.Clone(rect, imagen.PixelFormat);
            }
            catch(Exception)
            {
                return null;
            }
        }

        public Bitmap getImageDifference(Bitmap img1, Bitmap img2, Int32 precision)
        {
            Bitmap result = null;
            Color color;
            Int32 R = 0, G = 0, B = 0;
            if(img1 != null && img2 != null)
            {
                if(img1.Size == img2.Size)
                {
                    result = new Bitmap(img1);
                    for(int y = 0; y < img1.Size.Height; y++)
                    {
                        for(int x = 0; x < img1.Size.Width; x++)
                        {
                            R = Math.Abs(img1.GetPixel(x, y).R - img2.GetPixel(x, y).R);
                            G = Math.Abs(img1.GetPixel(x, y).G - img2.GetPixel(x, y).G);
                            B = Math.Abs(img1.GetPixel(x, y).B - img2.GetPixel(x, y).B);

                            if(R != 0 || G != 0 || B != 0)
                            {
                                if(R >= precision || G >= precision || B >= precision)
                                {
                                    color = Color.Red;
                                    result.SetPixel(x, y, color);
                                }
                            }
                        }
                    }
                }
            }
            
            return result;
        }
        
    }   
}
