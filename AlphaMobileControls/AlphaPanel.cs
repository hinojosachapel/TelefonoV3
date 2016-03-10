using System;
using System.Windows.Forms;


namespace AlphaMobileControls
{
	/// <summary>
	/// This Panel is able to handle alpha channel for its child controls
	/// inheriting from AlphaControl.
	/// </summary>
	public partial class AlphaPanel : Panel
	{
		private AlphaContainer _alphaManager;


		/// <summary>
		/// Default constructor.
		/// </summary>
		public AlphaPanel()
        {
            // Instantiate before the components to handle events triggered by InitializeComponent
            _alphaManager = new AlphaContainer(this);

            InitializeComponent();
		}


		protected override void OnResize(EventArgs e)
		{
            _alphaManager.OnResize(e);
			base.OnResize(e);
		}

		protected override void OnPaintBackground(PaintEventArgs e)
		{
			// Prevent flicker, we will take care of the background in OnPaint()
		}

		protected override void OnPaint(PaintEventArgs e)
		{
            _alphaManager.OnPaint(e);
		}
	}
}
