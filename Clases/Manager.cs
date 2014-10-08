using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
namespace VCPhotoManager.Clases
{
    public class Manager
    {
        public Image changeToGrayScale(Bitmap image)
        {
            Image result = image;
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
            result = aux;
            return result;
        }
    }   
}
