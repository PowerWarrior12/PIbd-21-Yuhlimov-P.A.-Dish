using DishProjectBusinessLogic.HelperModels;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using System.Collections.Generic;

namespace DishProjectBusinessLogic.BusinessLogics
{
    static class SaveToWord
    {
        /// <summary>
        /// Создание документа
        /// </summary>
        /// <param name="info"></param>
        public static void CreateDoc(WordInfo info)
        {
            using (WordprocessingDocument wordDocument = WordprocessingDocument.Create(info.FileName, WordprocessingDocumentType.Document))
            {
                MainDocumentPart mainPart = wordDocument.AddMainDocumentPart();
                mainPart.Document = new Document();
                Body docBody = mainPart.Document.AppendChild(new Body());
                docBody.AppendChild(CreateParagraph(new WordParagraph
                {
                    Texts = new List<(string, WordTextProperties)> { (info.Title, new WordTextProperties { Bold = true, Size = "24", }) },
                    TextProperties = new WordTextProperties
                    {
                        Size = "24",
                        JustificationValues = JustificationValues.Center
                    }
                }));
                if (info.Dishes != null)
                {
                    foreach (var dish in info.Dishes)
                    {
                        docBody.AppendChild(CreateParagraph(new WordParagraph
                        {
                            Texts = new List<(string, WordTextProperties)> {
                                (dish.DishName, new WordTextProperties {Bold = true, Size = "24", }) ,
                                (" : " + dish.Price.ToString(), new WordTextProperties {Bold = false, Size = "24", })},
                            TextProperties = new WordTextProperties
                            {
                                Size = "24",
                                JustificationValues = JustificationValues.Both
                            }
                        })); ;
                    }
                    docBody.AppendChild(CreateSectionProperties());
                }
                if (info.WareHouses != null)
                {
                    Table tbl = new Table();

                    // Set the style and width for the table.
                    TableProperties tableProp = new TableProperties();
                    TableStyle tableStyle = new TableStyle() { Val = "TableGrid" };

                    tbl.AppendChild(CreateTableBorders());

                    // Make the table width 100% of the page width.
                    TableWidth tableWidth = new TableWidth() { Width = "5000", Type = TableWidthUnitValues.Pct };

                    // Apply
                    tableProp.Append(tableStyle, tableWidth);
                    tbl.AppendChild(tableProp);

                    // Add 3 columns to the table.
                    TableGrid tg = new TableGrid(new GridColumn(), new GridColumn(), new GridColumn());
                    tbl.AppendChild(tg);

                    foreach (var warehouse in info.WareHouses)
                    {
                        tbl.AppendChild(CreateTableRow(new List<string>() { 
                            warehouse.Name,
                            warehouse.FIO,
                            warehouse.DateCreate.ToString()
                        }));
                    }
                    docBody.AppendChild(tbl);
                    docBody.AppendChild(CreateSectionProperties());
                }
                wordDocument.MainDocumentPart.Document.Save();
            }
        }
        /// <summary>
        /// Настройки страницы
        /// </summary>
        /// <returns></returns>
        private static SectionProperties CreateSectionProperties()
        {
            SectionProperties properties = new SectionProperties();
            PageSize pageSize = new PageSize
            {
                Orient = PageOrientationValues.Portrait
            };
            properties.AppendChild(pageSize);
            return properties;
        }
        /// <summary>
        /// Создание абзаца с текстом
        /// </summary>
        /// <param name="paragraph"></param>
        /// <returns></returns>
        private static Paragraph CreateParagraph(WordParagraph paragraph)
        {
            if (paragraph != null)
            {
                Paragraph docParagraph = new Paragraph();

                docParagraph.AppendChild(CreateParagraphProperties(paragraph.TextProperties));
                foreach (var run in paragraph.Texts)
                {
                    Run docRun = new Run();
                    RunProperties properties = new RunProperties();
                    properties.AppendChild(new FontSize { Val = run.Item2.Size });
                    if (run.Item2.Bold)
                    {
                        properties.AppendChild(new Bold());
                    }
                    docRun.AppendChild(properties);
                    docRun.AppendChild(new Text
                    {
                        Text = run.Item1,
                        Space =
                   SpaceProcessingModeValues.Preserve
                    });
                    docParagraph.AppendChild(docRun);
                }
                return docParagraph;
            }
            return null;
        }
        /// <summary>
        /// Задание форматирования для абзаца
        /// </summary>
        /// <param name="paragraphProperties"></param>
        /// <returns></returns>
        private static ParagraphProperties CreateParagraphProperties(WordTextProperties
       paragraphProperties)
        {
            if (paragraphProperties != null)
            {
                ParagraphProperties properties = new ParagraphProperties();
                properties.AppendChild(new Justification()
                {
                    Val = paragraphProperties.JustificationValues
                });
                properties.AppendChild(new SpacingBetweenLines
                {
                    LineRule = LineSpacingRuleValues.Auto
                });
                properties.AppendChild(new Indentation());
                ParagraphMarkRunProperties paragraphMarkRunProperties = new
               ParagraphMarkRunProperties();
                if (!string.IsNullOrEmpty(paragraphProperties.Size))
                {
                    paragraphMarkRunProperties.AppendChild(new FontSize
                    {
                        Val =
                   paragraphProperties.Size
                    });
                }
                properties.AppendChild(paragraphMarkRunProperties);
                return properties;
            }
            return null;
        }

        private static TableRow CreateTableRow(List<string> texts)
        {
            // Create 1 row to the table.
            TableRow tr1 = new TableRow();

            // Add a cell to each column in the row.
            foreach (string text in texts)
            {
                TableCell tc1 = new TableCell(CreateParagraph(new WordParagraph
                {
                    Texts = new List<(string, WordTextProperties)> {
                                (text, new WordTextProperties {Bold = true, Size = "24", })},
                    TextProperties = new WordTextProperties
                    {
                        Size = "24",
                        JustificationValues = JustificationValues.Both
                    }
                }));
                tr1.Append(tc1);
            }
            return tr1;
        }

        private static TableBorders CreateTableBorders()
        {
            TableBorders tableBorders = new TableBorders();

            BottomBorder bottomBorder = new BottomBorder();
            bottomBorder.Val = new EnumValue<BorderValues>(BorderValues.Thick);
            bottomBorder.Color = "000000";

            tableBorders.AppendChild(bottomBorder);

            TopBorder topBorder = new TopBorder();
            topBorder.Val = new EnumValue<BorderValues>(BorderValues.Thick);
            topBorder.Color = "000000";

            tableBorders.AppendChild(topBorder);

            InsideHorizontalBorder insHorBorder = new InsideHorizontalBorder();
            insHorBorder.Val = new EnumValue<BorderValues>(BorderValues.Thick);
            insHorBorder.Color = "000000";

            tableBorders.AppendChild(insHorBorder);

            InsideVerticalBorder insVerBorder = new InsideVerticalBorder();
            insVerBorder.Val = new EnumValue<BorderValues>(BorderValues.Thick);
            insVerBorder.Color = "000000";

            tableBorders.AppendChild(insVerBorder);

            LeftBorder leftBorder = new LeftBorder();
            leftBorder.Val = new EnumValue<BorderValues>(BorderValues.Thick);
            leftBorder.Color = "000000";

            tableBorders.AppendChild(leftBorder);

            RightBorder rightBorder = new RightBorder();
            rightBorder.Val = new EnumValue<BorderValues>(BorderValues.ThickThinMediumGap);
            rightBorder.Color = "000000";

            tableBorders.AppendChild(rightBorder);
            return tableBorders;
        }
    }
}
