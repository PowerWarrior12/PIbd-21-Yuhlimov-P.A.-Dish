using System;
using DishProjectBusinessLogic.BusinessLogics;
using DishProjectBusinessLogic.ViewModels;
using DishProjectBusinessLogic.BindingModels;
using System.Windows.Forms;
using System.Linq;

namespace DishProjectView
{
    public partial class FormLetters : Form
    {
        private readonly MailLogic logic;
        private IndexViewModel index;

        public FormLetters(MailLogic mailLogic)
        {
            logic = mailLogic;
            index = new IndexViewModel();
            InitializeComponent();
        }

        private void FormLetters_Load(object sender, EventArgs e)
        {
            LoadData();
        }
        private void LoadData(int page = 1)
        {
            int pageSize = 5; // Количество элементов на странице

            var list = logic.Read(null);
            if (list != null)
            {
                index.PageViewModel = new PageViewModel(list.Count, page, pageSize);
                index.Messages = list.Skip((page - 1) * pageSize).Take(pageSize).ToList();

                dataGridView.DataSource = index.Messages;
                dataGridView.Columns[0].Visible = false;
                dataGridView.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }
        }

        private void ButtonBack_Click(object sender, EventArgs e)
        {
            if (index.PageViewModel.HasPreviousPage)
            {
                LoadData(index.PageViewModel.PageNumber - 1);
            }
            else
            {
                MessageBox.Show("Первая страница", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void ButtonForward_Click(object sender, EventArgs e)
        {
            if (index.PageViewModel.HasNextPage)
            {
                LoadData(index.PageViewModel.PageNumber + 1);
            }
            else
            {
                MessageBox.Show("Последняя страница", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
