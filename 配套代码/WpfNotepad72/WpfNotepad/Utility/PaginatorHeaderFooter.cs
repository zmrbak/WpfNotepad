using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Media;
using WpfNotepad.Models;

namespace WpfNotepad.Utility
{
    public class PaginatorHeaderFooter : DocumentPaginator
    {
        public PaginatorHeaderFooter(DocumentPaginator documentPaginator, DocumentModel documentModel)
        {
            DocumentModel = documentModel;
            this.documentPaginator = documentPaginator;

            headerText = ReplaceCtrlChar(DocumentModel.PaperHeader);
            footerText = ReplaceCtrlChar(DocumentModel.PaperFooter);

            List<int> headerCtrlCharIndex = new List<int>();
            headerCtrlCharIndex.Add(headerText.Length);
            headerCtrlCharIndex = headerCtrlCharIndex.Concat(CheckCtrlChar(headerText, "&l")).ToList();
            headerCtrlCharIndex = headerCtrlCharIndex.Concat(CheckCtrlChar(headerText, "&c")).ToList();
            headerCtrlCharIndex = headerCtrlCharIndex.Concat(CheckCtrlChar(headerText, "&r")).ToList();
            headerCtrlCharIndex.Sort();

            List<int> footerCtrlCharIndex = new List<int>();
            footerCtrlCharIndex.Add(footerText.Length);
            footerCtrlCharIndex = footerCtrlCharIndex.Concat(CheckCtrlChar(footerText, "&l")).ToList();
            footerCtrlCharIndex = footerCtrlCharIndex.Concat(CheckCtrlChar(footerText, "&c")).ToList();
            footerCtrlCharIndex = footerCtrlCharIndex.Concat(CheckCtrlChar(footerText, "&r")).ToList();
            footerCtrlCharIndex.Sort();

            int index = 0;
            foreach (var item in headerCtrlCharIndex)
            {
                string data = headerText.Substring(index, item - index);
                index = item;

                if (data.StartsWith("&l"))
                {
                    headerAndL += data.Substring(2);
                }
                else if (data.StartsWith("&c"))
                {
                    headerAndC += data.Substring(2);
                }
                else if (data.StartsWith("&r"))
                {
                    headerAndR += data.Substring(2);
                }
            }

            index = 0;
            foreach (var item in footerCtrlCharIndex)
            {
                string data = footerText.Substring(index, item - index);
                index = item;

                if (data.StartsWith("&l"))
                {
                    footerAndL += data.Substring(2);
                }
                else if (data.StartsWith("&c"))
                {
                    footerAndC += data.Substring(2);
                }
                else if (data.StartsWith("&r"))
                {
                    footerAndR += data.Substring(2);
                }
            }
        }

        private string headerText;
        private string footerText;
        private string headerAndL = "";
        private string headerAndC = "";
        private string headerAndR = "";
        private string footerAndL = "";
        private string footerAndC = "";
        private string footerAndR = "";

        private string ReplaceCtrlChar(string text)
        {
            //全部换成小写
            text = text.Replace("&L", "&l");
            text = text.Replace("&C", "&c");
            text = text.Replace("&R", "&r");
            text = text.Replace("&D", "&d");
            text = text.Replace("&T", "&t");
            text = text.Replace("&F", "&f");
            text = text.Replace("&P", "&p");

            //替换
            //&d         打印当前日期 
            text = text.Replace("&d", DateTime.Now.ToString("yyyy年MM月dd日"));
            //&t         打印当前时间 
            text = text.Replace("&t", DateTime.Now.ToString("HH:mm:ss"));
            //&f         打印文档名称 
            text = text.Replace("&f", DocumentModel.FileName);
            //默认居中
            return "&c" + text;
        }

        private List<int> CheckCtrlChar(string text, string ctrlChar)
        {
            List<int> ctrlCharIndex = new List<int>();
            int start = 0;
            do
            {
                start = text.IndexOf(ctrlChar, start);
                if (start == -1) break;

                ctrlCharIndex.Add(start);
                start += 2;

            } while (start < text.Length);

            return ctrlCharIndex;
        }


        private readonly DocumentPaginator documentPaginator;

        public override bool IsPageCountValid => documentPaginator.IsPageCountValid;

        public override int PageCount => documentPaginator.PageCount;

        public override Size PageSize { get => documentPaginator.PageSize; set => documentPaginator.PageSize = value; }

        public override IDocumentPaginatorSource Source => documentPaginator.Source;

        public DocumentModel DocumentModel { get; }

        public override DocumentPage GetPage(int pageNumber)
        {
            DocumentPage documentPage = documentPaginator.GetPage(pageNumber);
            ContainerVisual containerVisual = new ContainerVisual();

            //页眉页脚字体
            Typeface typeface = new Typeface(new FontFamily("微软雅黑"), FontStyles.Normal, FontWeights.Bold, FontStretches.Normal);
            if (DocumentModel.PaperHeader.Length > 0)
            {
                DrawingVisual header = new DrawingVisual();
                using (DrawingContext ctx = header.RenderOpen())
                {
                    //处理页码
                    headerAndL = headerAndL.Replace("&p", " 第" + (pageNumber + 1) + "页 ");
                    headerAndC = headerAndC.Replace("&p", " 第" + (pageNumber + 1) + "页 ");
                    headerAndR = headerAndR.Replace("&p", " 第" + (pageNumber + 1) + "页 ");

                    //文本
                    FormattedText headerAndLText = new FormattedText(headerAndL, CultureInfo.CurrentCulture, FlowDirection.LeftToRight, typeface, 16, Brushes.Black, 96);
                    FormattedText headerAndCText = new FormattedText(headerAndC, CultureInfo.CurrentCulture, FlowDirection.LeftToRight, typeface, 16, Brushes.Black, 96);
                    FormattedText headerAndRText = new FormattedText(headerAndR, CultureInfo.CurrentCulture, FlowDirection.LeftToRight, typeface, 16, Brushes.Black, 96);

                    //写文本
                    //靠左
                    ctx.DrawText(headerAndLText, new Point(documentPage.ContentBox.Left, documentPage.ContentBox.Top - 20));
                    //靠右
                    ctx.DrawText(headerAndRText, new Point(documentPage.ContentBox.Right - headerAndRText.Width, documentPage.ContentBox.Top - 20));
                    //居中
                    ctx.DrawText(headerAndCText, new Point((documentPage.ContentBox.Left + documentPage.ContentBox.Right) / 2 - headerAndCText.Width / 2, documentPage.ContentBox.Top - 20));

                    //划线
                    ctx.DrawLine(
                        new Pen(Brushes.Black, 0.5),
                        new Point(documentPage.ContentBox.Left, documentPage.ContentBox.Top),
                        new Point(documentPage.ContentBox.Right, documentPage.ContentBox.Top)
                        );
                }
                containerVisual.Children.Add(header);
            }

            //页脚
            if (DocumentModel.PaperFooter.Length > 0)
            {
                DrawingVisual footer = new DrawingVisual();
                using (DrawingContext ctx = footer.RenderOpen())
                {
                    //处理页码
                    footerAndL = footerAndL.Replace("&p", " 第" + (pageNumber + 1) + "页 ");
                    footerAndC = footerAndC.Replace("&p", " 第" + (pageNumber + 1) + "页 ");
                    footerAndR = footerAndR.Replace("&p", " 第" + (pageNumber + 1) + "页 ");

                    //文本
                    FormattedText footerAndLText = new FormattedText(footerAndL, CultureInfo.CurrentCulture, FlowDirection.LeftToRight, typeface, 16, Brushes.Black, 96);
                    FormattedText footerAndRText = new FormattedText(footerAndR, CultureInfo.CurrentCulture, FlowDirection.LeftToRight, typeface, 16, Brushes.Black, 96);
                    FormattedText footerAndCText = new FormattedText(footerAndC, CultureInfo.CurrentCulture, FlowDirection.LeftToRight, typeface, 16, Brushes.Black, 96);

                    //写文本
                    //靠左
                    ctx.DrawText(footerAndLText, new Point(documentPage.ContentBox.Left, documentPage.ContentBox.Bottom));
                    //靠右
                    ctx.DrawText(footerAndRText, new Point(documentPage.ContentBox.Right - footerAndRText.Width, documentPage.ContentBox.Bottom));
                    //居中
                    ctx.DrawText(footerAndCText, new Point((documentPage.ContentBox.Left + documentPage.ContentBox.Right) / 2 - footerAndCText.Width / 2, documentPage.ContentBox.Bottom));

                    ctx.DrawLine(
                        new Pen(Brushes.Black, 0.5),
                        new Point(documentPage.ContentBox.Left, documentPage.ContentBox.Bottom),
                        new Point(documentPage.ContentBox.Right, documentPage.ContentBox.Bottom));
                }
                containerVisual.Children.Add(footer);
            }

            containerVisual.Children.Add(documentPage.Visual);

            return new DocumentPage(containerVisual, documentPage.Size, documentPage.BleedBox, documentPage.ContentBox);
        }
    }
}
