using System;

namespace DishProjectBusinessLogic.Attributes
{
    public class ColumnAttribute : Attribute
    {
        public ColumnAttribute(string title = "", bool visible = true, int width = 0,
       GridViewAutoSize gridViewAutoSize = GridViewAutoSize.None,FormatsEnum format = FormatsEnum.None)
        {
            Title = title;
            Visible = visible;
            Width = width;
            GridViewAutoSize = gridViewAutoSize;
            switch (format)
            {
                case FormatsEnum.None:
                    DateFormat = null;
                    break;
                case FormatsEnum.FirstFormat:
                    DateFormat = "dd.mm.yyyy - HH:mm";
                    break;
                case FormatsEnum.SecondFormat:
                    DateFormat = "dd.mm.yyyy";
                    break;
            }
        }
        public string Title { get; private set; }
        public bool Visible { get; private set; }
        public int Width { get; private set; }
        public string DateFormat { get; private set; }
        public GridViewAutoSize GridViewAutoSize { get; private set; }
    }
}
