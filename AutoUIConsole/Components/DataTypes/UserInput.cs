﻿using AutoUIConsole.Components.Commands;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace AutoUIConsole.Components.DataTypes
{
    public class UserInput
    {
        public LinkedList<UserInput> Arguments { get; set; }

        public string Content { get; set; }

        public bool IsEmpty { get; private set; }
        public bool IsMultiArgument { get; private set; }
        public bool IsCommand { get; private set; }
        public bool IsNumber { get; private set; }

        private bool _firstCall = true;

        public UserInput(params string[] input)
        {
            input = SplitInput(input);
            Content = input.Length > 0 ? input[0] : string.Empty;

            Arguments = ExtractArguments(input);

            if (!_firstCall && !IsEmpty && !IsCommand) Arguments.AddFirst(this);
            EvaluateInput();

            if (_firstCall) _firstCall = false;

        }

        private string[] SplitInput(string[] input)
        {
            if (input.Length == 1) return input[0].Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            return input;
        }

        private void EvaluateInput()
        {
            IsEmpty = Content.Equals(string.Empty);
            IsMultiArgument = Arguments.Count > 1;

            if (IsMultiArgument) return;

            IsCommand = CheckIsCommand(Content);

            if (!IsCommand) IsNumber = Regex.IsMatch(Content, @"\b\d+$");
        }

        private LinkedList<UserInput> ExtractArguments(string[] userInput)
        {
            if (userInput.Length <= 1)
            {
                var args = new LinkedList<UserInput>();
                args.AddFirst(this);
                return args;
            }

            string[] subArray = new string[userInput.Length - 1];
            Array.Copy(userInput, 1, subArray, 0, subArray.Length);

            var userIn = new UserInput(subArray);
            Arguments = ExtractArguments(subArray);

            Arguments.AddFirst(userIn);
            return Arguments;
        }


        private bool CheckIsCommand(string content)
        {
            return SuperCommand.AvailableCommands.Contains(content);
        }
    }
}