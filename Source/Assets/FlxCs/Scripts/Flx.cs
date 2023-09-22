using System;
using System.Collections.Generic;

namespace FlxCs
{
    public static class Flx
    {
        private static List<char> WORD_SEPARATORS = new List<char>()
        {
            ' ', '-', '_', ':', '.', '/', '\\',
        };

        private const int DEFAULT_SCORE = -35;

        /// <summary>
        /// Check if CHAR is a word character.
        /// </summary>
        public static bool Word(char? ch)
        {
            if (ch == null) return false;
            return !WORD_SEPARATORS.Contains(ch.Value);
        }

        /// <summary>
        /// Check if CHAR is an uppercase character.
        /// </summary>
        public static bool Capital(char? ch)
        {
            return Word(ch) && ch == char.ToUpper(ch.Value);
        }

        /// <summary>
        /// Check if LAST-CHAR is the end of a word and CHAR the start of the next.
        /// 
        /// This function is camel-case aware.
        /// </summary>
        public static bool Boundary(char? lastCh, char ch)
        {
            if (lastCh == null)
                return true;

            if (!Capital(lastCh) && Capital(ch))
                return true;

            if (!Word(lastCh) && Word(ch))
                return true;

            return false;
        }

        /// <summary>
        /// Increment each element in VEC between BEG and END by INC.
        /// </summary>
        public static void IncVec(List<int> vec, int? inc, int? beg, int? end)
        {
            int _inc = (inc == null) ? 1 : inc.Value;
            int _beg = (beg == null) ? 0 : beg.Value;
            int _end = (end == null) ? vec.Count : end.Value;

            while (_beg < _end)
            {
                vec[_beg] += _inc;
                ++_beg;
            }
        }

        /// <summary>
        /// Return hash-table for string where keys are characters.
        /// Value is a sorted list of indexes for character occurrences.
        /// </summary>
        public static void GetHashForString(Dictionary<int, List<int>> result, string str)
        {
            result.Clear();

            int strLen = str.Length;
            int index = strLen - 1;
            char ch;
            char downCh;

            while (0 <= index)
            {
                ch = str[index];

                if (Capital(ch))
                {
                    Util.DictInsert(result, ch, index);

                    downCh = char.ToLower(ch);
                }
                else
                {
                    downCh = ch;
                }

                Util.DictInsert(result, downCh, index);

                --index;
            }
        }

        /// <summary>
        /// Generate the heatmap vector of string.
        ///
        /// See documentation for logic.
        /// </summary>
        public static void GetHeatmapStr(List<int> scores, string str, char? groupSeparator)
        {
            int strLen = str.Length;
            int strLastIndex = strLen - 1;
            scores.Clear();

            for (int i = 0; i < strLen; ++i)
                scores.Add(DEFAULT_SCORE);

            int penaltyLead = (int)'.';

            var inner = new List<int>() { -1, 0 };
            var groupAlist = new List<List<int>>() { inner };

            // final char bonus
            scores[strLastIndex] += 1;

            // Establish baseline mapping
            char? lastCh = null;
            int groupWordCount = 0;
            int index1 = 0;

            foreach (char ch in str)
            {
                // before we find any words, all separaters are
                // considered words of length 1.  This is so "foo/__ab"
                // gets penalized compared to "foo/ab".
                char? effectiveLastChar = ((groupWordCount == 0) ? null : lastCh);

                if (Boundary(effectiveLastChar, ch))
                {
                    groupAlist[0].Insert(2, index1);
                }

                if (!Word(lastCh) && Word(ch))
                {
                    ++groupWordCount;
                }

                // ++++ -45 penalize extension
                if (lastCh != null && lastCh == penaltyLead)
                {
                    scores[index1] += -45;
                }

                if (groupSeparator != null && groupSeparator == ch)
                {
                    groupAlist[0][1] = groupWordCount;
                    groupWordCount = 0;
                    groupAlist.Insert(0, new List<int>() { index1, groupWordCount });
                }

                if (index1 == strLastIndex)
                {
                    groupAlist[0][1] = groupWordCount;
                }
                else
                {
                    lastCh = ch;
                }

                ++index1;
            }

            int groupCount = groupAlist.Count;
            int separatorCount = groupCount - 1;

            // ++++ slash group-count penalty
            if (separatorCount != 0)
            {
                IncVec(scores, groupCount * -2, null, null);
            }

            int index2 = separatorCount;
            int? lastGroupLimit = null;
            bool basepathFound = false;

            // score each group further
            foreach (List<int> group in groupAlist)
            {
                int groupStart = group[0];
                int wordCount = group[1];
                // this is the number of effective word groups
                int wordsLength = group.Count - 2;
                bool basepathP = false;

                if (wordsLength != 0 && !basepathFound)
                {
                    basepathFound = true;
                    basepathP = true;
                }

                int num;
                if (basepathP)
                {
                    // ++++ basepath separator-count boosts
                    int boosts = 0;
                    if (separatorCount > 1)
                        boosts = separatorCount - 1;
                    // ++++ basepath word count penalty
                    int penalty = -wordCount;
                    num = 35 + boosts + penalty;
                }
                // ++++ non-basepath penalties
                else
                {
                    if (index2 == 0)
                        num = -3;
                    else
                        num = -5 + (index2 - 1);
                }

                IncVec(scores, num, groupStart + 1, lastGroupLimit);

                var cddrGroup = new List<int>(group);  // clone it
                cddrGroup.RemoveAt(0);
                cddrGroup.RemoveAt(0);

                int wordIndex = wordsLength - 1;
                int? lastWord = (lastGroupLimit != null) ? lastGroupLimit : strLen;

                foreach (int word in cddrGroup)
                {
                    // ++++  beg word bonus AND
                    scores[word] += 85;

                    int index3 = word;
                    int charI = 0;

                    while (index3 < lastWord)
                    {
                        scores[index3] +=
                            (-3 * wordIndex) -  // ++++ word order penalty
                            charI;  // ++++ char order penalty
                        ++charI;

                        ++index3;
                    }

                    lastWord = word;
                    --wordIndex;
                }

                lastGroupLimit = groupStart + 1;
                --index2;
            }
        }

        /// <summary>
        /// Return sublist bigger than VAL from sorted SORTED-LIST.
        /// 
        /// If VAL is nil, return entire list.
        /// </summary>
        public static void BiggerSublist(List<int> result, List<int> sortedList, int? val)
        {
            if (sortedList == null)
                return;

            if (val != null)
            {
                foreach (var sub in sortedList)
                {
                    if (sub > val)
                    {
                        result.Add(sub);
                    }
                }
            }
            else
            {
                foreach (var sub in sortedList)
                {
                    result.Add(sub);
                }
            }
        }

        /// <summary>
        /// Recursively compute the best match for a string, passed as STR-INFO and
        /// HEATMAP, according to QUERY.
        /// </summary>
        public static void FindBestMatch(List<Score> imatch,
            Dictionary<int, List<int>> strInfo,
            List<int> heatmap,
            int? greaterThan,
            string query, int queryLength,
            int qIndex,
            Dictionary<int, List<Score>> matchCache)
        {
            int? greaterNum = (greaterThan != null) ? greaterThan : 0;
            int? hashKey = qIndex + (greaterNum * queryLength);
            List<Score> hashValue = Util.DictGet(matchCache, hashKey);

            if (hashValue != null)  // Process matchCache here
            {
                imatch.Clear();
                foreach (var val in hashValue)
                    imatch.Add(val);
            }
            else
            {
                int uchar = query[qIndex];
                List<int> sortedList = Util.DictGet(strInfo, uchar);
                var indexes = new List<int>();
                BiggerSublist(indexes, sortedList, greaterThan);
                int tempScore;
                int bestScore = int.MinValue;

                //Test.Print(sortedList);

                if (qIndex >= queryLength - 1)
                {
                    // At the tail end of the recursion, simply generate all possible
                    // matches with their scores and return the list to parent.
                    foreach (int index in indexes)
                    {
                        List<int> indices = new List<int>();
                        indices.Add(index);
                        imatch.Add(new Score(indices, heatmap[index], 0));
                    }
                }
                else
                {
                    foreach (int index in indexes)
                    {
                        List<Score> elemGroup = new List<Score>();
                        FindBestMatch(elemGroup,
                            new Dictionary<int, List<int>>(strInfo),
                            new List<int>(heatmap),
                            index, query, queryLength, qIndex + 1, matchCache);

                        foreach (var elem in elemGroup)
                        {
                            int caar = elem.indices[0];
                            int cadr = elem.score;
                            int cddr = elem.tail;

                            if ((caar - 1) == index)
                            {
                                tempScore = cadr + heatmap[index] +
                                    (Math.Min(cddr, 0) * 15) +  // boost contiguous matches
                                    60;
                            }
                            else
                            {
                                tempScore = cadr + heatmap[index];
                            }

                            // We only care about the optimal match, so only forward the match
                            // with the best score to parent
                            if (tempScore > bestScore)
                            {
                                bestScore = tempScore;

                                imatch.Clear();
                                List<int> indices = new List<int>(elem.indices);
                                indices.Insert(0, index);
                                int tail = 0;
                                if ((caar - 1) == index)
                                    tail = cddr + 1;
                                imatch.Add(new Score(indices, tempScore, tail));
                            }
                        }
                    }
                }

                // Calls are cached to avoid exponential time complexity
                Util.DictSet(matchCache, hashKey, new List<Score>(imatch));
            }
        }

        /// <summary>
        /// Return best score matching QUERY against STR.
        /// </summary>
        public static Score Score(string str, string query)
        {
            if (string.IsNullOrEmpty(str) || string.IsNullOrEmpty(query))
                return null;

            var strInfo = new Dictionary<int, List<int>>();
            GetHashForString(strInfo, str);

            var heatmap = new List<int>();
            GetHeatmapStr(heatmap, str, null);

            int queryLength = query.Length;
            bool fullMatchBoost = (1 < queryLength) && (queryLength < 5);
            var matchCache = new Dictionary<int, List<Score>>();
            var optimalMatch = new List<Score>();
            FindBestMatch(optimalMatch, strInfo, heatmap, null, query, queryLength, 0, matchCache);

            if (optimalMatch.Count == 0)
                return null;

            Score result1 = optimalMatch[0];
            int caar = result1.indices.Count;

            if (fullMatchBoost && caar == str.Length)
            {
                result1.score += 10000;
            }

            return result1;
        }
    }
}
