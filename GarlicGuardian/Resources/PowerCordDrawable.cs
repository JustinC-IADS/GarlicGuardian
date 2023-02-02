using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GarlicGuardian.Resources
{
    internal class PowerCordDrawable : IDrawable
    {
        public Color Stroke { get; set; }

        public float StrokeThickness { get; set; }

        public void Draw(ICanvas canvas, RectF dirtyRect)
        {
            PathF path = new PathF();

            path.MoveTo(400, 200);
            path.CurveTo(0, 600, 600, 300, 1000, 300);

            canvas.StrokeColor = Stroke;
            canvas.StrokeSize = StrokeThickness;
            canvas.DrawPath(path);
        }
    }
}
