using Xceed.Document.NET;
using Xceed.Words.NET;

namespace LabLibrary2
{
    public partial class TableComponent : System.ComponentModel.Component
    {
        public TableComponent()
        {
            InitializeComponent();
        }

        public TableComponent(System.ComponentModel.IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }

        public void CreateDocument(string filePath, string documentTitle,
                          List<HeaderGroup> headerGroups, // Группы заголовков
                          List<int> columnWidths,    // Ширина столбцов
                          List<object> data,                      // Данные
                          Dictionary<int, string> propertyMapping // Соответствие колонок и полей объекта
                          )
        {
            // Проверка на пустоту входных данных
            if (string.IsNullOrEmpty(filePath) || headerGroups == null || headerGroups.Count == 0
                || data == null || data.Count == 0 || propertyMapping == null || propertyMapping.Count == 0)
            {
                throw new ArgumentException("Входные данные не могут быть пустыми.");
            }

            // Проверка заданной ширины столбцов
            if (propertyMapping.Count != columnWidths.Count)
            {
                throw new ArgumentException("Ширина для столбцов задана неправильно.");
            }

            // Дополнительная проверка для ширины столбцов
            if (columnWidths.Any(width => width <= 0))
            {
                throw new ArgumentException("Ширина столбцов должна быть положительной.");
            }

            // Проверка что объединенные ячейки не накладываются друг на друга
            int totalColumns = headerGroups.Sum(group => group.ColumnSpan);
            if (totalColumns > propertyMapping.Count)
            {
                throw new ArgumentException("Объединенные ячейки превышают количество доступных столбцов.");
            }

            // Создание документа
            using (var document = DocX.Create(filePath))
            {
                // Добавление заголовка документа
                document.InsertParagraph(documentTitle).FontSize(20).Bold().Alignment = Alignment.center;

                // Определение количества строк и столбцов для таблицы
                int rowCount = data.Count + 2; // Добавляем 2 строки для заголовков
                int columnCount = propertyMapping.Count; // Количество столбцов соответствует количеству полей


                // Создание таблицы
                var table = document.InsertTable(rowCount, columnCount);
                table.Alignment = Alignment.center;

                // Настройка ширины колонок
                for (int col = 0; col < columnCount; col++)
                {
                    table.SetColumnWidth(col, columnWidths[col] * 30); // Перевод ширины в пункты
                }
                var firstRowColumn = 0;
                var secondRowColumn = 0;

                foreach (var headerGroup in headerGroups)
                {
                    if (string.IsNullOrEmpty(headerGroup.Title))
                    {
                        throw new ArgumentException("Все заголовки групп должны быть заполнены.");
                    }
                    if (headerGroup.ColumnSpan > 1)
                    {
                        if (headerGroup.SubHeaders == null || headerGroup.SubHeaders.Count != headerGroup.ColumnSpan)
                        {
                            throw new ArgumentException($"Для группы {headerGroup.Title} подзаголовков должно быть столько же, сколько указано в ColumnSpan.");
                        }
                        firstRowColumn += headerGroup.ColumnSpan;
                    }
                    else
                    {
                        table.Rows[0].Cells[firstRowColumn].Paragraphs[0].Append(headerGroup.Title).Bold().Alignment = Alignment.center;
                        table.MergeCellsInColumn(firstRowColumn, 0, 1);
                        firstRowColumn++;
                    }
                }

                firstRowColumn = 0;

                foreach (var headerGroup in headerGroups) {
                    if (headerGroup.ColumnSpan > 1)
                    {
                        table.Rows[0].Cells[firstRowColumn].Paragraphs[0].Append(headerGroup.Title).Bold().Alignment = Alignment.center;
                        table.Rows[0].MergeCells(firstRowColumn, firstRowColumn + headerGroup.ColumnSpan - 1);
                        firstRowColumn++;
                        foreach (var header in headerGroup.SubHeaders)
                        {
                            table.Rows[1].Cells[secondRowColumn].Paragraphs[0].Append(header).Bold().Alignment = Alignment.center;
                            secondRowColumn++;
                        }
                    }
                    else 
                    {
                        firstRowColumn++;
                        secondRowColumn++;
                    }
                }

                // Заполнение таблицы данными
                for (int i = 0; i < data.Count; i++)
                {
                    var item = data[i];
                    for (int col = 0; col < columnCount; col++)
                    {
                        // Проверяем, что свойство существует в объекте
                        if (propertyMapping.ContainsKey(col))
                        {
                            var propertyName = propertyMapping[col];
                            var property = item.GetType().GetProperty(propertyName);
                            
                            if (property != null)
                            {
                                var value = property.GetValue(item)?.ToString() ?? "";
                                table.Rows[i + 2].Cells[col].Paragraphs[0].Append(value);
                                
                            }
                            else
                            {
                                throw new ArgumentException($"Свойство {propertyName} не найдено в объекте данных.");
                            }
                        }
                    }
                }

                // Сохранение документа
                document.Save();
            }
        }

    }
    public class HeaderGroup
    {
        public string Title { get; set; } // Название группы
        public int ColumnSpan { get; set; } // Количество объединяемых столбцов
        public List<string> SubHeaders { get; set; } // Подзаголовки для данной группы

        public HeaderGroup(string title, int columnSpan, List<string> subHeaders)
        {
            Title = title;
            ColumnSpan = columnSpan;
            SubHeaders = subHeaders;
        }
    }
}
