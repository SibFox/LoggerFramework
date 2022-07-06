using System;
using System.IO;
using System.Collections.Generic;

namespace LoggerFramework
{
    internal delegate string GetFlatStr(string str);

    public sealed class Logger
    {
        string pathToLog;
        List<string> allLines = new List<string>();
        string preparedLine = "";
        bool timeStampAdded = false;
        GetFlatStr getFlatStr = LilHelper.GetFlatStr;

        /// <summary>
        /// Sets current log file location.
        /// </summary>
        /// <param name="path">Path to where log file will be located(without format)</param>
        /// <returns></returns>
        /// <exception cref="LoggerException">Occurs when tries to set log path file when it is already set.</exception>
        public Logger SetLogPath(string path)
        {
            if (pathToLog == null)
            {
                pathToLog = path + ".log";
                return this;
            }
            else
            {
                throw new LoggerException("The path to log file have already been set.");
            }
        }

        /// <summary>
        /// Adds a <see cref="DateTime">timestamp</see> at the start of the line.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="LoggerException">Occurs when you try to add timestamp when it was already added.</exception>
        public Logger AddTimeStamp()
        {
            if (!timeStampAdded)
            {
                preparedLine += $"[{DateTime.Now:HH:mm:ss}] ";
                timeStampAdded = true;
                return this;
            }
            else throw new LoggerException("Timestamp is already added for this line.");
        }

        /// <summary>
        /// Adds a standard line with <see cref="String"/> type.
        /// </summary>
        /// <param name="line"></param>
        /// <returns></returns>
        /// <exception cref="LoggerException">Occurs when you try to add empty line.</exception>
        public Logger AddInfoToLine(string line)
        {
            if (line == null) throw new ArgumentNullException("The line were null.");
            if (getFlatStr(line).Length > 0)
            {
                preparedLine += line + ", ";
                return this;
            }
            else throw new LoggerException("Attempt to add empty line.");
        }

        /// <summary>
        /// Adds a standard line with <see cref="String"/> type and the <see cref="DateTime">timestamp</see> at the begining.
        /// </summary>
        /// <param name="line"></param>
        /// <returns></returns>
        /// <exception cref="LoggerException"></exception>
        public Logger AddInfoToLineWithTime(string line)
        {
            if (line == null) throw new ArgumentNullException("The line were null.");
            if (timeStampAdded) throw new LoggerException("Timestamp is already added for this line.");
            if (getFlatStr(line).Length > 0)
            {
                preparedLine += $"[{DateTime.Now:HH:mm:ss}] ";
                timeStampAdded = true;
                preparedLine += line + ", ";
                return this;
            }
            else throw new LoggerException("Attempt to add empty line.");
        }

        /// <summary>
        /// Adds a standard line with <see cref="String"/> type, but as highlighted information([Header: Object]).
        /// </summary>
        /// <param name="head">Header of the topic.</param>
        /// <param name="line">Highlighted line.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="LoggerException"></exception>
        public Logger AddInfoToLineWithHeader(string head, string line)
        {
            if (head == null) throw new ArgumentNullException("The header were null.");
            if (line == null) throw new ArgumentNullException("The line were null.");
            if (getFlatStr(line).Length > 0 && getFlatStr(head).Length > 0)
            {
                preparedLine += $"{head}: {line}, ";
                return this;
            }
            else throw new LoggerException("Attempt to add empty header or line.");
        }

        #region

        //// ~~~~~~ Object ~~~~~~ ////

        /// <summary>
        /// Converts the array to one single string.
        /// </summary>
        /// <param name="array">Targeted array to handle.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public Logger AddArray(object[] array)
        {
            if (array == null) throw new ArgumentNullException("Targeted array were null.");
            else
            {
                preparedLine += HandleArray(array) + ", ";
                return this;
            }
        }

        /// <summary>
        /// Converts the array to one single string with the initial title.
        /// </summary>
        /// <param name="header">Word to designate a header for an array(without separators)</param>
        /// <param name="array">Targeted array to handle.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="LoggerException"></exception>
        public Logger AddArray(string header, object[] array)
        {
            if (header == null) throw new ArgumentNullException("The header string were null.");
            if (array == null) throw new ArgumentNullException("Targeted array were null.");
            if (header.Replace(" ", "").Replace("<a>", "").Trim().Length < 1) throw new LoggerException("The header string is empty.");
            else
            {
                preparedLine += header + ": " + HandleArray(array) + ", ";
                return this;
            }
        }

        private string HandleArray(object[] array)
        {
            if (array == null) throw new ArgumentNullException("Targeted array were null.");
            string whole = "";
            if (array.Length > 0)
            {
                foreach (object item in array)
                {
                    whole += item.ToString() + "<a>";
                }
                if (whole.Replace(" ", "").Replace("<a>", "").Trim().Length > 0)
                {
                    return whole.Remove(whole.Length - 3);
                }
                else throw new LoggerException("Attempt to add array with empty values.");
            }
            else throw new LoggerException("Attempt to use empty array");
        }

        //// ~~~~~~ String ~~~~~~ ////

        /// <summary>
        /// Converts the array to one single string.
        /// </summary>
        /// <param name="array">Targeted array to handle.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public Logger AddArray(string[] array)
        {
            if (array == null) throw new ArgumentNullException("Targeted array were null.");
            else
            {
                preparedLine += HandleArray(array) + ", ";
                return this;
            }
        }

        /// <summary>
        /// Converts the array to one single string with the initial title.
        /// </summary>
        /// <param name="header">Word to designate a header for an array(without separators)</param>
        /// <param name="array">Targeted array to handle.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="LoggerException"></exception>
        public Logger AddArray(string header, string[] array)
        {
            if (header == null) throw new ArgumentNullException("The header string were null.");
            if (array == null) throw new ArgumentNullException("Targeted array were null.");
            if (header.Replace(" ", "").Replace("<a>", "").Trim().Length < 1) throw new LoggerException("The header string is empty.");
            else
            {
                preparedLine += header + ": " + HandleArray(array) + ", ";
                return this;
            }
        }

        private string HandleArray(string[] array)
        {
            if (array == null) throw new ArgumentNullException("Targeted array were null.");
            string whole = "";
            if (array.Length > 0)
            {
                foreach (string item in array)
                {
                    whole += item + "<a>";
                }
                if (whole.Replace(" ", "").Replace("<a>", "").Trim().Length > 0)
                {
                    return whole.Remove(whole.Length - 3);
                }
                else throw new LoggerException("Attempt to add array with empty values.");
            }
            else throw new LoggerException("Attempt to use empty array");
        }

        //// ~~~~~~ Int ~~~~~~ ////

        /// <summary>
        /// Converts the array to one single string.
        /// </summary>
        /// <param name="array">Targeted array to handle.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public Logger AddArray(int[] array)
        {
            if (array == null) throw new ArgumentNullException("Targeted array were null.");
            else
            {
                preparedLine += HandleArray(array) + ", ";
                return this;
            }
        }

        /// <summary>
        /// Converts the array to one single string with the initial title.
        /// </summary>
        /// <param name="header">Word to designate a header for an array(without separators)</param>
        /// <param name="array">Targeted array to handle.</param>
        /// <returns></returns>
        public Logger AddArray(string header, int[] array)
        {
            if (header == null) throw new ArgumentNullException("The header string were null.");
            if (array == null) throw new ArgumentNullException("Targeted array were null.");
            if (header.Replace(" ", "").Replace("<a>", "").Trim().Length < 1) throw new LoggerException("The header string is empty.");
            else
            {
                preparedLine += header + ": " + HandleArray(array) + ", ";
                return this;
            }
        }

        private string HandleArray(int[] array)
        {
            if (array == null) throw new ArgumentNullException("Targeted array were null.");
            string whole = "";
            if (array.Length > 0)
            {
                foreach (int item in array)
                {
                    whole += item.ToString() + "<a>";
                }
                if (whole.Replace(" ", "").Replace("<a>", "").Trim().Length > 0)
                {
                    return whole.Remove(whole.Length - 3);
                }
                else throw new LoggerException("Attempt to add array with empty values.");
            }
            else throw new LoggerException("Attempt to use empty array");
        }

        //// ~~~~~~ Dictionary string ~~~~~~ ////

        /// <summary>
        /// Converts the dictionary to one single string with key at the start and values as continue.
        /// </summary>
        /// <param name="dict">Targeted dictionary to handle.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public Logger AddDictionaryWArray(Dictionary<string, string[]> dict)
        {
            if (dict == null) throw new ArgumentNullException("Targeted dictionary were null.");
            else
            {
                preparedLine += HandleDictionary(dict) + ", ";
                return this;
            }
        }

        /// <summary>
        /// Converts the dictionary to one single string with key at the start and values as continue with the initial title.
        /// </summary>
        /// <param name="header">Word to designate a header for an array(without separators)</param>
        /// <param name="dict">Targeted dictionary to handle.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="LoggerException"></exception>
        public Logger AddDictionaryWArray(string header, Dictionary<string, string[]> dict)
        {
            if (header == null) throw new ArgumentNullException("The header string were null.");
            if (dict == null) throw new ArgumentNullException("Targeted dictionary were null.");
            if (header.Replace(" ", "").Replace("<a>", "").Trim().Length < 1) throw new LoggerException("The header string is empty.");
            else
            {
                preparedLine += header + ": " + HandleDictionary(dict) + ", ";
                return this;
            }
        }

        private string HandleDictionary(Dictionary<string, string[]> dict)
        {
            if (dict == null) throw new ArgumentNullException("Targeted dictionary were null.");
            string whole = "";
            if (dict.Count > 0)
            {
                foreach (string key in dict.Keys)
                {
                    if (key != null && getFlatStr(key).Length > 0)
                    {
                        whole += key + ":~ ";
                        foreach (string value in dict[key])
                        {
                            if (value != null && getFlatStr(value).Length > 0)
                            {
                                whole += value + "<a>";
                            }
                            else throw new LoggerException("The value were null or empty.");
                        }
                        whole.Remove(whole.Length - 3);
                        whole += "\n";
                    }
                    else throw new LoggerException("The key were null or empty.");
                }
                if (getFlatStr(whole).Length > 0) return whole.TrimEnd();
                else throw new LoggerException("Attempt to add empty dictionary.");
            }
            else throw new LoggerException("Attempt to use empty dictionary");
        }

        #endregion

        /// <summary>
        /// Use for more precise setting the line.
        /// </summary>
        /// <returns></returns>
        public InDeepLine AddPreciseInfo()
        {
            return new InDeepLine(this);
        }

        public sealed class InDeepLine
        {
            Logger logger;
            string wholeLine = "";
            string fromMethod;
            string fromObject;
            List<string> highlightHolder = new List<string>();
            string deppLine = "";
            GetFlatStr getFlatStr = LilHelper.GetFlatStr;

            public InDeepLine(Logger logger) => this.logger = logger;

            /// <summary>
            /// Adds a method name from which the line is added.
            /// </summary>
            /// <param name="methodName">Name of the method.</param>
            /// <returns></returns>
            /// <exception cref="LoggerException">Occurs when you try to add method name when it was already added.</exception>
            public InDeepLine AddFromMethod(string methodName)
            {
                if (fromMethod == null)
                {
                    fromMethod = "Method/" + methodName;
                    return this;
                }
                else throw new LoggerException("From method name is already set.");
            }

            /// <summary>
            /// Adds name of the sender object. Mostly used for WinForms.
            /// </summary>
            /// <param name="obj">Name of the sender object.</param>
            /// <returns></returns>
            /// <exception cref="LoggerException">Occurs when you try to add object name when it was already added.</exception>
            public InDeepLine AddFromObject(object obj)
            {
                if (obj is null) throw new ArgumentNullException("Sender object were null.");

                if (fromObject == null)
                {
                    fromObject = "Object/" + obj.ToString();
                    return this;
                }
                else throw new LoggerException("From object name is already set.");
            }

            /// <summary>
            /// Used to highlight parts from which the information comes.
            /// </summary>
            /// <param name="light">Name of the parent method or object.</param>
            /// <param name="obj">Name of the sender object.</param>
            /// <returns></returns>
            /// <exception cref="ArgumentNullException">Occurs when you try to use <see cref="Nullable"/> <see cref="String"/></exception>
            public InDeepLine AddHighlight(string light, string obj)
            {
                if (light == null) throw new ArgumentNullException("Highlight string were null.");
                if (obj == null) throw new ArgumentNullException("Sender object name were null.");
                highlightHolder.Add(light + "/" + obj);
                return this;
            }

            /// <summary>
            /// Adds a standard line with <see cref="String"/> type.
            /// </summary>
            /// <param name="line"></param>
            /// <returns></returns>
            /// <exception cref="LoggerException">Occurs when you try to add empty line.</exception>
            public InDeepLine AddInfoToLine(string line)
            {
                if (getFlatStr(line).Length > 0)
                {
                    deppLine += line + ", ";
                    return this;
                }
                else throw new LoggerException("Attempt to add empty line.");
            }

            /// <summary>
            /// Adds a standard line with <see cref="String"/> type, but as highlighted information([Header: Object]).
            /// </summary>
            /// <param name="head">Header of the topic.</param>
            /// <param name="line">Highlighted line.</param>
            /// <returns></returns>
            /// <exception cref="ArgumentNullException"></exception>
            /// <exception cref="LoggerException"></exception>
            public InDeepLine AddInfoToLineWithHeader(string head, string line)
            {
                if (head == null) throw new ArgumentNullException("The header were null.");
                if (line == null) throw new ArgumentNullException("The line were null.");
                if (getFlatStr(line).Length > 0 && getFlatStr(head).Length > 0)
                {
                    deppLine += $"{head}: {line}, ";
                    return this;
                }
                else throw new LoggerException("Attempt to add empty header or line.");
            }
            private void AddingFunc()
            {
                if (logger.preparedLine.Length < 1)
                    wholeLine += $"[{DateTime.Now:HH:mm:ss}] ";
                if (fromMethod != null) wholeLine += $"[{fromMethod}] ";
                if (fromObject != null) wholeLine += $"[{fromObject}] ";
                if (highlightHolder.Count > 0) foreach (string hold in highlightHolder) { wholeLine += $"[{hold}] "; }
                if (getFlatStr(wholeLine).Length > $"[{DateTime.Now:HH:mm:ss}] ".Length && (fromMethod != null || fromObject != null || highlightHolder.Count > 0))
                    wholeLine = wholeLine.Remove(wholeLine.Length - 1) + ": ";
                if (deppLine != "") wholeLine += deppLine;
                logger.AddInfoToLine(wholeLine.Remove(wholeLine.Length - 2));
            }

            /// <summary>
            /// Adds the result line to global list of all added lines and retuns you to previous <see cref="Logger"/> class.
            /// </summary>
            /// <returns>Earlier modified <see cref="Logger"/> class.</returns>
            /// <exception cref="LoggerException">Occurs when the main info line is empty.</exception>
            public Logger AcceptDeepLine()
            {
                AddingFunc();
                return logger;
            }

            /// <summary>
            /// Adds the result line to global list of all added lines, wraps the line and returns you to previous <see cref="Logger"/> class.
            /// </summary>
            /// <returns>Earlier modified <see cref="Logger"/> class with wrapped line.</returns>
            public Logger AcceptWholeLine()
            {
                AddingFunc();
                logger.AcceptWholeLine();
                return logger;
            }

            /// <summary>
            /// Records the log file with all lines that've been added.
            /// <para>Can be used at the end without accepting the line.</para>
            /// </summary>
            /// <exception cref="LoggerException">Occurs when the main info line is empty.</exception>
            public void CreateLog()
            {
                AddingFunc();
                logger.CreateLog();
            }
        }

        /// <summary>
        /// Adds the result line to global list of all added lines.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="LoggerException"></exception>
        public Logger AcceptWholeLine()
        {
            if (preparedLine == "") throw new LoggerException("Attempt to add empty line.");
            if (getFlatStr(preparedLine).Length > 0)
            {
                string forAdd = preparedLine;
                if (getFlatStr(preparedLine).Length > 11) forAdd = forAdd.Remove(forAdd.Length - 2);
                allLines.Add(forAdd);
                timeStampAdded = false;
                preparedLine = "";
                return this;
            }
            else throw new LoggerException("Prepared line is empty.");
        }

        /// <summary>
        /// Records the log file with all lines that've been added.
        /// <para>Can be used at the end without accepting the line.</para>
        /// </summary>
        /// <param name="mode">Usualy appends new info to an existing file. For recreating a file use 1.</param>
        /// <exception cref="LoggerException">Occurs when you try to add empty log.</exception>
        public void CreateLog(int mode = 0)
        {
            string[] separatedPath = pathToLog.Split('/');
            string folderPath = "";
            if (separatedPath.Length > 1)
            for (int i = 0; i < separatedPath.Length - 1; i++) folderPath += separatedPath[i];
            if (folderPath != "") if (!Directory.Exists(folderPath)) Directory.CreateDirectory(folderPath);

            string forAdd = preparedLine;
            if (getFlatStr(preparedLine).Length > 11 && timeStampAdded) forAdd = forAdd.Remove(forAdd.Length - 2);
            else if (getFlatStr(preparedLine).Length > 0 && !timeStampAdded) forAdd = forAdd.Remove(forAdd.Length - 2);
            if (getFlatStr(forAdd).Length > 0) allLines.Add(forAdd);
            if (allLines.Count < 1)
            {
                throw new LoggerException("Attempt to add empty log.");
            }
            else
            {
                FileMode fileMode;
                switch (mode)
                {
                    case 1: fileMode = FileMode.Create; break;
                    default: fileMode = FileMode.Append; break;
                }
                using (FileStream fs = new FileStream(pathToLog, fileMode, FileAccess.Write))
                {
                    using (StreamWriter sw = new StreamWriter(fs))
                    {
                        foreach (string line in allLines)
                        {
                            sw.WriteLine(line.Replace("<a>", "; ").Trim());
                            Console.WriteLine(line.Replace("<a>", "; ").Trim());
                        }
                    }
                }
                pathToLog = null;
                allLines.Clear();
                preparedLine = "";
                timeStampAdded = false;
            }
        }
    }


    public class LoggerException : Exception
    {
        public LoggerException()
        {
        }

        public LoggerException(string message)
            : base(message)
        {
        }

        public LoggerException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }

    internal static class LilHelper
    {
        internal static string GetFlatStr(string str) => str.Replace("<a>", "").Replace(":~", "").Replace("\r", "").Replace("\n", "").Replace(" ", "").Trim();
    }
}
