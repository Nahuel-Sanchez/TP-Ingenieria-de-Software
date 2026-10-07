using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.ComponentModel;

namespace GUI_08YS
{
    public class DropdownMenuStrip_08YS : ContextMenuStrip
    {
        #region Campos

        //Campos
        private bool isMainMenu;
        private int menuItemHeight = 25;
        private Color menuItemTextColor = Color.Empty; //Sin color. El predeterminado esta en la clase MenuRenderer
        private Color primaryColor = Color.Empty; //Sin color. El predeterminado esta en la clase MenuRenderer

        private Bitmap menuItemHeaderSize;

        //Metodos privados
        // Alto de cada renglón: lo marca la imagen (ícono o espacio en blanco del mismo tamaño)
        private const int AnchoImagen = 26;
        private const int AltoRenglon = 38;

        #endregion

        #region Propiedades

        //Propiedades
        //Opcionalmente, ocultar las propiedades en el cuadro de herramientas para evitar el problema de mostrar y/o 
        //guardar los cambios de las propiedades del control en el diseñador en tiempo de diseño en Visual Studio.
        [Browsable(false)]
        public bool IsMainMenu
        {
            get { return isMainMenu; }
            set { isMainMenu = value; }
        }

        [Browsable(false)]
        public int MenuItemHeight
        {
            get { return menuItemHeight; }
            set { menuItemHeight = value; }
        }

        [Browsable(false)]
        public Color MenuItemTextColor
        {
            get { return menuItemTextColor; }
            set { menuItemTextColor = value; }
        }

        [Browsable(false)]
        public Color PrimaryColor
        {
            get { return primaryColor; }
            set { primaryColor = value; }
        }

        #endregion

        #region Constructor

        //Constructor
        public DropdownMenuStrip_08YS(IContainer container)
            : base(container)
        {

        }

        #endregion

        #region Métodos públicos

        /// <summary>Ícono dorado de FontAwesome a la izquierda del texto del ítem.</summary>
        public static void AsignarIcono(ToolStripMenuItem item, FontAwesome.Sharp.IconChar icono)
        {
            var lienzo = new Bitmap(AnchoImagen, AltoRenglon);
            using (var g = Graphics.FromImage(lienzo))
            {
                var imagen = UserControls.IconCache.Get(icono, FontAwesome.Sharp.IconFont.Auto, 18, Color.Goldenrod);
                if (imagen != null)
                    g.DrawImage(imagen, (AnchoImagen - 18) / 2 + 2, (AltoRenglon - 18) / 2, 18, 18);
            }
            item.Image = lienzo;
            item.ImageScaling = ToolStripItemImageScaling.None;
        }

        #endregion

        #region Métodos auxiliares

        private void LoadMenuItemApparence()
        {
            menuItemHeaderSize = new Bitmap(AnchoImagen, AltoRenglon);
            Padding = new Padding(0, 6, 0, 6);
            AplicarEstilo(Items);
        }

        private void AplicarEstilo(ToolStripItemCollection items)
        {
            foreach (ToolStripItem item in items)
            {
                item.ForeColor = UserControls.MenuHotelRenderer_68SA.Texto;
                item.Font = new Font("Segoe UI", 10F);
                item.ImageScaling = ToolStripItemImageScaling.None;
                item.Padding = new Padding(4, 0, 12, 0);

                if (item is ToolStripMenuItem menuItem)
                {
                    if (menuItem.Image == null) menuItem.Image = menuItemHeaderSize;
                    if (menuItem.HasDropDownItems)
                    {
                        // Los submenús usan el mismo dibujo que el menú principal
                        menuItem.DropDown.Renderer = Renderer;
                        menuItem.DropDown.Padding = new Padding(0, 6, 0, 6);
                        AplicarEstilo(menuItem.DropDownItems);
                    }
                }
            }
        }

        #endregion

        #region Sobrescrituras

        //Overrides
        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);
            if (this.DesignMode == false)
            {
                // Mismo estilo para todos los menús desplegables (antes dependía de IsMainMenu y PrimaryColor)
                this.Renderer = new UserControls.MenuHotelRenderer_68SA();
                LoadMenuItemApparence();
            }
        }

        #endregion
    }
}
