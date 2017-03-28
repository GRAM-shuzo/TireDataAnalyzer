using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
namespace CustomTrackBar
{
    public class DoubleTrackBar : Control
    {
        [Browsable(true)]
        public int NumberTicks
        {
            get
            {
                return numberTicksUser;
            }
            set
            {
                if(value < 0)
                {
                    return;
                }
                else
                {
                    numberTicksUser = value;
                }
                SetupTrackBar();
            }
        }

        [Browsable(true)]
        public double Max
        {
            get
            {
                return max;
            }
            set
            {
                max = value;
                SetupTrackBar();
            }

        }

        [Browsable(true)]
        public double Min
        {
            get
            {
                return min;
            }
            set
            {
                min = value;
                SetupTrackBar();
            }
        }
        [Browsable(false)]
        public double valueL
        {
            get
            {
                return (Max - Min) * currentTickPositionL / (numberTicks - 1)  + Min;
            }
            set
            {
                currentTickPositionL = (int)((value - Min)/ (Max - Min) * (numberTicks - 1));
                if (currentTickPositionL < 0) currentTickPositionL = 0;
                if (currentTickPositionL > numberTicks - 1) currentTickPositionL = numberTicks - 1;
                SetupTrackBar();
            }
        }
        [Browsable(false)]
        public double valueR
        {
            get
            {
                return (Max - Min) * currentTickPositionR / (numberTicks - 1)  + Min;
            }
            set
            {
                currentTickPositionR = (int)((value - Min) / (Max - Min) * (numberTicks - 1));
                if (currentTickPositionR < 0) currentTickPositionR = 0;
                if (currentTickPositionR > numberTicks - 1) currentTickPositionR = numberTicks - 1;
                SetupTrackBar();
            }
        }

        public double valueMean
        {
            get
            {
                return (valueL + valueR) / 2;
            }
        }

        public bool ThumbClicked
        {
            get { return thumbClickedL || thumbClickedR; }
        }


        public event EventHandler ValueChanged;

        private double max = 100;
        private double min = -100;
        private int numberTicks = 10;
        private int numberTicksUser = 10;
        private Rectangle trackRectangle = new Rectangle();
        private Rectangle ticksRectangle = new Rectangle();
        private Rectangle thumbRectangleL = new Rectangle();
        private Rectangle thumbRectangleR = new Rectangle();
        private Rectangle barRectangleC = new Rectangle();
        private int currentTickPositionL = 0;
        private int currentTickPositionR = 9;
        private float tickSpace = 0;
        private bool thumbClickedL = false;
        private bool thumbClickedR = false;
        private TrackBarThumbState thumbStateL = TrackBarThumbState.Normal;
        private TrackBarThumbState thumbStateR = TrackBarThumbState.Normal;

        public DoubleTrackBar()
        {
            this.Size = new Size(300, 50);
            this.DoubleBuffered = true;
            // Calculate the initial sizes of the bar, 
            // thumb and ticks.
            SetupTrackBar();
        }

        // Calculate the sizes of the bar, thumb, and ticks rectangle.
        private void SetupTrackBar()
        {
            if (!TrackBarRenderer.IsSupported)
                return;

            using (Graphics g = this.CreateGraphics())
            {
                // Calculate the size of the track bar.
                trackRectangle.X = ClientRectangle.X + 2;
                trackRectangle.Y = ClientRectangle.Y + 8;
                trackRectangle.Width = ClientRectangle.Width - 4;
                trackRectangle.Height = 4;

                // Calculate the size of the rectangle in which to 
                // draw the ticks.
                ticksRectangle.X = trackRectangle.X +3;
                ticksRectangle.Y = trackRectangle.Y - 8;
                ticksRectangle.Width = trackRectangle.Width -6;
                ticksRectangle.Height = 4;

                if (numberTicksUser == 0)
                {
                    currentTickPositionL = currentTickPositionL * (ticksRectangle.Width-1) / (numberTicks-1);
                    currentTickPositionR = currentTickPositionR * (ticksRectangle.Width - 1) / (numberTicks - 1);
                    numberTicks = ticksRectangle.Width;
                }
                else
                {
                    currentTickPositionL = currentTickPositionL * (numberTicksUser - 1) / (numberTicks - 1);
                    currentTickPositionR = currentTickPositionR * (numberTicksUser - 1) / (numberTicks - 1);
                    numberTicks = numberTicksUser;
                }

                tickSpace = ((float)ticksRectangle.Width-1) /
                    ((float)numberTicks-1);

                // Calculate the size of the thumb.
                thumbRectangleL.Size =
                    TrackBarRenderer.GetTopPointingThumbSize(g,
                    thumbStateL);

                thumbRectangleL.X = CurrentTickXCoordinate(currentTickPositionL);
                thumbRectangleL.Y = trackRectangle.Y - 8;

                

                // Calculate the size of the thumb.
                thumbRectangleR.Size =
                    TrackBarRenderer.GetTopPointingThumbSize(g,
                    thumbStateR);

                thumbRectangleR.X = CurrentTickXCoordinate(currentTickPositionR);
                thumbRectangleR.Y = trackRectangle.Y - 8;

                barRectangleC.X = (thumbRectangleR.X + thumbRectangleL.X + thumbRectangleL.Width) / 2;
                barRectangleC.Y = thumbRectangleR.Y;
                barRectangleC.Width = 1;
                barRectangleC.Height = thumbRectangleR.Height;

                if(ValueChanged != null )ValueChanged(this, new EventArgs());
            }
        }

        private int CurrentTickXCoordinate(int Potsition)
        {
            if (tickSpace == 0)
            {
                return 0;
            }
            else
            {
                return (int)(tickSpace * Potsition);
            }
        }

        private int nearPosition(int X)
        {
            int near = X / (int)Math.Round(tickSpace);
            if (near < 0) near = 0;
            if (near > numberTicks-1) near = numberTicks-1;
            return near;
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            SetupTrackBar();
            base.OnSizeChanged(e);
        }

        // Draw the track bar.
        protected override void OnPaint(PaintEventArgs e)
        {
            
            TrackBarRenderer.DrawHorizontalTrack(e.Graphics, trackRectangle);
            TrackBarRenderer.DrawTopPointingThumb(e.Graphics, thumbRectangleL, thumbStateL);
            TrackBarRenderer.DrawTopPointingThumb(e.Graphics, thumbRectangleR, thumbStateR);
            e.Graphics.FillRectangle(Brushes.OrangeRed, barRectangleC);
            if (numberTicksUser != 0 )
                TrackBarRenderer.DrawHorizontalTicks(e.Graphics, ticksRectangle, numberTicks, EdgeStyle.Raised);
        }

        // Determine whether the user has clicked the track bar thumb.
        protected override void OnMouseDown(MouseEventArgs e)
        {
            if (!TrackBarRenderer.IsSupported)
                return;

            if (this.thumbRectangleL.Contains(e.Location))
            {
                thumbClickedL = true;
                thumbStateL = TrackBarThumbState.Pressed;
            }
            if (this.thumbRectangleR.Contains(e.Location))
            {
                thumbClickedR = true;
                thumbStateR = TrackBarThumbState.Pressed;
            }
            this.Invalidate();
        }

        // Redraw the track bar thumb if the user has moved it.
        protected override void OnMouseUp(MouseEventArgs e)
        {
            if (!TrackBarRenderer.IsSupported)
                return;

            if (thumbClickedL == true)
            {
                if (e.Location.X > trackRectangle.X &&
                    e.Location.X < (trackRectangle.X +
                    trackRectangle.Width - thumbRectangleL.Width))
                {
                    thumbClickedL = false;
                    thumbStateL = TrackBarThumbState.Hot;
                    this.Invalidate();
                }

                thumbClickedL = false;
                if (ValueChanged != null) ValueChanged(this, new EventArgs());
            }
            if (thumbClickedR == true)
            {
                if (e.Location.X > trackRectangle.X &&
                    e.Location.X < (trackRectangle.X +
                    trackRectangle.Width - thumbRectangleR.Width))
                {
                    thumbClickedR = false;
                    thumbStateR = TrackBarThumbState.Hot;
                    this.Invalidate();
                }

                thumbClickedR = false;
                if (ValueChanged != null) ValueChanged(this, new EventArgs());
            }
        }

        // Track cursor movements.
        protected override void OnMouseMove(MouseEventArgs e)
        {
            if (!TrackBarRenderer.IsSupported)
                return;

            // The user is moving the thumb.
            if (thumbClickedL == true)
            {
                // Track movements to the next tick to the right, if 
                // the cursor has moved halfway to the next tick.
                /*
                if (currentTickPositionL < numberTicks - 1 &&
                    e.Location.X > CurrentTickXCoordinate(currentTickPositionL) + (int)(tickSpace) &&
                    currentTickPositionL < currentTickPositionR-1
                    )
                {
                    currentTickPositionL++;
                }

                // Track movements to the next tick to the left, if 
                // cursor has moved halfway to the next tick.
                else if (currentTickPositionL > 0 &&
                    e.Location.X < CurrentTickXCoordinate(currentTickPositionL) - (int)(tickSpace / 2)
                    )
                {
                    currentTickPositionL--;
                }
                

                thumbRectangleL.X = CurrentTickXCoordinate(currentTickPositionL);
                */
                currentTickPositionL = nearPosition(e.Location.X);
                if (currentTickPositionL >= currentTickPositionR) currentTickPositionL = currentTickPositionR - 1;
                thumbRectangleL.X = CurrentTickXCoordinate(currentTickPositionL);
                if (ValueChanged != null) ValueChanged(this, new EventArgs());
            }
            else if (thumbClickedR == true)
            {
                // Track movements to the next tick to the right, if 
                // the cursor has moved halfway to the next tick.
                /*if (currentTickPositionR < numberTicks - 1 &&
                    e.Location.X > CurrentTickXCoordinate(currentTickPositionR) + (int)(tickSpace)
                    )
                {
                    currentTickPositionR++;
                }

                // Track movements to the next tick to the left, if 
                // cursor has moved halfway to the next tick.
                else if (currentTickPositionR > 0 &&
                    e.Location.X < CurrentTickXCoordinate(currentTickPositionR) - (int)(tickSpace / 2) &&
                    currentTickPositionL < currentTickPositionR - 1
                    )
                {
                    currentTickPositionR--;
                }*/
                currentTickPositionR = nearPosition(e.Location.X);
                if (currentTickPositionL >= currentTickPositionR) currentTickPositionR = currentTickPositionL + 1;
                thumbRectangleR.X = CurrentTickXCoordinate(currentTickPositionR);
                if (ValueChanged != null) ValueChanged(this, new EventArgs());
            }
            
            // The cursor is passing over the track.
            else
            {
                thumbStateL = thumbRectangleL.Contains(e.Location) ?
                    TrackBarThumbState.Hot : TrackBarThumbState.Normal;
                thumbStateR = thumbRectangleR.Contains(e.Location) ?
                    TrackBarThumbState.Hot : TrackBarThumbState.Normal;
            }
            barRectangleC.X = (thumbRectangleL.X + thumbRectangleR.X + thumbRectangleL.Width) / 2;
            Invalidate();
        }
    }
}
