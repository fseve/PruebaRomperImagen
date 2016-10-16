using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PruebaRomperImagen
{
    public partial class Form1 : Form
    {
        
        int[] vec = new int[16];

        public Form1()
        {
            InitializeComponent();
        }

        private void btnExaminar_Click(object sender, EventArgs e)
        {
            OpenFileDialog oFD = new OpenFileDialog();
            oFD.Title = "Seleccionar imagen";
            oFD.Filter = "Imágenes|*.jpg;*.gif;*.png;*.bmp|Todos (*.*)|*.*";
            oFD.FileName = txtImagen.Text;
            if (oFD.ShowDialog() == DialogResult.OK)
            {
                txtImagen.Text = oFD.FileName;
                picMain.Image = Image.FromFile(txtImagen.Text);
                picMain.SizeMode = PictureBoxSizeMode.StretchImage;
            }
        }

        private void btnRomper_Click(object sender, EventArgs e)
        {
            LlenarVector();

            // Trocear una imagen en trozos más pequeños
            const int columnas = 4;
            const int filas = 4;
            //
            // El tamaño proporcional del ancho y alto
            // correspondientes a los trozos a usar
            int tamTrozoW = picMain.Image.Width / columnas;
            int tamTrozoH = picMain.Image.Height / filas;
            // El tamaño de cada trozo
            int nW = pic0.Width;
            int nH = pic0.Height;
            // El rectángulo que ocupará cada nuevo trozo
            Rectangle rectDest = new Rectangle(0, 0, nW, nH);
            // Estas variables se usan en el bucle
            Bitmap bmpDest;
            Graphics g;
            Rectangle rectOrig;
            //
            // Array con los pictures que hay en el formulario
            PictureBox[] trozos = { pic0, pic1, pic2, pic3, pic4, pic5, pic6, pic7, pic8, pic9, pic10, pic11, pic12, pic13, pic14, pic15};
            // Para contar cada columna
            int c = 0;
            // La posición X e Y en la imagen original
            int pX = 0, pY = 0;
            for (int i = 0; i < trozos.Length; i++)
            {
                // El trozo de la imagen original
                rectOrig = new Rectangle(pX, pY, tamTrozoW, tamTrozoH);
                // La imagen de destino
                bmpDest = new Bitmap(tamTrozoW, tamTrozoW);
                g = Graphics.FromImage(bmpDest);
                // Obtenemos un trozo de la imagen original
                // y lo dibujamos en la imagen de destino
                g.DrawImage(picMain.Image, rectDest, rectOrig, GraphicsUnit.Pixel);
                // Asignamos la nueva imagen al picture correspondiente
                trozos[vec[i]].Image = bmpDest;
                c += 1;
                pX += tamTrozoW;
                // Cuando hayamos recorrido las columnas,
                // pasamos a la siguiente fila
                if (c >= columnas)
                {
                    c = 0;
                    pX = 0;
                    pY += tamTrozoH;
                }
            }
        }
        
        public int GenerarNumero()
        {
            Random r = new Random();
            return r.Next(0, 16);
        }
        
        public void LlenarVector()
        {
            vec[0] = -1;
            vec[1] = -2;
            vec[2] = -3;
            vec[3] = -4;
            vec[4] = -5;
            vec[5] = -6;
            vec[6] = -7;
            vec[7] = -8;
            vec[8] = -9;
            vec[9] = -10;
            vec[10] = -11;
            vec[11] = -12;
            vec[12] = -13;
            vec[13] = -14;
            vec[14] = -15;
            vec[15] = -16;

            int i = 0;
            
            while (i < vec.Length)
            {
                int num = GenerarNumero();
                if (vec.Where(r => r == num).DefaultIfEmpty(-1).Single() == -1)                
                {
                    vec[i] = num;
                    i++;
                }                
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {            
            for (int i = 0; i < vec.Length; i++)
            {
                MessageBox.Show(vec[i].ToString());
            }
          
        }

    }
}
