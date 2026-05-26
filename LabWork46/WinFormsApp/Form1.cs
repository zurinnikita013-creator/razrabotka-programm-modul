using System;
using System.Windows.Forms;
using Microsoft.Data.Sqlite;

namespace WinFormsApp
{
    public partial class Form1 : Form
    {
        private SqliteConnection connection;
        private TextBox txtSql;
        private TextBox txtId;
        private TextBox txtName;
        private TextBox txtPrice;
        private TextBox txtYear;
        private Label lblResult;
        private Button btnExec;
        private Button btnUpdate;
        private Button btnInsert;

        public Form1()
        {
            // Инициализация компонентов (обязательно для дизайнера)
            InitializeComponent();

            // Создаём БД
            connection = new SqliteConnection("Data Source=games.db");
            connection.Open();

            string createTable = @"
                CREATE TABLE IF NOT EXISTS Games (
                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                    Name TEXT,
                    Price REAL,
                    Year INTEGER
                )";
            using var cmd = new SqliteCommand(createTable, connection);
            cmd.ExecuteNonQuery();

            // Настраиваем форму
            this.Text = "ЛР №46 - Работа с БД";
            this.Size = new System.Drawing.Size(500, 350);

            // Добавляем элементы
            AddControls();
        }

        private void AddControls()
        {
            // SQL запрос
            txtSql = new TextBox();
            txtSql.Location = new System.Drawing.Point(10, 10);
            txtSql.Size = new System.Drawing.Size(350, 20);
            txtSql.Text = "UPDATE Games SET Price = Price + 1";

            btnExec = new Button();
            btnExec.Text = "Выполнить";
            btnExec.Location = new System.Drawing.Point(370, 8);
            btnExec.Click += BtnExec_Click;

            // Обновление
            txtId = new TextBox();
            txtId.Location = new System.Drawing.Point(10, 60);
            txtId.Size = new System.Drawing.Size(50, 20);
            txtId.Text = "";

            txtName = new TextBox();
            txtName.Location = new System.Drawing.Point(70, 60);
            txtName.Size = new System.Drawing.Size(100, 20);

            txtPrice = new TextBox();
            txtPrice.Location = new System.Drawing.Point(180, 60);
            txtPrice.Size = new System.Drawing.Size(70, 20);

            btnUpdate = new Button();
            btnUpdate.Text = "Обновить";
            btnUpdate.Location = new System.Drawing.Point(260, 58);
            btnUpdate.Click += BtnUpdate_Click;

            // Добавление
            txtYear = new TextBox();
            txtYear.Location = new System.Drawing.Point(10, 110);
            txtYear.Size = new System.Drawing.Size(60, 20);

            btnInsert = new Button();
            btnInsert.Text = "Добавить игру";
            btnInsert.Location = new System.Drawing.Point(80, 108);
            btnInsert.Click += BtnInsert_Click;

            // Результат
            lblResult = new Label();
            lblResult.Location = new System.Drawing.Point(10, 160);
            lblResult.Size = new System.Drawing.Size(450, 50);
            lblResult.Text = "Результат:";

            // Добавляем на форму
            this.Controls.Add(txtSql);
            this.Controls.Add(btnExec);
            this.Controls.Add(txtId);
            this.Controls.Add(txtName);
            this.Controls.Add(txtPrice);
            this.Controls.Add(btnUpdate);
            this.Controls.Add(txtYear);
            this.Controls.Add(btnInsert);
            this.Controls.Add(lblResult);
        }

        private void BtnExec_Click(object sender, EventArgs e)
        {
            try
            {
                using var cmd = new SqliteCommand(txtSql.Text, connection);
                int rows = cmd.ExecuteNonQuery();
                lblResult.Text = $"✅ Изменено строк: {rows}";
            }
            catch (Exception ex)
            {
                lblResult.Text = $"❌ Ошибка: {ex.Message}";
            }
        }

        private void BtnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                int id = Convert.ToInt32(txtId.Text);
                string name = txtName.Text;
                decimal price = Convert.ToDecimal(txtPrice.Text);

                string sql = $"UPDATE Games SET Name = '{name}', Price = {price} WHERE Id = {id}";

                using var cmd = new SqliteCommand(sql, connection);
                int rows = cmd.ExecuteNonQuery();

                lblResult.Text = rows == 1 ? "✅ Игра обновлена" : "❌ ID не найден";
            }
            catch (Exception ex)
            {
                lblResult.Text = $"❌ Ошибка: {ex.Message}";
            }
        }

        private void BtnInsert_Click(object sender, EventArgs e)
        {
            try
            {
                string name = txtName.Text;
                decimal price = Convert.ToDecimal(txtPrice.Text);
                int year = Convert.ToInt32(txtYear.Text);

                string sql = "INSERT INTO Games (Name, Price, Year) VALUES (@name, @price, @year)";

                using var cmd = new SqliteCommand(sql, connection);
                cmd.Parameters.AddWithValue("@name", name);
                cmd.Parameters.AddWithValue("@price", price);
                cmd.Parameters.AddWithValue("@year", year);

                int rows = cmd.ExecuteNonQuery();
                lblResult.Text = rows == 1 ? "✅ Игра добавлена" : "❌ Ошибка";
            }
            catch (Exception ex)
            {
                lblResult.Text = $"❌ Ошибка: {ex.Message}";
            }
        }
    }
}