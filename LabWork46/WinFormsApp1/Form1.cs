using DatabaseLibrary;
using Microsoft.Data.Sqlite;

namespace WinFormsApp1
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
            InitializeComponent();
            string dbPath = Path.Combine(Application.StartupPath, "games.db");
            connection = new SqliteConnection($"Data Source={dbPath}");
            connection.Open();

            string createTable = "CREATE TABLE IF NOT EXISTS Games (Id INTEGER PRIMARY KEY AUTOINCREMENT, Name TEXT, Price REAL, Year INTEGER)";
            using var cmd = new SqliteCommand(createTable, connection);
            cmd.ExecuteNonQuery();

            // 5.1.4
            System.Diagnostics.Debug.WriteLine($"Строка подключения: Data Source={dbPath}");

            this.Text = "ЛР №46";
            this.Size = new Size(500, 350);
            this.StartPosition = FormStartPosition.CenterScreen;

            txtSql = new TextBox() { Location = new Point(10, 10), Size = new Size(340, 23), Text = "UPDATE Games SET Price = Price + 1" };
            btnExec = new Button() { Text = "5.2 Выполнить", Location = new Point(360, 8), Size = new Size(110, 27) };
            btnExec.Click += BtnExec_Click;

            // Поля для обновления (5.3)
            txtId = new TextBox() { Location = new Point(10, 60), Size = new Size(50, 23), PlaceholderText = "ID" };
            txtName = new TextBox() { Location = new Point(70, 60), Size = new Size(120, 23), PlaceholderText = "Название" };
            txtPrice = new TextBox() { Location = new Point(200, 60), Size = new Size(80, 23), PlaceholderText = "Цена" };
            btnUpdate = new Button() { Text = "5.3 Обновить", Location = new Point(290, 58), Size = new Size(100, 27) };
            btnUpdate.Click += BtnUpdate_Click;

            // Поля для вставки (5.4)
            txtYear = new TextBox() { Location = new Point(10, 110), Size = new Size(60, 23), PlaceholderText = "Год" };
            btnInsert = new Button() { Text = "5.4 Добавить", Location = new Point(80, 108), Size = new Size(100, 27) };
            btnInsert.Click += BtnInsert_Click;

            lblResult = new Label() { Location = new Point(10, 160), Size = new Size(450, 60), Text = "Результат:", BorderStyle = BorderStyle.FixedSingle };

            Controls.Add(txtSql);
            Controls.Add(btnExec);
            Controls.Add(txtId);
            Controls.Add(txtName);
            Controls.Add(txtPrice);
            Controls.Add(btnUpdate);
            Controls.Add(txtYear);
            Controls.Add(btnInsert);
            Controls.Add(lblResult);
        }

        // 5.2 
        private void BtnExec_Click(object sender, EventArgs e)
        {
            try
            {
                using var cmd = new SqliteCommand(txtSql.Text, connection);
                int rows = cmd.ExecuteNonQuery();
                lblResult.Text = $"Результат: изменено строк - {rows}";
            }
            catch (Exception ex)
            {
                lblResult.Text = $"Ошибка: {ex.Message}";
            }
        }

        // 5.3 
        private void BtnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                int id = int.Parse(txtId.Text);
                string name = txtName.Text;
                decimal price = decimal.Parse(txtPrice.Text);

                string sql = $"UPDATE Games SET Name = '{name}', Price = {price} WHERE Id = {id}";

                using var cmd = new SqliteCommand(sql, connection);
                int rows = cmd.ExecuteNonQuery();

                lblResult.Text = rows == 1 ? "Игра обновлена" : "ID не найден";
            }
            catch (Exception ex)
            {
                lblResult.Text = $"Ошибка: {ex.Message}";
            }
        }

        // 5.4 
        private void BtnInsert_Click(object sender, EventArgs e)
        {
            try
            {
                string name = txtName.Text;
                decimal price = decimal.Parse(txtPrice.Text);
                int year = int.Parse(txtYear.Text);

                string sql = "INSERT INTO Games (Name, Price, Year) VALUES (@name, @price, @year)";

                using var cmd = new SqliteCommand(sql, connection);
                cmd.Parameters.AddWithValue("@name", name);
                cmd.Parameters.AddWithValue("@price", price);
                cmd.Parameters.AddWithValue("@year", year);

                int rows = cmd.ExecuteNonQuery();
                lblResult.Text = rows == 1 ? "Игра добавлена" : "Ошибка";
            }
            catch (Exception ex)
            {
                lblResult.Text = $"Ошибка: {ex.Message}";
            }
        }
    }
}