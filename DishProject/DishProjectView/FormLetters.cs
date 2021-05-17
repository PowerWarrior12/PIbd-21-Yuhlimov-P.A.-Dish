using System;
using DishProjectBusinessLogic.BusinessLogics;
using DishProjectBusinessLogic.ViewModels;
using System.Windows.Forms;
using System.Linq;
using DishProjectBusinessLogic.BindingModels;

namespace DishProjectView
{
    public partial class FormLetters : Form
    {
        private readonly MailLogic logic;
        private PageViewModel pageViewModel;

        public FormLetters(MailLogic mailLogic)
        {
            logic = mailLogic;
            InitializeComponent();
        }

        private void FormLetters_Load(object sender, EventArgs e)
        {
            LoadData();
        }
        private void LoadData(int page = 1, int? lastPage = null)
        {
            int pageSize = 3; // Количество элементов на странице

            var list = logic.GetMessagesForPage(new MessageInfoBindingModel
            {
                Page = page,
                PageSize = pageSize
            });
            if (list != null)
            {
                pageViewModel = new PageViewModel(logic.Count(null), page, pageSize, list);
                dataGridView.DataSource = list;
                dataGridView.Columns[0].Visible = false;
                dataGridView.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }
            int pageStart = page < 3 ? 1 : page - 2;
            Button[] buttons = { buttonPage1, buttonPage2, buttonPage3, buttonPage4, buttonPage5 };
            for (int i = 0; i < buttons.Length; ++i)
            {
                buttons[i].Show();
                SetButtonPagetext(buttons[i], pageStart + i, pageViewModel.TotalPages);
            }
            buttons[page - 1 - (pageStart - 1)].BackColor = System.Drawing.SystemColors.ControlDark;
            if (lastPage.HasValue)
                buttons[(int)lastPage - 1 - (pageStart - 1)].BackColor = System.Drawing.SystemColors.Control;
        }

        private void ButtonBack_Click(object sender, EventArgs e)
        {
            if (pageViewModel.HasPreviousPage)
            {
                LoadData(pageViewModel.PageNumber - 1,pageViewModel.PageNumber);
            }
            else
            {
                MessageBox.Show("Это первая страница", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private void ButtonForward_Click(object sender, EventArgs e)
        {
            if (pageViewModel.HasNextPage)
            {
                LoadData(pageViewModel.PageNumber + 1, pageViewModel.PageNumber);
            }
            else
            {
                MessageBox.Show("Это последняя страница", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private void SetButtonPagetext(Button button, int pageNumber, int totalPages)
        {
            if (pageNumber <= totalPages)
            {
                button.Text = pageNumber.ToString();
            }
            else
            {
                button.Hide();
            }
        }
    }
}