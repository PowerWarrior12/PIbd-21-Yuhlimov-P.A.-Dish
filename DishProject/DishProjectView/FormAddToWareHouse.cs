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
            List<ComponentViewModel> listW = _logicC.Read(null);
            if (listW != null)
            {
                comboBoxWareHouse.DisplayMember = "HouseWareName";
                comboBoxWareHouse.ValueMember = "Id";
                comboBoxWareHouse.DataSource = listW;
                comboBoxWareHouse.SelectedItem = null;
            }
        }

        private void ButtonAdd_Click(object sender, EventArgs e)
        {

        }

        private void ButtonCancel_Click(object sender, EventArgs e)
        {

        }
    }
}
