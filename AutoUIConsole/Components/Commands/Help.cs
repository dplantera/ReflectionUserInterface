﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;

namespace AutoUIConsole.Components
{
    public partial class Commands
    {
        public void help()
        {
            Console.Clear();
            var tableLines = GenerateTable();
            tableLines.ForEach(x => Helper.WriteLine(x));
        }

        //TODO: Just Proof of work, need massive refactoring
        private List<string> GenerateTable()
        {
            XmlDocument helpDoc = new XmlDocument();
            helpDoc.Load(Config.MenuTexts.Storage("help.xml"));

            var tableRows = new List<List<string>>();
            var headerItems = RowItems(helpDoc, "header", "Name");
            var headerLineItems = new[] { headerItems[0][0], headerItems[1][0] };

            tableRows.AddRange(RowItems(helpDoc, "listItems", headerLineItems));

            var lines = new List<string>();
            List<(string column1, string column2)> columns = new List<(string column1, string column2)>();

            foreach (List<string> tableRow in tableRows)
            {
                var firstColumn = ((List<string>)typeof(Config.Commands).GetField(tableRow.First()).GetValue(null)).ToText();
                var secondColumn = tableRow[1];

                columns.Add((firstColumn, secondColumn));
            }

            int maxWidthColumn1 = columns.Max(x => x.column1.Length) + 1;
            int maxWidthColumn2 = columns.Max(x => x.column2.Length) + 2;
            string headerOffsetColumn1 = string.Format($"{" ".PadLeft((maxWidthColumn1 - headerLineItems[0].Length) / 2)}");
            string headerOffsetColumn2 = string.Format($"{" ".PadLeft((maxWidthColumn2 - headerLineItems[1].Length) / 2)}");

            lines.Add(string.Format($"-{"".PadLeft(maxWidthColumn1, '-') }") + "-" + string.Format($"{"".PadLeft(maxWidthColumn2, '-') }"));
            lines.Add(GenerateLine(maxWidthColumn1, headerOffsetColumn1 + headerLineItems[0], headerOffsetColumn2 + headerLineItems[1]));
            lines.Add(string.Format($"-{"".PadLeft(maxWidthColumn1, '-') }") + "-" + string.Format($"{"".PadLeft(maxWidthColumn2, '-') }"));
            columns.ForEach(x => lines.Add(GenerateLine(maxWidthColumn1, x.column1, x.column2)));

            string header = "Help Menu";

            if (header.Length > lines[0].Length) throw new IndexOutOfRangeException("Die Ueberschrifft ist zu lang");

            int lengthHeaderBox = (lines[0].Length - header.Length) / 2;
            string innerOffset = string.Format($"{"".PadLeft(lengthHeaderBox / 2)}");


            string outerOffset = string.Format($"{"".PadLeft((lines[0].Length - (lengthHeaderBox + header.Length)) / 2)}");

            Helper.WriteLine(string.Format(outerOffset + $"*{"".PadLeft(innerOffset.Length * 2 + header.Length, '*') }*"));
            Helper.WriteLine(string.Format(outerOffset + "|" + innerOffset + $"{header}" + innerOffset + "|"));
            Helper.WriteLine(string.Format(outerOffset + $"*{"".PadLeft(innerOffset.Length * 2 + header.Length, '*') }*" + Environment.NewLine));


            return lines;
        }

        private List<List<string>> RowItems(XmlDocument helpDoc, string xmlNodeName, params string[] attributNames)
        {
            var items = new List<List<string>>();

            var childNodes = helpDoc?.DocumentElement?.GetElementsByTagName(xmlNodeName).Item(0)?.ChildNodes;

            foreach (XmlNode child in childNodes)
            {
                var row = new List<string>();

                foreach (string attributName in attributNames)
                {
                    row.Add(child.Attributes?[attributName].Value);
                }

                items.Add(row);
            }
            return items;
        }

        public void h() => help();

        private string GenerateLine(int witdh1, params string[] columnns)
        {
            var line = " ";

            foreach (string columnn in columnns)
            {
                line = line + string.Format($"{columnn.PadRight(witdh1, ' ')}| ");
            }
            return line.Length < 1 ? string.Empty : line.Substring(0, line.Length - 2);
        }
    }
}