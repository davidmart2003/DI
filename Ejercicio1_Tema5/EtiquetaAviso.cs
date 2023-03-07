using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ejercicio1_Tema5
{
    public partial class EtiquetaAviso : Control
    {


        //ARREGLAR ESTO
        Cambiar omagwen por Image, OnMousecik crear ek evento clickEnMarca,

        public EtiquetaAviso()
        {
            InitializeComponent();
        }
        private int grosor = 0; //Grosor de las líneas de dibujo
        private int offsetX = 0; //Desplazamiento a la derecha del texto
        private int offsetY = 0; //Desplazamiento hacia abajo del texto
        // Altura de fuente, usada como referencia en varias partes
        private int h = 0;



        private Color initialGradient=Color.Gray;
        public Color InitialGradient
        {
            set
            {
                this.initialGradient = value;
                this.Refresh();
            }
            get
            {
                return this.initialGradient;
            }
        }

        private Color finalGradient=Color.Aquamarine;

        public Color FinalGradient
        {
            set
            {
                this.finalGradient = value;
                this.Refresh();
            }
            get
            {
                return this.finalGradient;
            }
        }

        private bool gradient;
        public bool Gradient
        {
            set
            {
                this.gradient = value;
                this.Refresh();
            }
            get
            {
                return this.gradient;
            }
        }

        private string imagenMarca = "";
        public string ImagenMarca
        {
            set
            {
                this.imagenMarca = value;
            }
            get
            {
                return imagenMarca;
            }
        }


        private eMarca marca = eMarca.NADA;
        [Category("Appearance")]
        [Description("Indica el tipo de marca que aparece junto al texto")]
        public eMarca Marca
        {
            set
            {
                marca = value;
                this.Refresh();
            }
            get
            {
                return marca;
            }
        }



        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics g = e.Graphics;

            grosor = 0; //Grosor de las líneas de dibujo
            offsetX = 0; //Desplazamiento a la derecha del texto
            offsetY = 0; //Desplazamiento hacia abajo del texto
                         // Altura de fuente, usada como referencia en varias partes
            h = this.Font.Height;

            switch (marca)
            {
                case eMarca.CIRCULO:
                    grosor = 20;

                    offsetX = grosor +h;
                    offsetY = grosor;
                    break;
                case eMarca.CRUZ:
                    grosor = 15;
                    offsetX = grosor - 5;
                    offsetY = grosor / 2;

                    break;
                case eMarca.IMAGEN:

                    offsetX = grosor + h;
                    offsetY = grosor / 2;

                    break;
            }

            if (gradient)
            {
                int x, y;
                LinearGradientBrush lgb = new LinearGradientBrush(new Point(0,0),new Point(offsetX+grosor+this.Width,offsetY), InitialGradient, FinalGradient);
                g.DrawLine(new Pen(lgb,(h+grosor+offsetY)*2),0,0,grosor+offsetX+this.Width,0);
                lgb.Dispose();
            }
            //Esta propiedad provoca mejoras en la apariencia o en la eficiencia
            // a la hora de dibujar
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            switch (marca)
            {
                case eMarca.CIRCULO:
                    g.DrawEllipse(new Pen(Color.Green, grosor), grosor, grosor, h, h);

                    break;
                case eMarca.CRUZ:
                    Pen lapiz = new Pen(Color.Red, grosor);
                    g.DrawLine(lapiz, grosor, grosor, h, h);
                    g.DrawLine(lapiz, h, grosor, grosor, h);

                    lapiz.Dispose();
                    break;
                case eMarca.IMAGEN:
                    Image img = pathImagen(imagenMarca);
                    if (img != null)
                    {
                        g.DrawImage(img, grosor, grosor, h, h);
                    }
                    break;
            }


            //Brocha
            SolidBrush b = new SolidBrush(this.ForeColor);
            g.DrawString(this.Text, this.Font, b, offsetX + grosor, offsetY);

            Size tamaño = g.MeasureString(this.Text, this.Font).ToSize();

            this.Size = new Size(tamaño.Width + offsetX + grosor, tamaño.Height + offsetY * 2);
            b.Dispose();
        }

        private Image pathImagen(string path)
        {
            Image img = null;
            try
            {
                img = Image.FromFile(path);
            }
            catch (ArgumentException e)
            {
                Marca = eMarca.NADA;
            }
            catch (System.IO.FileNotFoundException e1)
            {
                Marca = eMarca.NADA;
            }

            return img;
        }

        protected override void OnTextChanged(EventArgs e)
        {
            base.OnTextChanged(e);
            this.Refresh();
        }
        private void EtiquetaAviso_Load(object sender, EventArgs e)
        {

        }

        private void EtiquetaAviso_Load_1(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        protected override void OnMouseClick(MouseEventArgs e)
        {
               if (e.X >= grosor / 2 && e.X <= offsetX + grosor && e.Y >= grosor / 2 && e.Y <= h + offsetY)
            {

                base.OnMouseClick(e);
            }
        }

        private void label1_MouseClick(object sender, MouseEventArgs e)
        {

        }
    }
    public enum eMarca
    {
        NADA,
        CRUZ,
        CIRCULO,
        IMAGEN
    }
}
