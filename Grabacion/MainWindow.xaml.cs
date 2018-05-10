using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using NAudio;
using NAudio.Wave;
using NAudio.Dsp;
using System.Threading;
using System.Diagnostics;


namespace Grabacion
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        WaveIn wavein;
        WaveFormat formato;
        WaveFileWriter writer;
        AudioFileReader reader;
        WaveOutEvent waveOut;


        public MainWindow()
        {
            InitializeComponent();
        }
        


        private void btniniciar_Click(object sender, RoutedEventArgs e)
        {
            
            wavein = new WaveIn();
            wavein.WaveFormat = new WaveFormat(44100, 16, 1);
            formato = wavein.WaveFormat;

            wavein.DataAvailable += OnDaraAvailable;
            wavein.RecordingStopped += OnRectodingStopped;
            writer =
                new WaveFileWriter("sonido.wav", formato);

            wavein.StartRecording();
            

        }

        void OnRectodingStopped(object sender, StoppedEventArgs e)
        {
            writer.Dispose();
        }

        void OnDaraAvailable(object sender, WaveInEventArgs e)
        {
            byte[] buffer = e.Buffer;
            int bytesGrabados = e.BytesRecorded;

            double acumulador = 0;
            double nummuestras = bytesGrabados / 2;
            int exponente = 1;
            int numeroMuestrasComplejas = 0;
            int bitsMaximos = 0;
            do //1,200
            {
                bitsMaximos = (int)Math.Pow(2, exponente);
                exponente++;

            } while (bitsMaximos < nummuestras);

            //bitsMaximos = 2048
            //exponente = 12

            //numeroMuestrasComplejas = 1024
            //exponente = 10

            exponente -= 2;
            numeroMuestrasComplejas = bitsMaximos / 2;

            Complex[] muestrasComplejas =
                new Complex[numeroMuestrasComplejas];

            for (int i = 0; i < bytesGrabados; i += 2)
            {
                short muestra = (short)(buffer[i + 1] << 8 | buffer[i]);
                //lblmuestras.TextInput = muestra.ToString();
                //slbvolumen.Value = muestra;

                float muestra32bits = (float)muestra / 32768.0f;
                slbvolumen.Value = Math.Abs(muestra32bits);
                if (i / 2 < numeroMuestrasComplejas)
                {
                    muestrasComplejas[i / 2].X = muestra32bits;
                }



                /*acumulador += muestra;
                nummuestras++;*/
            }
            // double promedio = acumulador / nummuestras;
            // slbvolumen.Value = promedio;
            //writer.Write(buffer, 0, bytesGrabados);
            FastFourierTransform.FFT(true, exponente, muestrasComplejas);
            float[] valoresAbsolutos =
                new float[muestrasComplejas.Length];

            for (int i = 0; i < muestrasComplejas.Length; i++)
            {
                valoresAbsolutos[i] = (float)
                Math.Sqrt((muestrasComplejas[i].X * muestrasComplejas[i].X) +
                (muestrasComplejas[i].Y * muestrasComplejas[i].Y));
            }
            int indiceMaximo =
                valoresAbsolutos.ToList().IndexOf(
                    valoresAbsolutos.Max());
            float frecuenciaFundamental =
                (float)(indiceMaximo * wavein.WaveFormat.SampleRate) / 
                (float) valoresAbsolutos.Length;
            lblFrecuencia.Text = frecuenciaFundamental.ToString();


            if (frecuenciaFundamental > 100 && frecuenciaFundamental < 200)
            {
                Stopwatch sw = Stopwatch.StartNew();
                for (int index = 0; index < 3; index++)
                {

                    lblFrecuencia.Text = "A";
                }
                sw.Stop();
            }
            else if (frecuenciaFundamental > 200 && frecuenciaFundamental < 300)
            {
                Stopwatch sw = Stopwatch.StartNew();
                for (int index = 0; index < 3; index++)
                {

                    lblFrecuencia.Text = "B";
                }
                sw.Stop();
            }
            else if (frecuenciaFundamental > 300 && frecuenciaFundamental < 400)
            {
                Stopwatch sw = Stopwatch.StartNew();
                for (int index = 0; index < 3; index++)
                {

                    lblFrecuencia.Text = "C";
                }
                sw.Stop();
            }
            else if (frecuenciaFundamental > 400 && frecuenciaFundamental < 500)
            {
                Stopwatch sw = Stopwatch.StartNew();
                for (int index = 0; index < 3; index++)
                {

                    lblFrecuencia.Text = "D";
                }
                sw.Stop();
            }
            else if (frecuenciaFundamental > 500 && frecuenciaFundamental < 600)
            {
                Stopwatch sw = Stopwatch.StartNew();
                for (int index = 0; index < 3; index++)
                {

                    lblFrecuencia.Text = "F";
                }
                sw.Stop();
            }
            else if (frecuenciaFundamental > 600 && frecuenciaFundamental < 700)
            {
                Stopwatch sw = Stopwatch.StartNew();
                for (int index = 0; index < 3; index++)
                {

                    lblFrecuencia.Text = "G";
                }
                sw.Stop();
            }
            else if (frecuenciaFundamental > 700 && frecuenciaFundamental < 800)
            {
                Stopwatch sw = Stopwatch.StartNew();
                for (int index = 0; index < 3; index++)
                {

                    lblFrecuencia.Text = "H";
                }
                sw.Stop();
            }
            else if (frecuenciaFundamental > 800 && frecuenciaFundamental < 900)
            {
                Stopwatch sw = Stopwatch.StartNew();
                for (int index = 0; index < 3; index++)
                {

                    lblFrecuencia.Text = "I";
                }
                sw.Stop();
            }
            else if (frecuenciaFundamental > 900 && frecuenciaFundamental < 1000)
            {
                Stopwatch sw = Stopwatch.StartNew();
                for (int index = 0; index < 3; index++)
                {

                    lblFrecuencia.Text = "J";
                }
                sw.Stop();
            }
            else if (frecuenciaFundamental > 1000 && frecuenciaFundamental < 1100)
            {
                Stopwatch sw = Stopwatch.StartNew();
                for (int index = 0; index < 3; index++)
                {

                    lblFrecuencia.Text = "K";
                }
                sw.Stop();
            }
            else if (frecuenciaFundamental > 1100 && frecuenciaFundamental < 1200)
            {
                Stopwatch sw = Stopwatch.StartNew();
                for (int index = 0; index < 3; index++)
                {

                    lblFrecuencia.Text = "L";
                }
                sw.Stop();
            }
            else if (frecuenciaFundamental > 1200 && frecuenciaFundamental < 1300)
            {
                Stopwatch sw = Stopwatch.StartNew();
                for (int index = 0; index < 3; index++)
                {

                    lblFrecuencia.Text = "M";
                }
                sw.Stop();
            }
            else if (frecuenciaFundamental > 1300 && frecuenciaFundamental < 1400)
            {
                Stopwatch sw = Stopwatch.StartNew();
                for (int index = 0; index < 3; index++)
                {

                    lblFrecuencia.Text = "N";
                }
                sw.Stop();
            }
            else if (frecuenciaFundamental > 1400 && frecuenciaFundamental < 1500)
            {
                Stopwatch sw = Stopwatch.StartNew();
                for (int index = 0; index < 3; index++)
                {

                    lblFrecuencia.Text = "O";
                }
                sw.Stop();
            }
            else if (frecuenciaFundamental > 1500 && frecuenciaFundamental < 1600)
            {
                Stopwatch sw = Stopwatch.StartNew();
                for (int index = 0; index < 3; index++)
                {

                    lblFrecuencia.Text = "P";
                }
                sw.Stop();
            }
            else if (frecuenciaFundamental > 1600 && frecuenciaFundamental < 1700)
            {
                Stopwatch sw = Stopwatch.StartNew();
                for (int index = 0; index < 3; index++)
                {

                    lblFrecuencia.Text = "R";
                }
                sw.Stop();
            }
            else if (frecuenciaFundamental > 1700 && frecuenciaFundamental < 1800)
            {
                Stopwatch sw = Stopwatch.StartNew();
                for (int index = 0; index < 3; index++)
                {

                    lblFrecuencia.Text = "S";
                }
                sw.Stop();
            }
            else if (frecuenciaFundamental > 1800 && frecuenciaFundamental < 1900)
            {
                Stopwatch sw = Stopwatch.StartNew();
                for (int index = 0; index < 3; index++)
                {

                    lblFrecuencia.Text = "T";
                }
                sw.Stop();
            }
            else if (frecuenciaFundamental > 1900 && frecuenciaFundamental < 2000)
            {
                Stopwatch sw = Stopwatch.StartNew();
                for (int index = 0; index < 3; index++)
                {

                    lblFrecuencia.Text = "U";
                }
                sw.Stop();
            }
            else if (frecuenciaFundamental > 2000 && frecuenciaFundamental < 2100)
            {
                Stopwatch sw = Stopwatch.StartNew();
                for (int index = 0; index < 3; index++)
                {

                    lblFrecuencia.Text = "V";
                }
                sw.Stop();
            }
            else if (frecuenciaFundamental > 2200 && frecuenciaFundamental < 2300)
            {
                Stopwatch sw = Stopwatch.StartNew();
                for (int index = 0; index < 3; index++)
                {

                    lblFrecuencia.Text = "W";
                }
                sw.Stop();
            }
            else if (frecuenciaFundamental > 2300 && frecuenciaFundamental < 2400)
            {
                Stopwatch sw = Stopwatch.StartNew();
                for (int index = 0; index < 3; index++)
                {

                    lblFrecuencia.Text = "X";
                }
                sw.Stop();
            }
            else if (frecuenciaFundamental > 2400 && frecuenciaFundamental < 2500)
            {
                Stopwatch sw = Stopwatch.StartNew();
                for (int index = 0; index < 3; index++)
                {

                    lblFrecuencia.Text = "Y";
                }
                sw.Stop();
            }
            else if (frecuenciaFundamental > 2500 && frecuenciaFundamental < 2600)
            {
                Stopwatch sw = Stopwatch.StartNew();
                for (int index = 0; index < 3; index++)
                {

                    lblFrecuencia.Text = "Z";
                }
                sw.Stop();
            }
            else if (frecuenciaFundamental > 2600 && frecuenciaFundamental < 2700)
            {
                Stopwatch sw = Stopwatch.StartNew();
                for (int index = 0; index < 3; index++)
                {

                    lblFrecuencia.Text = "A";
                }
                sw.Stop();
            }
            
        }


        

        private void btnfinalizar_Click(object sender, RoutedEventArgs e)
        {
            wavein.StopRecording();
        }

        private void btnReproducir_Click(object sender, RoutedEventArgs e)
        {
            reader = new AudioFileReader("sonido.wav");
            waveOut = new WaveOutEvent();
            waveOut.Init(reader);
            waveOut.Play();
        }

        private void slbvolumen_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {

        }
    }
}
