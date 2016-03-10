using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;


namespace AlphaMobileControls
{
    /// <summary>
    /// Helper class for container controls handling alpha channel.
    /// The controlled control must forward the Resize and Paint events.
    /// It uses a double buffer to avoid flickering.
    /// </summary>
    internal class AlphaContainer
    {
        /// <summary> Controlled Control. </summary>
        private Control _control;

        /// <summary> Back buffer used for double buffering. </summary>
        private Bitmap _backBuffer;


        /// <summary>
        /// Constructor, the controlled control must be supplied.
        /// </summary>
        /// <param name="control"></param>
        public AlphaContainer(Control control)
        {
            if (control == null)
                throw new ArgumentNullException("A valid control must be supplied.");

            _control = control;

            CreateBackBuffer();
        }

        private void CreateBackBuffer()
        {
            if (_backBuffer != null)
                _backBuffer.Dispose();

            // The bitmap needs to be created with the 32bpp pixel format for the IImage to do the right thing.
            _backBuffer = new Bitmap(_control.ClientSize.Width, _control.ClientSize.Height, PixelFormat.Format32bppRgb);
        }

        
        /// <summary>
        /// Handles the Resize event to update the back buffer.
        /// </summary>
		public void OnResize(EventArgs e)
        {
            CreateBackBuffer();
		}

        /// <summary>
        /// Handles the Paint event, it is where the magic happens ;o)
        /// </summary>
        public void OnPaint(PaintEventArgs e)
        {
            if (_backBuffer != null)
            {
                // We need a Graphics object on the buffer to get an HDC
                using (Graphics gxBuffer = Graphics.FromImage(_backBuffer))
                {
                    // Since we nop'd OnPaintBackground, take care of it here
                    gxBuffer.Clear(_control.BackColor);

                    Region gxClipBounds = new Region(Rectangle.Ceiling(gxBuffer.ClipBounds));

                    // Iterates the child control list in reverse order
                    // to respect the Z-order
                    for (int i = _control.Controls.Count - 1; i >= 0; i--)
                    {
                        // Handle controls inheriting AlphaControl only
                        AlphaControl ctrl = _control.Controls[i] as AlphaControl;
                        if (ctrl == null)
                            continue;

                        // Something to draw?
                        Rectangle clipRect = Rectangle.Intersect(e.ClipRectangle, ctrl.Bounds);
                        if (clipRect.IsEmpty)
                            continue;

                        // Clip to the control bounds
                        gxBuffer.Clip = new Region(clipRect);

                        // Perform the actual drawing
                        ctrl.DrawInternal(gxBuffer);
                    }

                    // Restore clip bounds
                    gxBuffer.Clip = gxClipBounds;
                }

                // Put the final composed image on screen.
                e.Graphics.DrawImage(_backBuffer, e.ClipRectangle, e.ClipRectangle, GraphicsUnit.Pixel);
            }
            else
            {
                // This should never happen, should it?
                e.Graphics.Clear(_control.BackColor);
            }
        }
    }
}
