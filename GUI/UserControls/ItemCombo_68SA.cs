using CustomControls;

namespace GUI_08YS.UserControls
{
    /// <summary>Ítem de IconComboBox: texto visible + Id (null = "Todos" en los filtros).</summary>
    public class ItemCombo_68SA
    {
        public int? Id { get; }
        public string Texto { get; }

        public ItemCombo_68SA(int? id, string texto)
        {
            Id = id;
            Texto = texto;
        }

        public override string ToString() => Texto;

        public static int? IdSeleccionado(IconComboBox cmb) => (cmb.SelectedItem as ItemCombo_68SA)?.Id;

        // Selecciona el ítem con ese Id; si no está, el primero (en los filtros, "Todos")
        public static void Seleccionar(IconComboBox cmb, int? id)
        {
            for (int i = 0; i < cmb.Items.Count; i++)
            {
                if (cmb.Items[i] is ItemCombo_68SA item && item.Id == id)
                {
                    cmb.SelectedIndex = i;
                    return;
                }
            }
            cmb.SelectedIndex = cmb.Items.Count > 0 ? 0 : -1;
        }
    }
}