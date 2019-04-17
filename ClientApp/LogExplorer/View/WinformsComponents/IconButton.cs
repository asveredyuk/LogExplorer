using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClientApp.LogExplorer.View.WinformsComponents
{
    sealed class IconButton : Button
    {

        public IconButton()
        {
            FlatStyle = FlatStyle.Flat;
            FlatAppearance.BorderSize = 0;
            this.Size = new Size(40,40);
            BackColor = Color.Transparent;
            Size = new Size(40,40);
            ImageAlign = ContentAlignment.MiddleCenter;


        }


    }
}
