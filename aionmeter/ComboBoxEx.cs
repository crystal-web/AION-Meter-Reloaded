using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Imaging;

namespace AIONMeter
{
    class ComboBoxEx : ComboBox
    {
        private ImageList imageList = null;
        public ImageList ImageList
        {
            get { return imageList; }
            set { imageList = value; }
        }

        public ComboBoxEx()
        {
            DrawMode = DrawMode.OwnerDrawFixed;
        }

        protected override void OnDrawItem(DrawItemEventArgs ea)
        {
            ea.DrawBackground();
            ea.DrawFocusRectangle();

            if (imageList != null)
            {
                ComboBoxExItem item;
                Size imageSize = imageList.ImageSize;
                Rectangle bounds = ea.Bounds;

                try
                {
                    item = (ComboBoxExItem)Items[ea.Index];

                    if (item.ImageIndex != -1)
                    {
                        ImageAttributes attr = new ImageAttributes();
                        Color color = System.Drawing.ColorTranslator.FromHtml("#425B8C");
                        ColorMatrix cm=GraphEngine.get_colormatrix(color);
                        attr.SetColorMatrix(cm);
                        Image image = ImageList.Images[item.ImageIndex];
                        ea.Graphics.DrawImage(image, new Rectangle(bounds.Left, bounds.Top, image.Width, image.Height), 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, attr);
                    }
                }
                catch
                { }
            }

            base.OnDrawItem(ea);
        }
    }

    class ComboBoxExItem
    {
        private int _imageIndex;
        public string tag;
        public int ImageIndex
        {
            get { return _imageIndex; }
            set { _imageIndex = value; }
        }

        public ComboBoxExItem()
        {
        }

        public ComboBoxExItem(int imageIndex,string _tag)
        {
            _imageIndex = imageIndex;
            tag = _tag;
        }

        public override string ToString()
        {
            return "";
        }
    }
}
