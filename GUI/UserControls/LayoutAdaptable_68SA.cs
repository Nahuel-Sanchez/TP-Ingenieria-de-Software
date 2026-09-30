using System;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

namespace GUI_08YS.UserControls
{
    /// <summary>
    /// Ayudas de layout para que las pantallas se adapten al espacio disponible (distintas
    /// resoluciones, escalado de Windows o tamaño de la ventana) sin superponer controles
    /// ni mostrar barras de desplazamiento innecesarias.
    /// Complementan Dock / Anchor / Padding / Margin del diseñador en los casos que estas
    /// propiedades no resuelven solas (reparto proporcional o tarjetas dentro de un FlowLayoutPanel).
    /// </summary>
    public static class LayoutAdaptable_68SA
    {
        /// <summary>
        /// Cada hijo directo del FlowLayoutPanel ocupa todo el ancho útil (menos Padding y Margin).
        /// El ancho se ajusta en el evento Layout, ANTES de que el FlowLayoutPanel acomode los hijos:
        /// así nunca aparece (ni por un instante) la barra horizontal.
        /// Opcionalmente, <paramref name="relleno"/> toma el alto que sobra para que el contenido
        /// llene la pantalla sin barra vertical; si no alcanza el mínimo, aparece solo la vertical.
        /// </summary>
        /// <param name="anchoMaximo">0 = sin tope; si es mayor a 0, los hijos no superan ese ancho.</param>
        /// <param name="relleno">Hijo que se estira en alto hasta completar el espacio libre (opcional).</param>
        /// <param name="altoMinimoRelleno">Alto mínimo del relleno; por debajo, el contenedor muestra la barra vertical.</param>
        public static void EstirarHijosAlAncho(FlowLayoutPanel contenedor, int anchoMaximo, Control relleno, int altoMinimoRelleno)
            => EstirarHijosAlAncho(contenedor, anchoMaximo, () => relleno, altoMinimoRelleno);

        /// <summary>
        /// Igual que la otra sobrecarga, pero el relleno se obtiene en cada ajuste: sirve cuando la
        /// tarjeta que completa el alto se crea por código y se vuelve a crear al recargar la pestaña.
        /// </summary>
        public static void EstirarHijosAlAncho(FlowLayoutPanel contenedor, int anchoMaximo = 0,
            Func<Control> obtenerRelleno = null, int altoMinimoRelleno = 0)
        {
            OcultarScrollHorizontal(contenedor);
            bool ajustando = false;

            void Ajustar()
            {
                if (ajustando) return;
                ajustando = true;
                contenedor.SuspendLayout();
                try
                {
                    int disponible = contenedor.ClientSize.Width - contenedor.Padding.Horizontal;
                    if (disponible <= 0) return;

                    foreach (Control hijo in contenedor.Controls)
                    {
                        int ancho = disponible - hijo.Margin.Horizontal;
                        if (anchoMaximo > 0) ancho = Math.Min(ancho, anchoMaximo);
                        ancho = Math.Max(ancho, hijo.MinimumSize.Width);
                        if (hijo.Width != ancho) hijo.Width = ancho;
                    }

                    // El relleno se calcula solo con el contenedor visible (Visible de los hijos depende del padre)
                    Control relleno = obtenerRelleno?.Invoke();
                    if (relleno != null && !relleno.IsDisposed && relleno.Parent == contenedor && contenedor.Visible)
                    {
                        int ocupado = contenedor.Padding.Vertical + relleno.Margin.Vertical;
                        foreach (Control hijo in contenedor.Controls)
                            if (hijo != relleno && hijo.Visible)
                                ocupado += hijo.Height + hijo.Margin.Vertical;

                        int alto = Math.Max(altoMinimoRelleno, contenedor.ClientSize.Height - ocupado);
                        if (relleno.Height != alto) relleno.Height = alto;
                    }
                }
                finally
                {
                    contenedor.ResumeLayout(false);
                    ajustando = false;
                }
            }

            contenedor.Layout += (s, e) => Ajustar();
            Ajustar();
        }

        /// <summary>
        /// Deja al contenedor con desplazamiento solo vertical: el contenido nunca se desborda
        /// hacia los costados porque los hijos se ajustan al ancho.
        /// </summary>
        public static void OcultarScrollHorizontal(ScrollableControl contenedor)
        {
            if (!contenedor.AutoScroll) return;
            contenedor.AutoScroll = false;
            contenedor.HorizontalScroll.Maximum = 0;
            contenedor.HorizontalScroll.Enabled = false;
            contenedor.HorizontalScroll.Visible = false;
            contenedor.AutoScroll = true;
        }

        /// <summary>
        /// Activa el doble buffer de un control que no lo expone (DataGridView, Panel, FlowLayoutPanel...):
        /// el control se pinta en memoria y se vuelca de una vez, sin parpadeo al desplazarse o redimensionar.
        /// </summary>
        public static void HabilitarDobleBuffer(Control control)
        {
            typeof(Control)
                .GetProperty("DoubleBuffered", BindingFlags.NonPublic | BindingFlags.Instance)
                ?.SetValue(control, true, null);
        }

        /// <summary>
        /// Reparte el ancho del contenedor en columnas iguales. Cada columna es un grupo de controles
        /// apilados (por ejemplo etiqueta + campo) que comparten la misma X; los Label conservan su
        /// AutoSize y el resto toma el ancho de la columna.
        /// El margen izquierdo y la separación se toman de la posición actual de los controles
        /// (ya escalada), y el margen derecho es igual al izquierdo.
        /// </summary>
        public static void RepartirColumnas(Control contenedor, params Control[][] columnas)
        {
            if (columnas == null || columnas.Length == 0) return;

            int margen = columnas[0].Min(c => c.Left);
            int separacion = columnas.Length > 1
                ? Math.Max(0, columnas[1].Min(c => c.Left) - AnchoCampo(columnas[0]).Right)
                : 0;

            void Ajustar()
            {
                int n = columnas.Length;
                int disponible = contenedor.ClientSize.Width - margen * 2 - separacion * (n - 1);
                if (disponible <= 0) return;

                int ancho = disponible / n;
                int x = margen;
                foreach (var columna in columnas)
                {
                    foreach (var control in columna)
                    {
                        if (control.Left != x) control.Left = x;
                        if (!(control is Label) && control.Width != ancho) control.Width = ancho;
                    }
                    x += ancho + separacion;
                }
            }

            // Layout se dispara antes de acomodar los hijos: se reubican sin un repintado intermedio
            contenedor.Layout += (s, e) => Ajustar();
            Ajustar();
        }

        /// <summary>Estira los controles al ancho del contenedor dejando a la derecha el mismo margen que a la izquierda.</summary>
        public static void EstirarAlAncho(Control contenedor, params Control[] controles)
        {
            foreach (var control in controles)
                RepartirColumnas(contenedor, new[] { control });
        }

        /// <summary>
        /// Ajusta el alto de un FlowLayoutPanel con WrapContents (y el de la tarjeta que lo contiene)
        /// a lo que necesita su contenido: si los controles pasan a otra fila, la tarjeta crece.
        /// </summary>
        /// <param name="margenInferior">Espacio libre debajo del contenido dentro de la tarjeta.</param>
        public static void AjustarAltoAlContenido(Control tarjeta, FlowLayoutPanel contenido, int margenInferior)
        {
            bool ajustando = false;

            void Ajustar()
            {
                if (ajustando || contenido.Width <= 0) return;
                ajustando = true;
                try
                {
                    int alto = contenido.GetPreferredSize(new System.Drawing.Size(contenido.Width, 0)).Height;
                    if (contenido.Height != alto) contenido.Height = alto;
                    int altoTarjeta = contenido.Top + alto + margenInferior;
                    if (tarjeta.Height != altoTarjeta) tarjeta.Height = altoTarjeta;
                }
                finally
                {
                    ajustando = false;
                }
            }

            contenido.Resize += (s, e) => Ajustar();
            contenido.ControlAdded += (s, e) => Ajustar();
            Ajustar();
        }

        private static Control AnchoCampo(Control[] columna)
        {
            // El control más ancho que no sea Label define el borde derecho de la columna
            return columna.Where(c => !(c is Label)).DefaultIfEmpty(columna[0]).OrderByDescending(c => c.Right).First();
        }
    }
}
