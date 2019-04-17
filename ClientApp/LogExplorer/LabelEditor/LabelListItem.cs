﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ClientApp.LogExplorer.Controller;
using ClientApp.LogExplorer.View.RecycleListView;
using LogEntity;
using Newtonsoft.Json;

namespace ClientApp.LogExplorer.LabelEditor
{
    public partial class LabelListItem : RecycleLIstViewItemControl<LogLabel>
    {
        private LogLabel data;
        private LogExplorerController _controller;
        public LabelListItem()
        {
            InitializeComponent();
            
        }

        public LabelListItem(LogExplorerController controller) : this()
        {
            _controller = controller;
        }

        void MatchTheBox()
        {
            var label = boxLabel;
            var lbSize = label.Size;
            
            Font font2 = FlexFont(0, 15, new Size(lbSize.Width-5, lbSize.Height-5), label.Text, label.Font);
            label.Font = font2;
            label.BackColor = ColorTranslator.FromHtml(data.Color);
            label.ForeColor = label.BackColor.GetBrightness() > 0.5 ? Color.Black : Color.White;
            
        }

        public static Font FlexFont(float minFontSize, float maxFontSize, Size layoutSize, string s, Font f)
        {
            if (maxFontSize == minFontSize)
                f = new Font(f.FontFamily, minFontSize, f.Style);

            SizeF extent = TextRenderer.MeasureText(s, f);

            if (maxFontSize <= minFontSize)
                return f;

            float hRatio = layoutSize.Height / extent.Height;
            float wRatio = layoutSize.Width / extent.Width;
            float ratio = (hRatio < wRatio) ? hRatio : wRatio;

            float newSize = f.Size * ratio;

            if (newSize < minFontSize)
                newSize = minFontSize;
            else if (newSize > maxFontSize)
                newSize = maxFontSize;


            f = new Font(f.FontFamily, newSize, f.Style);
            //extent = g.MeasureString(s, f);

            return f;
        }

        public override void BindData(LogLabel data)
        {
            this.data = data;
            UpdateView();
        }

        private void UpdateView()
        {
            labelName.Text = data.Name;
            labelGuid.Text = data._id;
            boxLabel.Text = data.Text;
            MatchTheBox();
        }

        private async void btEdit_Click(object sender, EventArgs e)
        {
            var (res, form) = LabelEditorForm.GoEdit(data);
            if (res == DialogResult.OK)
            {
                await _controller.UpdateLabel(form.Result);
                RaiseOnDataDirty();
            }
        }

        private async void removeButton_Click(object sender, EventArgs e)
        {
            await _controller.DeleteLabel(data);
            RaiseOnDataDirty();
        }

        private async void cloneButton_Click(object sender, EventArgs e)
        {
            var clonedItem = JsonConvert.DeserializeObject<LogLabel>(JsonConvert.SerializeObject(data));
            clonedItem._id = Guid.NewGuid().ToString();
            clonedItem.Name += "_cloned";
            await _controller.AddLabel(clonedItem);
            RaiseOnDataDirty();
        }
    }
}