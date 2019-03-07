using System;
using SkiaSharp;


        
void Draw(SKCanvas canvas, int width, int height)
{
            
	 SKPaint backPaint = new SKPaint
            {
                Style = SKPaintStyle.Fill,
                Color = SKColors.WhiteSmoke,
            };

            canvas.DrawRect(new SKRect(0, 0, width, height), backPaint);

            canvas.Save();

            canvas.Translate(width / 2, height / 2);
            canvas.Scale(Math.Min(width / 210f, height / 520f));
           

            var rect = new SKRect(-100, -100, 100, 100);

            // Add a buffer for the rectangle
            rect.Inflate(-10, -10);           
            
           

            SKPaint HighlightRangePaint = new SKPaint
            {
                Style = SKPaintStyle.Fill,
                Color = SKColor.Parse("E6F4F7")
            };

            SKPoint center = new SKPoint(0, 0);

            // Draw the range of values
            if (true)
            {
                var rangeStartAngle = AmountToAngle(0);
                var rangeEndAngle = AmountToAngle(100);
                var angleDistance = rangeEndAngle - rangeStartAngle;

                using (SKPath path = new SKPath())
                {
                    path.AddArc(rect, rangeStartAngle, angleDistance);
                    path.LineTo(center);
                    canvas.DrawPath(path, HighlightRangePaint);
                }
            }

            // Draw the main gauge line/arc
            //Sector1
           SKPaint GaugeMainLinePaintP1 = new SKPaint
            {
                IsAntialias  = true,
                Style = SKPaintStyle.Stroke,
                Color = SKColors.Blue,                
                StrokeWidth = 10
            };
            var startAngle = 135;
            var sweepAngle = 67.5f;
            
            using (SKPath path = new SKPath())
            {
                path.AddArc(rect, startAngle, sweepAngle);                
               canvas.DrawPath(path, GaugeMainLinePaintP1);
            }
            
           //Sector2
           SKPaint GaugeMainLinePaintP2 = new SKPaint
            {
                IsAntialias  = true,
                Style = SKPaintStyle.Stroke,
                Color = SKColors.Green,                
                StrokeWidth = 10
            };
            
            var startAngleP2 = 202.5f;
            using (SKPath path = new SKPath())
            {
                path.AddArc(rect, startAngleP2, sweepAngle);                
                canvas.DrawPath(path, GaugeMainLinePaintP2);
            }
            
            //Sector3
           SKPaint GaugeMainLinePaintP3 = new SKPaint
            {
                IsAntialias  = true,
                Style = SKPaintStyle.Stroke,
                Color = SKColors.Orange,                
                StrokeWidth = 10
            };
            
            var startAngleP3 = 270f;
            using (SKPath path = new SKPath())
            {
                path.AddArc(rect, startAngleP3, sweepAngle);                
                canvas.DrawPath(path, GaugeMainLinePaintP3);
            }
            
            //Sector 4
            
             SKPaint GaugeMainLinePaintP4 = new SKPaint
            {
                IsAntialias  = true,
                Style = SKPaintStyle.Stroke,
                Color = SKColors.Red,                
                StrokeWidth = 10
            };
            
            var startAngleP4 = 337.5f;
            using (SKPath path = new SKPath())
            {
                path.AddArc(rect, startAngleP4, sweepAngle);                
                canvas.DrawPath(path, GaugeMainLinePaintP4);
            }
            
            //Draw Needle
            DrawNeedle(canvas,ValueToAngle(50));
            
            //Draw Screw
             SKPaint NeedleScrewPaint = new SKPaint()
            {
                IsAntialias = true,
                Shader = SKShader.CreateRadialGradient(center, width / 60, new SKColor[]
                { new SKColor(171, 171, 171), SKColors.White }, new float[] { 0.05f, 0.9f }, SKShaderTileMode.Mirror)
            };

            canvas.DrawCircle(center, width / 60, NeedleScrewPaint);
            
            SKPaint paint = new SKPaint
            {
                IsAntialias = true,
                Style = SKPaintStyle.Stroke,
                Color = new SKColor(81, 84, 89).WithAlpha(100),
                StrokeWidth = 1f
            };

            canvas.DrawCircle(center, width / 60, paint);

            // Draw the Units of Measurement Text on the display
            SKPaint textPaint = new SKPaint
            {
                IsAntialias = true,
                Color = SKColors.Black
            };

            float textWidth = textPaint.MeasureText("m/s");
            textPaint.TextSize = 12f;

            SKRect textBounds = SKRect.Empty;
            textPaint.MeasureText("m/s", ref textBounds);

            float xText = -1 * textBounds.MidX;
            float yText = 95 - textBounds.Height;

            // And draw the text
            canvas.DrawText("m/s", xText, yText, textPaint);

            // Draw the Value on the display
            var valueText = 50.ToString("F1");
            float valueTextWidth = textPaint.MeasureText(valueText);
            textPaint.TextSize = 35f;

            textPaint.MeasureText(valueText, ref textBounds);

            xText = -1 * textBounds.MidX;
            yText = 85 - textBounds.Height;

            // And draw the text
            canvas.DrawText(valueText, xText, yText, textPaint);

            canvas.Restore();
            
}
void DrawNeedle(SKCanvas canvas,float angle)
        {
            canvas.Save();
            canvas.RotateDegrees(angle);
            float needleWidth = 6f;
            float needleHeight = 76f;
            float x = 0f, y = 0f;

            SKPaint paint = new SKPaint
            {
                IsAntialias = true,
                Color = SKColors.Brown
            };

            SKPath needleRightPath = new SKPath();
            needleRightPath.MoveTo(x, y);
            needleRightPath.LineTo(x + needleWidth, y);
            needleRightPath.LineTo(x, y - needleHeight);
            needleRightPath.LineTo(x, y);
            needleRightPath.LineTo(x + needleWidth, y);
            

            SKPath needleLeftPath = new SKPath();
            needleLeftPath.MoveTo(x, y);
            needleLeftPath.LineTo(x - needleWidth, y);
            needleLeftPath.LineTo(x, y - needleHeight);
            needleLeftPath.LineTo(x, y);
            needleLeftPath.LineTo(x - needleWidth, y);
            

            canvas.DrawPath(needleRightPath, paint);
            canvas.DrawPath(needleLeftPath, paint);
            canvas.Restore();
        }

 float AmountToAngle(float value)
        {
            return 135f + (value / (100 - 0)) * 270f;
        }
float ValueToAngle(float value)
        {
            return -135f + (value / (100 - 0)) * 270f;
        }


