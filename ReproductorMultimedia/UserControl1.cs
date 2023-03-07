using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ReproductorMultimedia
{
    public partial class ReproductorMultimedia : UserControl
    {
        public ReproductorMultimedia()
        {
            InitializeComponent();
        }
        private int segundos = 0;
        public int Segundos
        {
            get => segundos;
            set
            {
                if (value < 0)
                {
                    throw new ArgumentException();
                }

                if (segundos >= 59)
                {
                    segundos = value % 60;
                    DesbordaTiempo?.Invoke(this, EventArgs.Empty);
                }
                else
                {
                    segundos = value;

                }
            }
        }

        private int minutos = 0;


        public int Minutos
        {
            get => minutos;
            set
            {
                if (value < 0)
                {
                    throw new ArgumentException();
                }
                if (value > 59)
                {
                    minutos = 0;
                }
                else
                {
                    minutos = value;
                }
            }
        }

        public string TextLbl
        {
            set
            {
                lblTime.Text = value;
            }

            get
            {
                return lblTime.Text;
            }
        }

        public bool Encendido
        {
            set
            {
                Encendido = value;
            }

            get
            {
                return Encendido;
            }
        }

        public bool IsPause
        { 
            get 
            {
               return btnPlay.Text == "Play";
            }
        }

        public event EventHandler PlayClick;

        public event EventHandler DesbordaTiempo;



        private void ChangeText(object sender, EventArgs e)
        {
            btnPlay.Text = IsPause ? "Pause" : "Play";
            PlayClick?.Invoke(this, EventArgs.Empty);
        }
    }
}

