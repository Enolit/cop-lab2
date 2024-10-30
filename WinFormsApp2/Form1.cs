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
                    new ParagraphData("��� ������ �����."),
                    new ParagraphData("��� ������ �����."),
                    new ParagraphData("��� ������ �����."),
                    new ParagraphData("��� ��������� �����."),
                    new ParagraphData("��� ����� �����."),
                    new ParagraphData("��� ������ �����."),
                    new ParagraphData("��� ������� �����."),
                    new ParagraphData("��� ������� �����."),
                    new ParagraphData("��� ������� �����."),
                    new ParagraphData("��� ������� �����."),
                };

                // �������� ������� �������
                string projectDirectory = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName;

                // ���������� ������ ���� � ����� � ����� � ��������
                string filePath = Path.Combine(projectDirectory, "document.docx");
                string title = "��������� ���������";

                bool success = bigTextComponent1.CreateDocument(filePath, title, paragraphs);

                if (success)
                {
                    MessageBox.Show("�������� ������� ������!", "���������", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "������", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonCreateTable_Click(object sender, EventArgs e)
        {
            // �������� ������� �������
            string projectDirectory = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName;

            // �������� ���� � �����
            string filePath = Path.Combine(projectDirectory, "EmployeeTable.docx");
            string documentTitle = "������� �����������";

            // ������ ����������
            List<HeaderGroup> headerGroups = new List<HeaderGroup>
            {
                new HeaderGroup("ID", 1, null),
                new HeaderGroup("������ ������", 3, new List<string> { "���", "�������", "�������" }),
                new HeaderGroup("����", 1, null),
                new HeaderGroup("������", 2, new List<string> { "���������", "�������������" })
            };

            // ��������� ������ �������
            List<int> columnWidths = new List<int> { 2, 2, 2, 2, 2, 3, 3 };
            // ������ ������ �����������
            var employees = new List<object>
            {
                new { Id = 1, Name = "����", Surname = "������", Kids = "���", Age = 30, JobTitle = "�������", Department = "����� IT" },
                new { Id = 2, Name = "����", Surname = "������", Kids = "���", Age = 25, JobTitle = "��������", Department = "����� ������" }
            };

            // ������������ ������� � ����� ��������
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

            // ����� ������ ��� �������� ���������
            try
            {
                tableComponent1.CreateDocument(filePath, documentTitle, headerGroups, columnWidths, employees, propertyMapping);
                MessageBox.Show("�������� ������� ������!", "�����", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"������ ��� �������� ���������: {ex.Message}", "������", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

    }
}
