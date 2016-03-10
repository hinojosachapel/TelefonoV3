using System.Drawing;


namespace AlphaMobileControls
{
    /// <summary>
    /// Simple Alpha Label control.
    /// </summary>
    public class AlphaLabel : AlphaControl
    {
        private bool _border;

        /// <summary>
        /// Active or not a thin border around this label.
        /// </summary>
        public bool Border
        {
            get { return _border; }
            set
            {
                if (_border == value)
                    return;
                _border = value;
                Refresh();
            }
        }


        #region AlphaControl Members

        /// <summary>
        /// Draws the label.
        /// </summary>
        public override void Draw(Graphics gx)
        {
            Rectangle rect =
                new Rectangle(this.Bounds.X, this.Bounds.Y, this.Bounds.Width - 1, this.Bounds.Height - 1);

            // Draw the border
            if (_border)
            {
                Pen pen = new Pen(this.ForeColor);
                gx.DrawRectangle(pen, rect);
            }

            // Specify a rectangle to activate the line wrapping
            rect.Inflate(-4, -2);
            SolidBrush brush = new SolidBrush(this.ForeColor);
            gx.DrawString(this.Text, this.Font, brush, rect);
        }

        #endregion
    }
}
