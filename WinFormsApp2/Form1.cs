using LabLibrary2;

namespace WinFormsApp2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void buttonCreateDoc_Click(object sender, EventArgs e)
        {
            try
            {
                List<ParagraphData> paragraphs = new List<ParagraphData>
                {
                    new ParagraphData("Это первый абзац."),
                    new ParagraphData("Это второй абзац."),
                    new ParagraphData("Это третий абзац."),
                    new ParagraphData("Это четвертый абзац."),
                    new ParagraphData("Это пятый абзац."),
                    new ParagraphData("Это шестой абзац."),
                    new ParagraphData("Это седьмой абзац."),
                    new ParagraphData("Это восьмой абзац."),
                    new ParagraphData("Это девятый абзац."),
                    new ParagraphData("Это десятый абзац."),
                };

                // Получаем текущий каталог
                string projectDirectory = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName;

                // Определяем полный путь к файлу в папке с проектом
                string filePath = Path.Combine(projectDirectory, "document.docx");
                string title = "Заголовок документа";

                bool success = bigTextComponent1.CreateDocument(filePath, title, paragraphs);

                if (success)
                {
                    MessageBox.Show("Документ успешно создан!", "Результат", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonCreateTable_Click(object sender, EventArgs e)
        {
            // Получаем текущий каталог
            string projectDirectory = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName;

            // Получаем путь к файлу
            string filePath = Path.Combine(projectDirectory, "EmployeeTable.docx");
            string documentTitle = "Таблица сотрудников";

            // Группы заголовков
            List<HeaderGroup> headerGroups = new List<HeaderGroup>
            {
                new HeaderGroup("ID", 1, null),
                new HeaderGroup("Личные данные", 3, new List<string> { "Имя", "Фамилия", "Возраст" }),
                new HeaderGroup("Дети", 1, null),
                new HeaderGroup("Работа", 2, new List<string> { "Должность", "Подразделение" })
            };

            // Настройка ширины колонок
            List<int> columnWidths = new List<int> { 2, 2, 2, 2, 2, 2, 2 };
            // Пример данных сотрудников
            var employees = new List<object>
            {
                new { Id = 1, Name = "Иван", Surname = "Иванов", Kids = "Нет", Age = 30, JobTitle = "Инженер", Department = "Отдел IT" },
                new { Id = 2, Name = "Петр", Surname = "Петров", Kids = "Нет", Age = 25, JobTitle = "Аналитик", Department = "Отдел продаж" }
            };

            // Соответствие колонок и полей объектов
            Dictionary<int, string> propertyMapping = new Dictionary<int, string>
            {
                { 0, "Id" },
                { 1, "Name" },
                { 2, "Surname" },
                { 3, "Age" },
                { 4, "Kids" },
                { 5, "Department" },
                { 6, "JobTitle" }
            };

            // Вызов метода для создания документа
            try
            {
                tableComponent1.CreateDocument(filePath, documentTitle, headerGroups, columnWidths, employees, propertyMapping);
                MessageBox.Show("Документ успешно создан!", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при создании документа: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

    }
}
