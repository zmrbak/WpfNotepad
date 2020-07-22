using System;
using System.Windows;
using WpfNotepad.Models;

namespace WpfNotepad.Utility
{
    public static class Tools
    {
        public static void OnFindTextCommand(DocumentModel documentModel, Direction direction = Direction.None)
        {
            //查找开始点
            int indexStart = 0;

            //找点的地方
            int indexFind = 0;

            switch (direction)
            {
                case Direction.None:
                    //向上查找
                    if (documentModel.IsFindUp)
                    {
                        FindUp();
                    }
                    //向下查找
                    else
                    {
                        FindDown();
                    }
                    break;

                case Direction.Up:
                    FindUp();

                    break;

                case Direction.Down:
                    FindDown();
                    break;

                default:
                    break;
            }

            if (indexFind == -1)
            {
                MessageBox.Show("找不到“" + documentModel.FindText + "”", "记事本", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            documentModel.TextBox.Select(indexFind, documentModel.FindText.Length);
            documentModel.TextBox.Focus();

            void FindUp()
            {
                indexStart = documentModel.TextBox.SelectionStart;
                indexFind = documentModel.TextBox.Text.LastIndexOf(
                    documentModel.FindText,
                    indexStart,
                    documentModel.IsFindCaseSensitive ? StringComparison.Ordinal : StringComparison.OrdinalIgnoreCase
                    );

                if (indexFind == -1)
                {
                    if (documentModel.IsFindCirculated)
                    {
                        documentModel.TextBox.ScrollToEnd();
                        indexFind = documentModel.TextBox.Text.LastIndexOf(
                                           documentModel.FindText,
                                           documentModel.TextBox.Text.Length,
                                           documentModel.IsFindCaseSensitive ? StringComparison.Ordinal : StringComparison.OrdinalIgnoreCase
                                           );
                    }
                }
            }

            void FindDown()
            {
                indexStart = documentModel.TextBox.SelectionStart + documentModel.FindText.Length;
                indexFind = documentModel.TextBox.Text.IndexOf(
                    documentModel.FindText,
                    indexStart,
                    documentModel.IsFindCaseSensitive ? StringComparison.Ordinal : StringComparison.OrdinalIgnoreCase
                    );

                if (indexFind == -1)
                {
                    if (documentModel.IsFindCirculated)
                    {
                        documentModel.TextBox.ScrollToHome();
                        indexFind = documentModel.TextBox.Text.IndexOf(
                                           documentModel.FindText,
                                           0,
                                           documentModel.IsFindCaseSensitive ? StringComparison.Ordinal : StringComparison.OrdinalIgnoreCase
                                           );
                    }
                }
            }
        }
    }

    public enum Direction
    {
        None,
        Up,
        Down
    }
}