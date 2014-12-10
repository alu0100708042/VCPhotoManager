using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Collections;

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


  
        public Int32 getMinValue(Bitmap imagen)
        {
            Int32 result = Int32.MaxValue;
            for(int x = 0; x < imagen.Width; x++)
            {
                for(int y = 0; y < imagen.Height; y++)
                {
                    if(imagen.GetPixel(x, y).R < result)
                    {
                        result = imagen.GetPixel(x, y).R;
                    }
                }
            }
            return result;
        }

        public Int32 getMaxValue(Bitmap imagen)
        {
            Int32 result = Int32.MinValue;
            for(int x = 0; x < imagen.Width; x++)
            {
                for(int y = 0; y < imagen.Height; y++)
                {
                    if(imagen.GetPixel(x, y).R > result)
                    {
                        result = imagen.GetPixel(x, y).R;
                    }
                }
            }
            return result;
        }

        public Bitmap linearTransformation(List<Point> puntos, Bitmap image)
        {

            Bitmap result = new Bitmap(image.Width, image.Height);
            for (int i = 0; i < puntos.Count; i+=2)
            {
                Double a = (Double)(puntos[i+1].Y - puntos[i].Y) / (puntos[i+1].X - puntos[i].X);
                Double b = (Double)(puntos[i+1].Y - a * puntos[i+1].X);

                for (int x = 0; x < image.Width; x++)
                {
                    for (int y = 0; y < image.Height; y++)
                    {
                        Color aux = image.GetPixel(x, y);
                        if (aux.R >= puntos[i].X && aux.R <= puntos[i+1].X)
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
            bac[1] = 0;
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

          return result;
        }

        public Bitmap EcualizeImage(Bitmap Image, Int32 maxValue, Int32 minValue)
        {
            Decimal acumulado = 0;
            Bitmap result = new Bitmap(Image.Width, Image.Height);
            Int32[] AcumulativeHistogram = new Int32[256];
            Int32[] Histogram = getHistogram(Image);
            Int32[] Vout = new Int32[256];
            Int32 m = 256;
            Int32 size = Image.Width * Image.Height;

            // Vout = max[0, rount(M/Size*Co(Vin)) -1 ]
            // M niveles de gris que haya
            //Size tamaño imagen
            //Co histograma acumulativo de Ii

            // Calculamos el histograma acumulativo de la imagen actual
            for(int i = 0; i < 256; i++)
            {
                acumulado += Histogram[i];
                AcumulativeHistogram[i] = (Int32)acumulado;
            }

            for(int i = 0; i < 256; i++)
            {
                Vout[i] = (Int32)(Math.Max(0, Math.Round((Double)(((Double)m / (Double)size) * AcumulativeHistogram[i] - 1))));
            }

            for(int i = 0; i < Image.Width; i++)
            {
                for(int j = 0; j < Image.Height; j++)
                {
                    Color aux = Image.GetPixel(i, j);

                    byte transcolor = (byte)Vout[aux.R];
                    Color newaux = Color.FromArgb(transcolor, transcolor, transcolor);
                    result.SetPixel(i, j, newaux);
                }
            }
            
            return result;
        }

        /// <summary>
        /// Función que simula la digitalización de una imagen
        /// </summary>
        /// <param name="Image">Imagen sobre la que se desea simular</param>
        /// <param name="desplazamiento">Valor de muestreo</param>
        /// <param name="bits">Número de bits de la imagen resultado</param>
        /// <returns></returns>
        public Bitmap DigitalSimulation(Bitmap Image, Int32 desplazamiento, Int32 bits) 
        {
            Bitmap result = new Bitmap(Image.Width, Image.Height);
            Int32 v_medio;
            Color color= new Color();
            List<Point> cuantizar = new List<Point>();
            for (int x = 0; x < Image.Width; x+= desplazamiento)
            {
                for (int y = 0; y < Image.Height; y+= desplazamiento)
                {
                    v_medio = 0;
                    if (x + desplazamiento < Image.Width && y + desplazamiento < Image.Height -1)
                    {
                        for (int z = x; z < x + desplazamiento; z++)
                        {
                            for (int w = y; w < y + desplazamiento; w++)
                            {
                                color = Image.GetPixel(z, w);
                                v_medio += (Int32)color.R;
                            }
                        }
                        v_medio = Convert.ToInt32(Math.Round((Double)v_medio / (Double)(desplazamiento * desplazamiento), MidpointRounding.AwayFromZero)); // Se multiplica por dos porque se desplaza en forma de matriz
                        for (int z = x; z < x + desplazamiento; z++)
                        {
                            for (int w = y; w < y + desplazamiento; w++)
                            {
                                result.SetPixel(z, w, Color.FromArgb(v_medio, v_medio, v_medio));
                            }

                        }
                    }
                    else // Si es mayor 
                    {
                        if (x + desplazamiento > Image.Width && y + desplazamiento < Image.Height)
                        {
                            for (int w = y; w < y + desplazamiento; w++)
                            {
                                color = Image.GetPixel(x, w);
                                v_medio += (Int32)color.R;
                            }
                            v_medio = v_medio / desplazamiento;
                            for (int w = y; w < y + desplazamiento; w++)
                            {
                                result.SetPixel(x, w, Color.FromArgb(v_medio, v_medio, v_medio));
                            }
                        }
                        else if (x + desplazamiento < Image.Width && y + desplazamiento > Image.Height)
                        {
                            for (int w = x; w < x + desplazamiento; w++)
                            {
                                color = Image.GetPixel(w, y);
                                v_medio += (Int32)color.R;
                            }
                            v_medio = v_medio / desplazamiento;
                            for (int w = x; w < x + desplazamiento; w++)
                            {
                                result.SetPixel(w, y, Color.FromArgb(v_medio, v_medio, v_medio));
                            }
                        }
                        else // Si se pasas los dos se pone el pixel original 
                        {
                            color = Image.GetPixel(x, y);
                            v_medio = color.R;
                            result.SetPixel(x, y, Color.FromArgb(v_medio, v_medio, v_medio));
                        }
                    }
               }
            }
            //Cuantizar

            Int32 original = 255;
            Int32  destino = (Int32)((Math.Pow(2, bits) - 1));
            Int32 rango = (original / destino);


            for (var y = 0; y < result.Height; y++)
            {
                for (var x = 0; x < result.Width; x++)
                {


                    Int32 lugar = (Int32)(Math.Floor((Double)(result.GetPixel(x,y).R / rango)));
                    Int32 redondear;

                    if (result.GetPixel(x,y).R % rango >= rango / 2)
                    {
                        //console.log("REDONDEO ARRIBA");
                        redondear = rango;
                    }
                    else
                    {
                        redondear = 0;
                        //console.log("REDONDEO ABAJO");
                    }
                    byte cfinal = Convert.ToByte((rango * lugar) + redondear);
                    result.SetPixel(x, y, Color.FromArgb(cfinal, cfinal, cfinal));
                }
            }
            /*Int32 v_med = (Int32)(255 / Math.Pow(2, bits));
            for(int x = 0;  x < result.Width; x++)
            {
                for (int y = 0; y < result.Height; y++)
                {
                    for (int z = 0; z < Math.Pow(2,bits); z++) 
                    {
                        Color aux = result.GetPixel(x, y);
                        Int32 tr =(Int32)(Math.Truncate((Double)(aux.R / v_med))); // lugar donde se encuentra
                        byte anterior = Convert.ToByte(v_med * tr);
                        byte posterior = Convert.ToByte(v_med * (tr + 2));
                        if (aux.R > posterior/2)
                        {
                            result.SetPixel(x, y, Color.FromArgb(posterior, posterior, posterior));
                        }
                        else 
                        {
                            result.SetPixel(x, y, Color.FromArgb(anterior,anterior,anterior));
                        }

                    }
                }
            }*/

            /*
            cuantizar.Add(new Point(0, 0));
            cuantizar.Add(new Point(255,Convert.ToInt32(Math.Pow(2,bits))));
            result = linearTransformation(cuantizar, result);
            cuantizar.RemoveAt(1);
            cuantizar.RemoveAt(0);
            cuantizar.Add(new Point(0, 0));
            cuantizar.Add(new Point(Convert.ToInt32(Math.Pow(2, bits)),255 ));
            cuantizar.Add(new Point(Convert.ToInt32(Math.Pow(2,bits))+1, 255));
            cuantizar.Add(new Point(255, 255));
            result = linearTransformation(cuantizar, result);*/
            return result;
        }


        /// <summary>
        /// Función a la cual se le pasan dos imágenes y devuelve la primera con el histograma acumulativo de la segunda
        /// </summary>
        /// <param name="img1"></param>
        /// <param name="img2"></param>
        /// <returns>Retorna la imagen 1 con un histograma similar a la imagen 2</returns>
        public Bitmap HistogramSpecification(Bitmap img1, Bitmap img2)
        {
            Bitmap result = new Bitmap(img1.Width, img1.Height);
            Int32[] pixelTransform = new Int32[256];
            Int32[] Histogram = getHistogram(img1);
            Int32[] HistogramResult = getHistogram(img2);
            Double[] AcumulativeHistogram = new Double[256];
            Double[] DestineHistogram = new Double[256];
            Int32 acumuladoImg1 = 0, acumuladoImg2 = 0, ifuente = 0, idestino = 0;
            for (int i = 0; i < 256; i++)
            {
                pixelTransform[i] = 0;
                acumuladoImg1 += Histogram[i];
                acumuladoImg2 += HistogramResult[i];
                AcumulativeHistogram[i] = (Int32)acumuladoImg1;
                DestineHistogram[i] = (Int32)acumuladoImg2;
            }
            //Normalizamos entre 0 y 1
            for (int i = 0; i < 256; i++)
            {
                AcumulativeHistogram[i] = AcumulativeHistogram[i] / acumuladoImg1;
                DestineHistogram[i] = DestineHistogram[i] / acumuladoImg2;
            }

            while (ifuente < 256 && idestino < 256)
            {
                if (DestineHistogram[idestino] > AcumulativeHistogram[ifuente])
                {
                    pixelTransform[ifuente] = idestino;
                    ifuente++;
                }
                else
                {
                    if (ifuente != 0)
                        pixelTransform[ifuente] = pixelTransform[ifuente - 1];
                    idestino++;
                }
            }

            for (int i = 0; i < result.Width; i++)
            {
                for (int j = 0; j < result.Height; j++)
                {
                    Color aux = img1.GetPixel(i, j);
                    byte transcolor = (byte)pixelTransform[aux.R];
                    Color newaux = Color.FromArgb(transcolor, transcolor, transcolor);
                    result.SetPixel(i, j, newaux);
                }
            }
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
                    if(pixel.A != 0)
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

        public Bitmap getDiference(Bitmap img1, Bitmap img2)
        {
            Bitmap result = new Bitmap(img1.Width,img1.Height);
            Color color = new Color();
            Int32 R = 0, G = 0, B = 0;

            if (img1 != null && img2 != null)
            {
                if (img1.Size == img2.Size)
                {
                    for (int y = 0; y < img1.Size.Height; y++)
                    {
                        for (int x = 0; x < img1.Size.Width; x++)
                        {
                            R = Math.Abs(img1.GetPixel(x, y).R - img2.GetPixel(x, y).R);
                            G = Math.Abs(img1.GetPixel(x, y).G - img2.GetPixel(x, y).G);
                            B = Math.Abs(img1.GetPixel(x, y).B - img2.GetPixel(x, y).B);
                            color = Color.FromArgb(R, G, B);
                            result.SetPixel(x,y,color);
                        }
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="image"></param>
        /// <returns></returns>
        public Bitmap EspejoHorizontal(Bitmap image)
        {
            Bitmap result = image.Clone() as Bitmap;
            result.RotateFlip(RotateFlipType.RotateNoneFlipX);
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="image"></param>
        /// <returns></returns>
        public Bitmap EspejoVertical(Bitmap image)
        {
            Bitmap result = image.Clone() as Bitmap;
            result.RotateFlip(RotateFlipType.RotateNoneFlipY);
            return result;    
        }

        public Bitmap ZoomVMP(Bitmap image, float nwidth, float nheigth)
        {
            Bitmap result = new Bitmap((Int32)nwidth, (Int32)nheigth);
            float factorX =(nwidth / image.Width);
            float factorY = (nheigth / image.Height);
            for (int x = 0; x < result.Width; x++)
            {
                for (int y = 0; y < result.Height; y++)
                { 
                    result.SetPixel(x,y,image.GetPixel((Int32)(x/factorX),(Int32)(y/factorY)));
                }
            }
                return result;
        }

        public Bitmap ZoomBilineal(Bitmap image, Int32 nwidth, Int32 nheigth)
        {
            Bitmap result = new Bitmap(nwidth, nheigth);
            Double factorEscalaX = Convert.ToDouble((Double)result.Width / (Double)image.Width);
            Double factorEscalaY = Convert.ToDouble((Double)result.Height / (Double)image.Height);
            Double p = 0.0, q = 0.0;
            Double[] pTi = new Double[2]; // Variables que almacenan el punto de transformacion inversa
            Int32[] ul = new Int32[2];
            Int32[] ur = new Int32[2];
            Int32[] dl = new Int32[2];
            Int32[] dr = new Int32[2];
            byte ncolor;
            for (int x = 0; x < result.Width; x++)
            {
                for (int y = 0; y < result.Height; y++)
                {
                    pTi[0] = x / factorEscalaX;
                    pTi[1] = y / factorEscalaY;
                    ul[0] = Convert.ToInt32(Math.Truncate(pTi[0]));
                    ul[1] = Convert.ToInt32(Math.Truncate(pTi[1]));
                    if (ul[0] == image.Width-1)
                    {
                        ur[0] = ul[0];
                        dl[0] = ul[0];
                        dr[0] = ul[0];
                        p = 0.0;
                    }
                    else 
                    {
                        ur[0] = ul[0] + 1;
                        dl[0] = ul[0];
                        dr[0] = ul[0] + 1;
                        p = pTi[0] - dl[0];
                    }
                    if (ul[1] == image.Height-1)
                    {
                        ur[1] = ul[1];
                        dl[1] = ul[1];
                        dr[1] = ul[1];
                        q = 0.0;
                    }
                    else
                    {
                        ur[1] = ul[1];
                        dl[1] = ul[1] + 1;
                        dr[1] = ul[1] + 1;
                        q = dl[1] - pTi[1];
                    }

                    Color A = image.GetPixel(ul[0],ul[1]);
                    Color B = image.GetPixel(ur[0],ur[1]);
                    Color C = image.GetPixel(dl[0],dl[1]);
                    Color D = image.GetPixel(dr[0], dr[1]);
                    ncolor = Convert.ToByte(C.R + (D.R - C.R) * p + (A.R - C.R) * q + (B.R + C.R - A.R - D.R) * p * q);
                    result.SetPixel(x, y, Color.FromArgb(ncolor, ncolor, ncolor));
                }
            }
                return result;
        }


        public Bitmap RotateImageTD(Bitmap Image, Int32 angulo)
        {
            Bitmap result = null;
            Double radianes = (angulo * Math.PI)/ 180; // Paso a radianes
            Double cosT =Math.Round(Math.Cos(radianes),3);
            Double senT = Math.Round(Math.Sin(radianes),3);
            Int32[] nValores = new Int32[2];
            Int32 transX, transY;
               
            Int32 ny, nx; //Auxiliares
            Int32 nWidth, nHeight;
            // Calculo de las esquinas para las dimensiones de la nueva imagen
            //xcos-ysen = x
            //xsen + ycos = y
            if (Image.Width * cosT < 0)
            {
                nWidth = Math.Abs((Int32)(-Image.Width * cosT + Image.Height * senT)); // Alto nueva imagen
                nHeight = Math.Abs((Int32)(Image.Width * senT + Image.Height * cosT)); // Nuevo ancho
            }
            else 
            {
                nWidth = Math.Abs((Int32)(Image.Width * cosT + Image.Height * senT)); // Alto nueva imagen
                nHeight = Math.Abs((Int32)(Image.Width * senT + Image.Height * cosT)); // Nuevo ancho
            }
            
            result = new Bitmap(nWidth, nHeight);
                if (angulo >= 0 && angulo <= 270)
                {
                    transX = Math.Abs((Int32)(Image.Height * senT));
                }
                else 
                {
                    transX = 0;
                }
                if (angulo >= 180 && angulo <= 360)
                {
                    transY = Math.Abs((Int32)(Image.Height * cosT) - 1);
                }
                else
                {
                    transY = 0;
                }
               
                result = new Bitmap(nWidth, nHeight);
                for (int x = 0; x < Image.Width; x++)
                {
                    for (int y = 0; y < Image.Height; y++)
                    { 
                
                        nx = (Int32)(x*cosT - y*senT);
                        ny = (Int32)(x*senT+y*cosT);
                        if ((Math.Abs(ny + transY) >= result.Height) && (Math.Abs(nx + transX) >= result.Width))
                        {
                            result.SetPixel(Math.Abs(result.Width - 1), Math.Abs(result.Height - 1), Image.GetPixel(x, y));
                        }
                        else if (Math.Abs(ny + transY) >= result.Height)
                        {
                            result.SetPixel(Math.Abs(nx + transX), Math.Abs(result.Height - 1), Image.GetPixel(x, y));
                        }
                        else if (Math.Abs(nx + transX) >= result.Width)
                        {
                            result.SetPixel(Math.Abs(result.Width - 1), Math.Abs(ny + transY), Image.GetPixel(x, y)); 
                        }
                        else
                        {
                            result.SetPixel(Math.Abs(nx + transX), Math.Abs(ny + transY), Image.GetPixel(x, y));
                        }
                    }
                }
                return result;
        }


     
        public Bitmap rotarBasico(Bitmap Image, Int32 grados)
        {

            Bitmap result = null;

            switch(grados){
                    case 90:
                            result = new Bitmap(Image.Height,Image.Width);  

                            for(int y = 0; y < result.Height; y++) { 
                                    for(int x = 0; x < result.Width; x++) {
                                        result.SetPixel(x,y,Image.GetPixel(y,x));
                                    }
                            }
                        


                    break;

                    case 180:
                            result = new Bitmap(Image.Width,Image.Height);

                            for(int y = 0; y < result.Height; y++) 
                            { 
                                    for(var x = 0; x < result.Width; x++) 
                                    {
                                        
                                        result.SetPixel(x,y,Image.GetPixel(Image.Width-1-x,Image.Height-1-y));

                                    }
                            }
                        
                
                    break;

                    case 270:
                            result = new Bitmap (Image.Height,Image.Width);
                            for(int y = 0; y < result.Height; y++) { 
                                    for(int x = 0; x < result.Width; x++) {
                                        result.SetPixel(x,y,Image.GetPixel(Image.Height-1-y,Image.Width-1-x));
                                    }
                            }
                
                    break;


                    default:

                    break;
            }
        

           return result;
        }

        private Int32[] calcularNuevosPixelesRotacion(Int32 antiguoPixelX, Int32 antiguoPixelY, Int32 puntoAnclajeX, Int32 puntoAnclajeY, Double angulo)
        { //Pasar el angulo en radianes
             Int32[] nuevosPixel= new Int32[2];
             nuevosPixel[0] = Convert.ToInt32(puntoAnclajeX + 
                 ((antiguoPixelX-puntoAnclajeX)*Math.Cos(angulo)) - ((antiguoPixelY - puntoAnclajeY)*Math.Sin(angulo)));
             nuevosPixel[1] = Convert.ToInt32(puntoAnclajeY + 
                 ((antiguoPixelX-puntoAnclajeX)*Math.Sin(angulo)) + ((antiguoPixelY - puntoAnclajeY)*Math.Cos(angulo)));

            return nuevosPixel;
        }

        private bool estaContenidoEn(Int32 x, Int32 y,Int32[] UL, Int32[] UR, Int32[] DL,Int32[] DR)
        { //TODO: Comprobar que en todos los casos el pixel x,y se encuentra dentro del recinto formado por las rectas que unen los 4 puntos
        
            Int32 aY = (DL[1]-UL[1]);
            Int32 aX = (DL[0]-UL[0]);
            //Claramente separado
            Int32 uY = (UR[1] - UL[1]);
            Int32 uX = (UR[0] - UL[0]);
            Double constanteArriba = (uX * UL[1]) - (uY * UL[0]);
            Double rectaArriba = (x * uY) + constanteArriba - (uX * y);


            Double constante = (aX*UL[1])-(aY*UL[0]);

            Double rectaIzquierda = (x*aY) + constante - (aX*y);

            Int32 bY = (DR[1]-UR[1]);
            Int32 bX = (DR[0]-UR[0]);

            Double consta = (bX*UR[1])-(bY*UR[0]);

            Double rectaDerecha = (x*bY) + consta - (bX*y);

            if( (rectaIzquierda >= 0) && (rectaDerecha <= 0 && rectaArriba <= 0)){
                    return true;
            }
            return false;
        
        }

        private Int32[] calcularViejosPixelesRotacion(Int32 nuevoPixelX,Int32 nuevoPixelY,Int32 puntoAnclajeX,Int32  puntoAnclajeY,Double angulo){ //Pasar el angulo en radianes
            Int32[] viejosPixels= new Int32[2];
            viejosPixels[0] = Convert.ToInt32(puntoAnclajeX + ((nuevoPixelX-puntoAnclajeX)*Math.Cos(angulo)) + ((nuevoPixelY - puntoAnclajeY)*Math.Sin(angulo)));
            viejosPixels[1] = Convert.ToInt32(puntoAnclajeY - ((nuevoPixelX-puntoAnclajeX)*Math.Sin(angulo)) + ((nuevoPixelY - puntoAnclajeY)*Math.Cos(angulo)));

            return viejosPixels;
        }       
        public Bitmap rotarInterpolar(Bitmap image, Int32 puntoAnclajeX, Int32 puntoAnclajeY, Int32 angulo){ //Pasar el angulo en radianes

                Bitmap result = null;
                Double angle = (angulo * Math.PI) / 180;
                List<Int32> posiblesExtremosX = new List<Int32>();
                List<Int32> posiblesExtremosY = new List<Int32>();

                Int32[] aux_UL = calcularNuevosPixelesRotacion(0, 0, puntoAnclajeX, puntoAnclajeY, angle);
                Int32[] aux_UR = calcularNuevosPixelesRotacion(image.Width-1, 0, puntoAnclajeX, puntoAnclajeY, angle);
                Int32[] aux_DL = calcularNuevosPixelesRotacion(0, image.Height-1, puntoAnclajeX, puntoAnclajeY, angle);
                Int32[] aux_DR = calcularNuevosPixelesRotacion(image.Width-1, image.Height-1, puntoAnclajeX, puntoAnclajeY, angle);

                posiblesExtremosX.Add(aux_UL[0]);
                posiblesExtremosY.Add(aux_UL[1]);
                posiblesExtremosX.Add(aux_UR[0]);
                posiblesExtremosY.Add(aux_UR[1]);
                posiblesExtremosX.Add(aux_DL[0]);
                posiblesExtremosY.Add(aux_DL[1]);
                posiblesExtremosX.Add(aux_DR[0]);
                posiblesExtremosY.Add(aux_DR[1]);

                Int32 maximoX = posiblesExtremosX.Max();
                Int32 maximoY = posiblesExtremosY.Max();
                Int32 minimoX = posiblesExtremosX.Min();
                Int32 minimoY = posiblesExtremosY.Min();
                result = new Bitmap(maximoX-minimoX + 2, maximoY - minimoY + 2);
        
                Int32 desplazamientoY = minimoY;

                Int32 desplazamientoX;

                if(minimoX < 0)
                {
                    desplazamientoX = minimoX-1;
                }else
                {
                    desplazamientoX = 0;
                }

                for(Int32 y = 0; y < result.Height; y++) { 
                        for(Int32 x = 0; x < result.Width; x++){

                                //Si esta contenido 
                                if(estaContenidoEn(x+desplazamientoX, y+desplazamientoY, aux_UL, aux_UR, aux_DL, aux_DR)){

                                        Int32[] viejosPixeles = calcularViejosPixelesRotacion(x+desplazamientoX, y+desplazamientoY, puntoAnclajeX, puntoAnclajeY, angle);
                                        Int32[] viejosPixelesRedondeado = new Int32[2];
                                        viejosPixelesRedondeado[0] = viejosPixeles[0];
                                        viejosPixelesRedondeado[1] = viejosPixeles[1];

                                        Int32 pesoX = viejosPixeles[0] - viejosPixelesRedondeado[0];
		                                Int32 pesoY = viejosPixeles[1] - viejosPixelesRedondeado[1];
                                        if(viejosPixeles[0] < image.Width && viejosPixeles[1] < image.Height )
                                            result.SetPixel(x, y, image.GetPixel(Math.Abs(viejosPixeles[0]), Math.Abs(viejosPixeles[1])));
		                                   
                            }

                        }
                }

                return result;
        }
    }  
 
}
