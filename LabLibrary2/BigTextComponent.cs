using Xceed.Document.NET;
using Xceed.Words.NET;

namespace LabLibrary2
{
    public partial class BigTextComponent : System.ComponentModel.Component
    {
        public BigTextComponent()
        {
            InitializeComponent();
        }

        public BigTextComponent(System.ComponentModel.IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }

        public bool CreateDocument(string filePath, string documentTitle, List<ParagraphData> paragraphs)
        {
            if (string.IsNullOrEmpty(filePath) || string.IsNullOrEmpty(documentTitle) || paragraphs == null || paragraphs.Count == 0)
            {
                throw new ArgumentException("Недостаточно данных для создания документа.");
            }
            try
            {
                using (var document = DocX.Create(filePath))
                {
                    // Добавляем заголовок
                    document.InsertParagraph(documentTitle).FontSize(20).Bold().Alignment = Alignment.center;

                    // Добавляем абзацы
                    foreach (var paragraph in paragraphs)
                    {
                        document.InsertParagraph(paragraph.Text).FontSize(12).SpacingAfter(10);
                    }

                    document.Save(); // Сохраняем документ
                }

                return true; // Успешно сохранён
            }
            catch (Exception ex)
            {
                throw new IOException("Ошибка при создании документа: " + ex.Message);
            }
        }
    }

    // Класс для хранения абзацев
    public class ParagraphData
    {
        public string Text { get; set; }

        public ParagraphData(string text)
        {
            Text = text;
        }
    }
}
