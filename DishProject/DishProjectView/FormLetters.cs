using System;
using DishProjectBusinessLogic.BusinessLogics;
using DishProjectBusinessLogic.ViewModels;
using DishProjectBusinessLogic.BindingModels;
using System.Windows.Forms;

namespace DishProjectView
{
    public partial class FormLetters : Form
    {
        private readonly MailLogic logic;

        public FormLetters(MailLogic mailLogic)
        {
            logic = mailLogic;
            InitializeComponent();
        }

        private void FormLetters_Load(object sender, EventArgs e)
        {
            var list = logic.Read(null);
            if (list != null)
            {
                dataGridView.DataSource = list;
                dataGridView.Columns[0].Visible = false;
                dataGridView.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }
        }
    }
}
