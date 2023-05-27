﻿namespace Kuaniminka.KobFlow.ToolBox
{
    public class LogToolFactory
    {
        public static LogToolFactory New { get => new LogToolFactory(); }

        public IKLogTool Create()
        {
            IKLogTool result = new ConsoleLogTool(new KNewtonJSonParser());
            return result;

        }
    }
}