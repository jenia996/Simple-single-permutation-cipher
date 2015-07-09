using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;

namespace Шифр_простой_одинарной_перестановки
{
    class Program
    {
        private static string read ()
        {
            using (StreamReader reader = new StreamReader("input.txt"))
            {
                return reader.ReadLine();
            }
        }
        private static List<int> createKey (String input)
        {
            Random random = new Random();
            List<int> key = new List<int>();
            while (key.Count < input.Length)
            {
                int temp = random.Next() % input.Length;
                if (!key.Contains(temp))
                {
                    key.Add(temp);
                }
            }
            return key;
        }
        private static String encode (String input, List<int> key)
        {
            StringBuilder encodedText = new StringBuilder();
            foreach (int position in key)
            {
                encodedText.Append(input[position]);
            }
            return encodedText.ToString();
        }
        private static List<int> findSequence (String encodedText, String match)
        {
            int j = 0;
            List<Char> encodedTextSequence = encodedText.ToList();
            List<Char> matchTextSequence = match.ToList();
            List<int> sequence = new List<int>();
            while (sequence.Count < matchTextSequence.Count && j < encodedTextSequence.Count)
            {
                List<int> positions = matchTextSequence.Select((c, i) => new { character = c, index = i })
                         .Where(list => String.Compare(list.character.ToString(), encodedTextSequence[j].ToString(), true) == 0)
                         .Select(o => o.index).ToList();
                foreach (int pos in positions)
                {
                    if (!sequence.Contains(pos))
                    {
                        sequence.Add(pos);
                        break;
                    }
                }
                j++;

            }
            return sequence;

        }
        private static Dictionary<String, List<int>> answersWithKey (String encodedText, List<String> matches)
        {
            Dictionary<String, List<int>> answers = new Dictionary<string, List<int>>();
            foreach (String match in matches)
            {
                answers.Add(match, findSequence(encodedText, match));
            }
            return answers;
        }
        static void Main (string[] args)
        {
            String input = read();
            List<int> key = createKey(input);
            String encodedText = encode(input, key);
            DictionaryHandler dicHandler = new DictionaryHandler();
            List<String> answers = dicHandler.findAnswers(encodedText);
            using(StreamWriter writer = new StreamWriter("output.txt"))
            {
                writer.WriteLine(encodedText);
                foreach(KeyValuePair<String,List<int>> pair in   answersWithKey(encodedText,answers))
                {
                    writer.Write(pair.Key+ " ");
                    foreach(int x in pair.Value)
                    {
                        writer.Write(x+1);
                        if (key.Count > 10)
                            writer.Write(" ");
                    }
                    writer.WriteLine();
                }
                foreach (int x in key)
                {
                    writer.Write(x+1);
                    if (key.Count > 10)
                        writer.Write(" ");
                }
            }
          
        }
    }
}
