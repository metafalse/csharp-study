using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.IO;

namespace RegularExpressions_Examples
{
    class Program
    {


        #region "Verifying the Syntax of a Regular Expression"

        public static void TestUserInputRegEx(string regEx)
        {
            if (VerifyRegEx(regEx))
                Console.WriteLine("This is a valid regular expression.");
            else
                Console.WriteLine("This is not a valid regular expression.");
        }

        public static bool VerifyRegEx(string testPattern)
        {
            bool isValid = true;

            if ((testPattern != null) && (testPattern.Length > 0))
            {
                try
                {
                    Regex.Match("", testPattern);
                }
                catch (ArgumentException ae)
                {
                    // BAD PATTERN: Syntax error
                    isValid = false;
                    Console.WriteLine(ae);
                }
            }
            else
            {
                //BAD PATTERN: Pattern is null or empty
                isValid = false;
            }

            return (isValid);
        }

        #endregion

        #region "Quickly Finding Only The Last Match In A String"
        public static void TestFindLast()
        {
            Match theMatch = Regex.Match("one two three two one", "two", RegexOptions.RightToLeft);
            Console.WriteLine(theMatch.Value);

            Regex RE = new Regex("two", RegexOptions.RightToLeft);
            theMatch = RE.Match("one two three two one");
            Console.WriteLine(theMatch.Value);
        }
        #endregion

        #region "Augmenting the Basic String Replacement Function"
        public static string MatchHandler(Match theMatch)
        {
            // Handle Top property of the Property tag
            if (theMatch.Value.StartsWith("<Property", StringComparison.Ordinal))
            {
                long topPropertyValue = 0;

                // Obtain the numeric value of the Top property
                Match topPropertyMatch = Regex.Match(theMatch.Value, "Top=\"([-]*\\d*)");
                if (topPropertyMatch.Success)
                {
                    if (string.IsNullOrEmpty(topPropertyMatch.Groups[1].Value.Trim()))
                    {
                        // If blank, set to zero
                        return (theMatch.Value.Replace("Top=\"\"", "Top=\"0\""));
                    }
                    else if (topPropertyMatch.Groups[1].Value.Trim().Equals("-"))
                    {
                        // If only a negative sign (syntax error), set to zero
                        return (theMatch.Value.Replace("Top=\"-\"", "Top=\"0\""));
                    }
                    else
                    {
                        // We have a valid number
                        // Convert the matched string to a numeric value
                        topPropertyValue = long.Parse(topPropertyMatch.Groups[1].Value,
                            System.Globalization.NumberStyles.Any);

                        // If the Top property is out of the specified range, set it to zero
                        if (topPropertyValue < 0 || topPropertyValue > 5000)
                        {
                            return (theMatch.Value.Replace("Top=\"" + topPropertyValue + "\"",
                                "Top=\"0\""));
                        }
                    }
                }
            }

            return (theMatch.Value);
        }

        public static void ComplexReplace(string matchPattern, string source)
        {
            MatchEvaluator replaceCallback = new MatchEvaluator(MatchHandler);
            string newString = Regex.Replace(source, matchPattern, replaceCallback);

            Console.WriteLine("Replaced String = " + newString);
        }

        public static void TestComplexReplace()
        {
            string matchPattern = "<.*>";
            string source = @"<?xml version=""1.0\"" encoding=\""UTF-8\""?>
        <Window ID=""Main"">
            <Control ID=""TextBox"">
                <Property Top=""100"" Left=""0"" Text=""BLANK""/>
            </Control>
            <Control ID=""Label"">
                <Property Top=""99990"" Left=""0"" Caption=""Enter Name Here""/>
            </Control>
        </Window>";

            ComplexReplace(matchPattern, source);
        }
        #endregion

        #region "A Better Tokenizer"
        public static string[] Tokenize(string equation)
        {
            Regex RE = new Regex(@"([\+\-\*\(\)\^\\])");
            return (RE.Split(equation));
        }

        public static void TestTokenize()
        {
            foreach (string token in Tokenize("(y - 3)(3111*x^21 + x + 320)"))
                Console.WriteLine("String token = " + token.Trim());

            Console.WriteLine();
        }
        #endregion

        #region "Counting the Number of Lines of Text"
        public static long LineCount(string source, bool isFileName)
        {
            if (source != null)
            {
                string text = source;

                if (isFileName)
                {
                    FileStream FS = new FileStream(source, FileMode.Open,
                        FileAccess.Read, FileShare.Read);
                    StreamReader SR = new StreamReader(FS);
                    text = SR.ReadToEnd();
                    SR.Close();
                    FS.Close();
                }

                Regex RE = new Regex("\n", RegexOptions.Multiline);
                MatchCollection theMatches = RE.Matches(text);

                if (isFileName)
                    Console.WriteLine("LineCount: " + (theMatches.Count).ToString());
                else
                    Console.WriteLine("LineCount: " + (theMatches.Count + 1).ToString());

                // Needed for files with zero length
                //   Note that a string will always have a line terminator and thus will
                //        always have a length of 1 or more
                if (isFileName)
                {
                    return (theMatches.Count);
                }
                else
                {
                    return (theMatches.Count) + 1;
                }
            }
            else
            {
                // Handle a null source here
                return (0);
            }
        }

        public static long LineCount2(string source, bool isFileName)
        {
            if (source != null)
            {
                string text = source;
                long numOfLines = 0;

                if (isFileName)
                {
                    FileStream FS = new FileStream(source, FileMode.Open,
                        FileAccess.Read, FileShare.Read);
                    StreamReader SR = new StreamReader(FS);

                    while (text != null)
                    {
                        text = SR.ReadLine();

                        if (text != null)
                        {
                            ++numOfLines;
                        }
                    }

                    SR.Close();
                    FS.Close();

                    Console.WriteLine("LineCount: " + numOfLines.ToString());

                    return (numOfLines);
                }
                else
                {
                    Regex RE = new Regex("\n", RegexOptions.Multiline);
                    MatchCollection theMatches = RE.Matches(text);
                    Console.WriteLine("LineCount: " + (theMatches.Count + 1).ToString());
                    return (theMatches.Count + 1);
                }
            }
            else
            {
                // Handle a null source here
                return (0);
            }
        }

        public static void TestLineCount()
        {
            // Count the lines within the file TestFile.txt
            //LineCount(@"..\..\TestFile.txt", true);
            LineCount(@"TestFile.txt", true);
            Console.WriteLine();

            // Count the lines within the string TestString
            LineCount("Line1\r\nLine2\r\nLine3\nLine4", false);
            Console.WriteLine();

            // Count the lines within the string TestString
            LineCount("", false);
            Console.WriteLine();

            // Count the lines within the file TestFile.txt
            //LineCount2(@"..\..\TestFile.txt", true);
            LineCount(@"TestFile.txt", true);
            Console.WriteLine();

            // Count the lines within the string TestString
            LineCount2("Line1\r\nLine2\r\nLine3\nLine4", false);
            Console.WriteLine();

            // Count the lines within the string TestString
            LineCount2("", false);
        }
        #endregion

        #region "Returning the Entire Line in Which a Match is Found"
        public static List<string> GetLines2(string source, string pattern, bool isFileName)
        {
            string text = source;
            List<string> matchedLines = new List<string>();

            // If this is a file, get the entire file's text
            if (isFileName)
            {
                FileStream FS = new FileStream(source, FileMode.Open,
                    FileAccess.Read, FileShare.Read);
                StreamReader SR = new StreamReader(FS);

                while (text != null)
                {
                    text = SR.ReadLine();

                    if (text != null)
                    {
                        // Run the regex on each line in the string
                        Regex RE = new Regex(pattern, RegexOptions.Multiline);
                        MatchCollection theMatches = RE.Matches(text);

                        if (theMatches.Count > 0)
                        {
                            // Get the line if a match was found
                            matchedLines.Add(text);
                        }
                    }
                }

                SR.Close();
                FS.Close();
            }
            else
            {
                // Run the regex once on the entire string
                Regex RE = new Regex(pattern, RegexOptions.Multiline);
                MatchCollection theMatches = RE.Matches(text);

                // Get the line for each match
                foreach (Match m in theMatches)
                {
                    int lineStartPos = GetBeginningOfLine(text, m.Index);
                    int lineEndPos = GetEndOfLine(text, (m.Index + m.Length - 1));
                    string line = text.Substring(lineStartPos, lineEndPos - lineStartPos);
                    matchedLines.Add(line);
                }
            }

            return (matchedLines);
        }

        public static List<string> GetLines(string source, string pattern, bool isFileName)
        {
            string text = source;
            List<string> matchedLines = new List<string>();

            // If this is a file, get the entire file's text
            if (isFileName)
            {
                FileStream FS = new FileStream(source, FileMode.Open,
                    FileAccess.Read, FileShare.Read);
                StreamReader SR = new StreamReader(FS);
                text = SR.ReadToEnd();
                SR.Close();
                FS.Close();
            }

            // Run the regex once on the entire string
            Regex RE = new Regex(pattern, RegexOptions.Multiline);
            MatchCollection theMatches = RE.Matches(text);

            // Get the line for each match
            foreach (Match m in theMatches)
            {
                int lineStartPos = GetBeginningOfLine(text, m.Index);
                int lineEndPos = GetEndOfLine(text, (m.Index + m.Length - 1));
                string line = text.Substring(lineStartPos, lineEndPos - lineStartPos);
                matchedLines.Add(line);
            }

            return (matchedLines);
        }

        public static int GetBeginningOfLine(string text, int startPointOfMatch)
        {
            if (startPointOfMatch > 0)
            {
                --startPointOfMatch;
            }

            if (startPointOfMatch >= 0 && startPointOfMatch < text.Length)
            {
                // Move to the left until the first '\n char is found
                for (int index = startPointOfMatch; index >= 0; index--)
                {
                    if (text[index] == '\n')
                    {
                        return (index + 1);
                    }
                }

                return (0);
            }

            return (startPointOfMatch);
        }

        public static int GetEndOfLine(string text, int endPointOfMatch)
        {
            if (endPointOfMatch >= 0 && endPointOfMatch < text.Length)
            {
                // Move to the right until the first '\n char is found
                for (int index = endPointOfMatch; index < text.Length; index++)
                {
                    if (text[index] == '\n')
                    {
                        return (index);
                    }
                }

                return (text.Length);
            }

            return (endPointOfMatch);
        }

        public static void TestGetLine()
        {
            // Get the matching lines within the file TestFile.txt
            Console.WriteLine("\n\n~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~\n\n");
            //List<string> lines = GetLines(@"..\..\TestFile.txt", "\n", true);
            List<string> lines = GetLines(@"TestFile.txt", "\n", true);
            foreach (string s in lines)
                Console.WriteLine("MatchedLine: " + s);

            // Get the matching lines within the string TestString
            Console.WriteLine("\n\n~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~\n\n");
            lines = GetLines("Line1\r\nLine2\r\nLine3\nLine4", "Line", false);
            foreach (string s in lines)
                Console.WriteLine("MatchedLine: " + s);

            // Get the matching lines within the string TestString
            Console.WriteLine("\n\n~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~\n\n");
            lines = GetLines("\rLLLLLL", "L", false);
            foreach (string s in lines)
                Console.WriteLine("MatchedLine: " + s);

            // Get the matching lines within the file TestFile.txt
            Console.WriteLine("\n\n~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~\n\n");
            //lines = GetLines2(@"..\..\TestFile.txt", "\n", true);
            lines = GetLines2(@"TestFile.txt", "\n", true);
            foreach (string s in lines)
                Console.WriteLine("MatchedLine: " + s);

            // Get the matching lines within the string TestString
            Console.WriteLine("\n\n~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~\n\n");
            lines = GetLines2("Line1\r\nLine2\r\nLine3\nLine4", "Line", false);
            foreach (string s in lines)
                Console.WriteLine("MatchedLine: " + s);

            // Get the matching lines within the string TestString
            Console.WriteLine("\n\n~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~\n\n");
            lines = GetLines2("\rLLLLLL", "L", false);
            foreach (string s in lines)
                Console.WriteLine("MatchedLine: " + s);

            Console.WriteLine();
        }
        #endregion

        #region "Finding a Particular Occurrence of a Match"
        public static Match FindOccurrenceOf(string source, string pattern, int occurrence)
        {
            if (occurrence < 1)
            {
                throw (new ArgumentException("Cannot be less than 1", "occurrence"));
            }

            // Make occurrence zero-based
            --occurrence;

            // Run the regex once on the source string
            Regex RE = new Regex(pattern, RegexOptions.Multiline);
            MatchCollection theMatches = RE.Matches(source);

            if (occurrence >= theMatches.Count)
            {
                return (null);
            }
            else
            {
                return (theMatches[occurrence]);
            }
        }

        public static List<Match> FindEachOccurrenceOf(string source, string pattern, int occurrence)
        {
            List<Match> occurrences = new List<Match>();

            // Run the regex once on the source string
            Regex RE = new Regex(pattern, RegexOptions.Multiline);
            MatchCollection theMatches = RE.Matches(source);

            for (int index = (occurrence - 1); index < theMatches.Count; index += occurrence)
            {
                occurrences.Add(theMatches[index]);
            }

            return (occurrences);
        }

        public static void TestOccurrencesOf()
        {
            Match matchResult = FindOccurrenceOf("one two three one two three one two three one"
                + " two three one two three one two three", "two", 2);
            if (matchResult != null)
                Console.WriteLine(matchResult.ToString() + "\t" + matchResult.Index);

            Console.WriteLine();
            List<Match> results = FindEachOccurrenceOf("one one two three one two three one two" +
                " three one two three", "one", 2);
            foreach (Match m in results)
                Console.WriteLine(m.ToString() + "\t" + m.Index);

            Console.WriteLine();
        }
        #endregion

        #region "Extracting Groups from a MatchCollection"
        public static void TestExtractGroupings()
        {
            //string source = @"Path = ""\\MyServer\MyService\MyPath;
            //                  \\MyServer2\MyService2\MyPath2\""";
            string matchPattern = @"\\\\					# \\
									(?<TheServer>\w*)		# Server name
									\\						# \
									(?<TheService>\w*)\\	# Service name";

            foreach (Dictionary<string, Group> grouping in ExtractGroupings(source, matchPattern, true))
            {
                foreach (KeyValuePair<string, Group> kvp in grouping)
                    Console.WriteLine("Key/Value = " + kvp.Key + " / " + kvp.Value);
                Console.WriteLine("");
            }
        }

        public static List<Dictionary<string, Group>> ExtractGroupings(string source, string matchPattern,
                                                       bool wantInitialMatch)
        {
            List<Dictionary<string, Group>> keyedMatches = new List<Dictionary<string, Group>>();
            int startingElement = 1;
            if (wantInitialMatch)
            {
                startingElement = 0;
            }

            Regex RE = new Regex(matchPattern, RegexOptions.Multiline | RegexOptions.IgnorePatternWhitespace);
            MatchCollection theMatches = RE.Matches(source);

            foreach (Match m in theMatches)
            {
                Dictionary<string, Group> groupings = new Dictionary<string, Group>();

                for (int counter = startingElement; counter < m.Groups.Count; counter++)
                {
                    groupings.Add(RE.GroupNameFromNumber(counter), m.Groups[counter]);
                }

                keyedMatches.Add(groupings);
            }

            return (keyedMatches);
        }
        #endregion

        static void Main(string[] args)
        {
            TestUserInputRegEx("Test");

            TestUserInputRegEx("");

            TestFindLast();

            TestComplexReplace();

            TestTokenize();

            TestLineCount();

            TestGetLine();

            TestOccurrencesOf();

            TestExtractGroupings();

            Console.Read();
        }
    }
}
