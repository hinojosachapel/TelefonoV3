// Cómo hacer una llamada telefónica en Windows Mobile con C#
// Cómo abrir el menú Inicio en Windows Mobile con C#
// Cómo usar imágenes y botones con fondo transparente en Windows Mobile con C#

#region License

/* Copyright (c) 2011 Rubén Hinojosa Chapel
 * 
 * Permission is hereby granted, free of charge, to any person obtaining a copy 
 * of this software and associated documentation files (the "Software"), to 
 * deal in the Software without restriction, including without limitation the 
 * rights to use, copy, modify, merge, publish, distribute, sublicense, and/or 
 * sell copies of the Software, and to permit persons to whom the Software is 
 * furnished to do so, subject to the following conditions:
 * 
 * The above copyright notice and this permission notice shall be included in 
 * all copies or substantial portions of the Software. 
 * 
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR 
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, 
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE 
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER 
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, 
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN 
 * THE SOFTWARE.
 */

#endregion

#region Contact

/*
 * Rubén Hinojosa Chapel
 * Web: www.hinojosachapel.com
 */

#endregion

#region Using directives

using System;
using System.Windows.Forms;
using Microsoft.WindowsMobile.Telephony;
using System.Reflection;
using AlphaMobileControls;

#endregion

namespace RH.MobilePhone
{
    public partial class Form1 :  AlphaMobileControls.AlphaForm
    {
        public Form1()
        {
            InitializeComponent();

            //Inicialización de los controles de AlphaMobileControls
            pboxWallPaper.Image = LoadImage("bkimage.png");

            pboxTopBar.Image = LoadImage("topbar.png");
            pboxBottomBar.Image = LoadImage("bottombar.png");

            atxtboxPhoneNumber.Image = LoadImage("textbox.png");

            abtnInicio.BackgroundImage = LoadImage("start.png");
            abtnInicio.ActiveBackgroundImage = LoadImage("start-down.png");

            abtn1.BackgroundImage = LoadImage("boton1.png");
            abtn1.ActiveBackgroundImage = LoadImage("boton1-down.png");

            abtn2.BackgroundImage = LoadImage("boton2.png");
            abtn2.ActiveBackgroundImage = LoadImage("boton2-down.png");

            abtn3.BackgroundImage = LoadImage("boton3.png");
            abtn3.ActiveBackgroundImage = LoadImage("boton3-down.png");

            abtn4.BackgroundImage = LoadImage("boton4.png");
            abtn4.ActiveBackgroundImage = LoadImage("boton4-down.png");

            abtn5.BackgroundImage = LoadImage("boton5.png");
            abtn5.ActiveBackgroundImage = LoadImage("boton5-down.png");

            abtn6.BackgroundImage = LoadImage("boton6.png");
            abtn6.ActiveBackgroundImage = LoadImage("boton6-down.png");

            abtn7.BackgroundImage = LoadImage("boton7.png");
            abtn7.ActiveBackgroundImage = LoadImage("boton7-down.png");

            abtn8.BackgroundImage = LoadImage("boton8.png");
            abtn8.ActiveBackgroundImage = LoadImage("boton8-down.png");

            abtn9.BackgroundImage = LoadImage("boton9.png");
            abtn9.ActiveBackgroundImage = LoadImage("boton9-down.png");

            abtn0.BackgroundImage = LoadImage("boton0.png");
            abtn0.ActiveBackgroundImage = LoadImage("boton0-down.png");

            abtnDel.BackgroundImage = LoadImage("delete.png");
            abtnDel.ActiveBackgroundImage = LoadImage("delete-down.png");

            abtnCall.BackgroundImage = LoadImage("Phone.png");
            abtnCall.ActiveBackgroundImage = LoadImage("Phone-down.png");
        }

        private void menuItem1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            if (txtBoxPhoneNumber.Text.Length > 0)
            {
                txtBoxPhoneNumber.Text =
                    txtBoxPhoneNumber.Text.Substring(0, txtBoxPhoneNumber.Text.Length - 1);
                txtBoxPhoneNumber.Select(txtBoxPhoneNumber.TextLength, 1);
                txtBoxPhoneNumber.Focus();
            }
        }

        private void AddDigit(char digit)
        {
            if (txtBoxPhoneNumber.TextLength < txtBoxPhoneNumber.MaxLength)
            {
                txtBoxPhoneNumber.Text += digit;
                txtBoxPhoneNumber.Select(txtBoxPhoneNumber.TextLength, 1);
                txtBoxPhoneNumber.Focus();
            }
        }

        private void btn1_Click(object sender, EventArgs e)
        {
            AddDigit('1');
        }

        private void btn2_Click(object sender, EventArgs e)
        {
            AddDigit('2');
        }

        private void btn3_Click(object sender, EventArgs e)
        {
            AddDigit('3');
        }

        private void btn4_Click(object sender, EventArgs e)
        {
            AddDigit('4');
        }

        private void btn5_Click(object sender, EventArgs e)
        {
            AddDigit('5');
        }

        private void btn6_Click(object sender, EventArgs e)
        {
            AddDigit('6');
        }

        private void btn7_Click(object sender, EventArgs e)
        {
            AddDigit('7');
        }

        private void btn8_Click(object sender, EventArgs e)
        {
            AddDigit('8');
        }

        private void btn9_Click(object sender, EventArgs e)
        {
            AddDigit('9');
        }

        private void btn0_Click(object sender, EventArgs e)
        {
            AddDigit('0');
        }

        /// <summary>
        /// Ejecuta una llamada a un número de teléfono
        /// </summary>
        private void btnCall_Click(object sender, EventArgs e)
        {
            if (txtBoxPhoneNumber.Text == String.Empty)
            {
                MessageBox.Show("No existe ningún número de teléfono al cual llamar",
                    "Información...", MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1);
                return;
            }

            Phone p = new Phone();
            p.Talk(txtBoxPhoneNumber.Text);
        }

        #region Abrir menú Inicio

        [System.Runtime.InteropServices.DllImport("coredll.dll", EntryPoint = "keybd_event", SetLastError = true)]
        internal static extern void keybd_event(byte bVk, byte bScan, int dwFlags, int dwExtraInfo);

        const int VK_LWIN = 0x5B;
        const int KEYEVENTF_KEYUP = 0x2;
        const int KEYEVENTF_KEYDOWN = 0x0;
        
        /// <summary>
        /// Abre el menú Inicio
        /// </summary>
        private void btnInicio_Click(object sender, EventArgs e)
        {
            keybd_event(VK_LWIN, 0, KEYEVENTF_KEYDOWN, 0);
            keybd_event(VK_LWIN, 0, KEYEVENTF_KEYUP, 0);

            //Código alternativo
            //keybd_event((byte)Keys.LWin, 0, KEYEVENTF_KEYDOWN, 0);
            //keybd_event((byte)Keys.LWin, 0, KEYEVENTF_KEYUP, 0);
        }

        #endregion

        
        public static AlphaImage LoadImage(string FileName)
        {
            string path = System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().GetName().CodeBase);
            return AlphaImage.CreateFromFile(String.Format(@"{0}\Images\{1}", path, FileName));
        }
    }
}