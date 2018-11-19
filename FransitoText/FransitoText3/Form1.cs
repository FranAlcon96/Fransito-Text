using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.VisualBasic;

namespace FransitoText3
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private String rutaGuardar="";
        private Boolean busqueda=false;
        /*Proyecto realizado por Fran Alcón de 2º de DAM*/
        
        //Nuevo archivo
        private void menuNuevo_Click(object sender, EventArgs e)
        {
            if (texto.Text.ToString()!="")
            {
                DialogResult result = MessageBox.Show("¿ Desea guardar el archivo", "Nuevo archivo", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Asterisk);
                if (result == DialogResult.Yes)
                {
                    SaveFileDialog saveFileDialog1 = new SaveFileDialog();
                    saveFileDialog1.Title = "Guardar texto en un fichero.";
                    saveFileDialog1.Filter = "txt files (*.txt)|*.txt|rtf files (*.rtf)|*.rtf";
                    saveFileDialog1.DefaultExt = "txt";
                    saveFileDialog1.AddExtension = true;
                    saveFileDialog1.RestoreDirectory = true;
                    //saveFileDialog1.InitialDirectory = @"H:\LO DEL ESCRITORIO";

                    if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                    {
                        this.rutaGuardar = saveFileDialog1.FileName;
                        if (rutaGuardar != "")
                        {
                            StreamWriter fichero = new StreamWriter(this.rutaGuardar);
                            fichero.Write(texto.Text);
                            fichero.Close();
                            MessageBox.Show("Se guardo el archivo: " + saveFileDialog1.FileName);
                            this.rutaGuardar = "";
                        }
                    }
                }
                texto.Text = "";
            }
            else
            {
                MessageBox.Show("No ha escrito nada");
            }
        }

        //Abrir archivo 
        private void abrirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            open.Filter = "txt files (*.txt)|*.txt|rtf files (*.rtf)|*.rtf";
            if (open.ShowDialog() == DialogResult.OK && open.ToString() != " ")
            {
                this.rutaGuardar = open.FileName;
                try
                {
                    using (StreamReader sr = new StreamReader(this.rutaGuardar))
                    {
                        String contenido = sr.ReadToEnd();
                        texto.Text = contenido;
                        this.rutaGuardar = "";
                    }
                }
                catch (Exception excepcion)
                {
                    MessageBox.Show("No se pudo leer el archivo:" + excepcion.Message);
                }
            }
        }

        //Guardar archivo
        private void guardarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            if (this.rutaGuardar=="")
            {
                saveFileDialog1.Title = "Guardar texto en un fichero.";
                saveFileDialog1.Filter = "txt files (*.txt)|*.txt|rtf files (*.rtf)|*.rtf";
                saveFileDialog1.DefaultExt = "txt";
                saveFileDialog1.AddExtension = true;
                saveFileDialog1.RestoreDirectory = true;
                saveFileDialog1.ShowDialog();
                this.rutaGuardar = saveFileDialog1.FileName;
                if (rutaGuardar!="")
                {
                    StreamWriter fichero = new StreamWriter(rutaGuardar);
                    fichero.Write(texto.Text);
                    fichero.Close();
                    MessageBox.Show("Se guardo el archivo: " + this.rutaGuardar);
                }
            }
            else
            {
                StreamWriter fichero = new StreamWriter(rutaGuardar);
                fichero.Write(texto.Text);
                fichero.Close();
                MessageBox.Show("Se guardo el archivo: " + this.rutaGuardar);
            }
        }

        //Salir de la aplicación
        private void salirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Gracias por usar Fransito Text, Adiós!!!");
            this.Close();
        }

        //Cortar texto
        private void cortarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            texto.Cut();
        }

        //Copiar texto
        private void copiarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            texto.Copy();
        }

        //Pegar texto
        private void pegarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            texto.Paste();
        }

        //Buscar texto
        private void buscarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            String busqueda = Microsoft.VisualBasic.Interaction.InputBox("Palabra a buscar: " , "Búsqueda");
            int index = 0;
            String aux =texto.Text;
            texto.Text = "";
            texto.Text = aux;
            this.busqueda=true;
            if (texto.Text.Contains(busqueda))
            {
               
                while (index < texto.Text.LastIndexOf(busqueda))
                {
                    texto.Find(busqueda, index, texto.TextLength, RichTextBoxFinds.None);
                    texto.SelectionBackColor = Color.Red;
                    index = texto.Text.IndexOf(busqueda, index) + 1;
                }

            }
            else
            {
                MessageBox.Show("No existe la cadena.");
            }
        }

        //este método cambia color al usar el teclado después de la búsqueda
        private void cambioFondoBuscar(object sender, EventArgs e)
        {
            if (busqueda)
            {
                String aux = texto.Text;
                texto.Clear();
                texto.Text = aux;
                texto.Select(texto.Text.Length,1);
                busqueda = false;
            }
        }

        //Fuente
        private void fuenteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FontDialog fontDialog1 = new FontDialog();
            DialogResult result = fontDialog1.ShowDialog();
            
            if (result == DialogResult.OK)
            {
                Font font = fontDialog1.Font;
                texto.Font = font;
            }
        }

        //Color de fuente
        private void colorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ColorDialog colorDialog1 = new ColorDialog();
            DialogResult result = colorDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                texto.ForeColor = colorDialog1.Color;
            }
        }

        //Número de la linea
        private void texto_TextChanged(object sender, EventArgs e)
        {
           int numLinea = texto.GetLineFromCharIndex(texto.SelectionStart);
            lineas.Text = "Linea: " + (numLinea + 1);
        }

    }
}

