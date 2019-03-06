using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ClientApp.LogExplorer.Controller;
using ClientApp.LogExplorer.Model;
using LogEntity;

namespace ClientApp.LogExplorer.View
{
    class MainView : ViewBase
    {
        const float RECT_SIZE = 50;
        const float RECT_INDENT = 5;
        const float TRACE_INDENT = 20;
        const float TRACE_INFO_W = 24;

        private PictureBox _pb;
        private MainViewClickHandler _clickHandler = new MainViewClickHandler();

        public MainView(LogExplorerController controller) : base(controller)
        {
        }

        public void Bind(PictureBox pb)
        {
            _pb = pb;
            _pb.Paint += PbOnPaint;
            _pb.MouseWheel += PbOnMouseWheel;
            _pb.SizeChanged += PbOnSizeChanged;
            _pb.MouseClick += PbOnMouseClick;
        }

        private void PbOnMouseClick(object sender, MouseEventArgs e)
        {
            _clickHandler.HandleClick(e.Location);
        }

        private void PbOnSizeChanged(object sender, EventArgs eventArgs)
        {
            var newVal = _pb.Size.Height / (int)(RECT_SIZE + RECT_INDENT);
            if (newVal != State.TracesInView)
            {
                Controller.ChangeTracesInWindow(newVal);
            }
            this.Refresh();
        }

        private void PbOnMouseWheel(object sender, MouseEventArgs e)
        {
            var pos = State.Pos;
            var dt = e.Delta * -1;
            var newPos = Math.Min(Math.Max(pos + dt, 0), State.Info.ItemsCount);//todo: add here window size
            Controller.ChangePos(newPos, this, true);
        }

        private void PbOnPaint(object sender, PaintEventArgs e)
        {
            _clickHandler.Reset();
            var g = e.Graphics;
            g.Clear(Color.White);
            if (State.Info == null)
                return;
            var rectSize = new SizeF(RECT_SIZE, RECT_SIZE);

            Vector2 y_cursor = new Vector2(0, RECT_INDENT);


            var y_step = new Vector2(0, RECT_SIZE + RECT_INDENT);
            bool yIsOverflow()
            {
                return (y_cursor + y_step).Y > e.ClipRectangle.Height;
            }
            foreach (var trace in State.Log.EnumerateInRage(State.Pos, State.Pos + State.TracesInView))
            {
                int linesNum = 1;
                Vector2 y_start = y_cursor;
                Vector2 x_start = new Vector2(TRACE_INFO_W + RECT_INDENT, 0);
                Vector2 x_cursor = x_start;
                Vector2 x_step = new Vector2(RECT_SIZE + RECT_INDENT, 0);
                bool notFullyDrawn = false;
                if (trace == null)
                {
                    var rect = new RectangleF((x_cursor + y_cursor).ToPointF(), rectSize);
                    g.FillRectangle(Brushes.LightGray, rect);
                }
                else
                {
                    for (int i = 0; i < trace.Items.Count; i++)
                    {
                        //check if need to go to new line
                        if ((x_cursor + x_step).X > e.ClipRectangle.Width)
                        {
                            //new line
                            linesNum++;
                            x_cursor = x_start;
                            y_cursor += y_step;
                            //do we need break here (y overflow)
                            if (yIsOverflow())
                            {
                                notFullyDrawn = true;
                                break;
                            }

                        }
                        var rect = new RectangleF((x_cursor + y_cursor).ToPointF(), rectSize);

                        _clickHandler.PushItemRect(rect, trace, i);

                        var rules = GetRulesForItem(trace, i).ToArray();
                        switch (rules.Length)
                        {
                            case 0:
                                g.FillRectangle(Brushes.Gray, rect);
                                break;
                            
                            case 2:
                            case 3:
                                {
                                    for (int j = 0; j < rules.Length; j++)
                                    {
                                        var partRect = new RectangleF(rect.X + rect.Width/rules.Length*j, rect.Y, rect.Width/rules.Length,rect.Height);
                                        g.FillRectangle(new SolidBrush(rules[j].Color), partRect);
                                    }
                                    //two sided rect
                                    //var rect1 = new RectangleF(rect.X, rect.Y, rect.Width / 2, rect.Height);
                                    //var rect2 = new RectangleF(rect.X + rect.Width / 2, rect.Y, rect.Width / 2, rect.Height);
                                    //g.FillRectangle(new SolidBrush(rules[0].Color), rect1);
                                    //g.FillRectangle(new SolidBrush(rules[1].Color), rect2);
                                    break;

                                }
                            case 1:
                            default:

                                var bgColor = rules[0].Color;
                                g.FillRectangle(new SolidBrush(bgColor), rect);
                                var text = rules[0].Text;
                                var textColor = bgColor.GetBrightness() > 0.5 ? Color.Black : Color.White;
                                var font = new Font("Cosnsolas", 15);
                                var textSize = g.MeasureString(text, font);
                                var pos = new PointF(rect.X + rect.Width/2 - textSize.Width/2, rect.Y + rect.Height/2 - textSize.Height/2);
                                g.DrawString(text,font,new SolidBrush(textColor), pos);
                                break;


                        }
                        //increment x pos
                        x_cursor += x_step;
                    }
                    //draw trace left green line

                    if (notFullyDrawn)
                    {
                        var start = y_start;
                        var w = new Vector2(TRACE_INFO_W, 0);
                        var h = y_cursor - new Vector2(0, RECT_INDENT) - y_start;

                        var b = new ShapeBuilder()
                            .MoveTo(start)
                            .MoveBy(w)
                            .MoveBy(h);

                        //drawing /\/\/\/\
                        var fluct = TRACE_INFO_W / 4;
                        var fluctUp = new Vector2(-fluct, -fluct);
                        var fluctDown = new Vector2(-fluct, fluct);
                        for (int i = 0; i < 4; i++)
                        {
                            if (i % 2 == 0)
                            {
                                b.MoveBy(fluctUp);
                            }
                            else
                            {
                                b.MoveBy(fluctDown);
                            }
                        }
                        g.FillPolygon(new SolidBrush(Color.FromArgb(87, 116 + 40, 48)), b.GetPtsF());
                    }
                    else
                    {
                        var start = y_start;
                        var size = new SizeF(TRACE_INFO_W, (y_cursor + y_step - new Vector2(0, RECT_INDENT) - y_start).Y);
                        var rect = new RectangleF(start.ToPointF(), size);
                        _clickHandler.PushTraceRect(rect, trace);
                        g.FillRectangle(new SolidBrush(Color.FromArgb(87, 116 + 40, 48)), rect);
                    }
                }

                y_cursor += y_step;

                if (yIsOverflow())
                    break;
            }

        }

        class ShapeBuilder
        {
            private List<Vector2> points = new List<Vector2>();
            /// <summary>
            /// Adds point to shape, no matter what previous points are
            /// </summary>
            /// <param name="pt"></param>
            public ShapeBuilder MoveTo(Vector2 pt)
            {
                points.Add(pt);
                return this;
            }

            public ShapeBuilder MoveBy(Vector2 dt)
            {
                points.Add(points.Last() + dt);
                return this;
            }

            public PointF[] GetPtsF()
            {
                return points.Select(t => t.ToPointF()).ToArray();
            }
        }
        private void PbOnPaintold(object sender, PaintEventArgs e)
        {
            var g = e.Graphics;
            g.Clear(Color.White);
            if (State.Info == null)
                return;
            //g.DrawString(State.Info?.Name + " pos : " + State.Pos.ToString(), new Font("consolas", 25), Brushes.Black, 5,5);

            int rectsInRow = (int)((_pb.Width - TRACE_INFO_W) / (RECT_SIZE + TRACE_INDENT));
            var rectSize = new SizeF(RECT_SIZE, RECT_SIZE);
            int linesUsed = 0;

            Vector2 GetPos()
            {
                return new Vector2(RECT_INDENT, RECT_INDENT + linesUsed * (RECT_SIZE + TRACE_INDENT));
            }
            for (int i = 0; i < State.TracesInView; i++)
            {
                int traceSize = 0;
                //var rect = new RectangleF(0,50+i*55,50,50);
                var trace = Controller.State.Log[State.Pos + i];
                if (trace == null)
                {
                    var rect = new RectangleF(GetPos().ToPointF(), rectSize);
                    g.FillRectangle(Brushes.LightGray, rect);
                }
                else
                {
                    int x_used = 0;
                    for (int j = 0; j < trace.Items.Count; j++)
                    {
                        //check if new line needed
                        if (x_used == rectsInRow)
                        {
                            linesUsed++;
                            traceSize++;
                            x_used = 0;
                        }
                        var item = trace.Items[j];
                        var move = new Vector2(x_used * (RECT_SIZE + RECT_INDENT), 0);
                        var rect = new RectangleF((GetPos() + move).ToPointF(), rectSize);
                        var rules = GetRulesForItem(trace, j).ToArray();
                        if (rules.Length == 0)
                        {
                            g.FillRectangle(Brushes.Gray, rect);
                        }
                        else
                        {
                            if (rules.Length == 1)
                            {
                                g.FillRectangle(new SolidBrush(rules[0].Color), rect);
                            }
                            else
                            {
                                //multicolor
                            }
                        }

                        x_used++;

                    }
                    //g.FillRectangle(Brushes.DodgerBlue, rect);
                }

                traceSize++;
                linesUsed++;
            }
        }

        private IEnumerable<Rule> GetRulesForItem(LogTraceWithLabels tr, int pos)
        {
            if (pos >= tr.ItemsLabels.Count)
                yield break;
            var lb = tr.ItemsLabels[pos];
            foreach (var i in lb)
            {
                yield return Controller.State.Rules[i];
            }

        }

        public override void Refresh()
        {
            _pb.Refresh();
        }
    }
}
