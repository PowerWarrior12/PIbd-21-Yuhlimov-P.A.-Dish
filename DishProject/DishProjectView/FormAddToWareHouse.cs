using DishProjectBusinessLogic.BindingModels;
using DishProjectBusinessLogic.BusinessLogics;
using DishProjectBusinessLogic.ViewModels;
using System;
using System.Windows.Forms;
using Unity;
using System.Collections.Generic;

namespace DishProjectView
{
    public partial class FormAddToWareHouse : Form
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }
        private readonly ComponentLogic _logicC;
        private readonly WareHouseLogic _logicW;
        public FormAddToWareHouse(ComponentLogic logicC, WareHouseLogic logicW)
        {
            InitializeComponent();
            _logicC = logicC;
            _logicW = logicW;
            List<ComponentViewModel> listC = _logicC.Read(null);
            if (listC != null)
            {
                comboBoxComponent.DisplayMember = "ComponentName";
                comboBoxComponent.ValueMember = "Id";
                comboBoxComponent.DataSource = listC;
                comboBoxComponent.SelectedItem = null;
            }
            List<WareHouseViewModel> listW = _logicW.Read(null);
            if (listW != null)
            {
                comboBoxWareHouse.DisplayMember = "Name";
                comboBoxWareHouse.ValueMember = "Id";
                comboBoxWareHouse.DataSource = listW;
                comboBoxWareHouse.SelectedItem = null;
            }
        }

        private void ButtonAdd_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxCount.Text))
            {
                MessageBox.Show("Заполните поле Количество", "Ошибка",
               MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (comboBoxComponent.SelectedValue == null)
            {
                MessageBox.Show("Выберите компонент", "Ошибка", MessageBoxButtons.OK,
               MessageBoxIcon.Error);
                return;
            }
            if (comboBoxWareHouse.SelectedValue == null)
            {
                MessageBox.Show("Выберите склад", "Ошибка", MessageBoxButtons.OK,
               MessageBoxIcon.Error);
                return;
            }
            try
            {
                string component = comboBoxComponent.Text;
                string wareHouse = comboBoxWareHouse.Text;
                List<ComponentViewModel> listC = _logicC.Read(null);
                
                _logicW.AddNewComponent(
                    new AddComponentBindingModel {
                        WareHouseId = Convert.ToInt32(comboBoxWareHouse.SelectedValue),
                        ComponentId = listC[comboBoxComponent.SelectedIndex].Id,
                        Count = Int32.Parse(textBoxCount.Text)
                    });
                MessageBox.Show("Сохранение прошло успешно", "Сообщение",
               MessageBoxButtons.OK, MessageBoxIcon.Information);
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK,
               MessageBoxIcon.Error);
            }
        }

        private void ButtonCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
