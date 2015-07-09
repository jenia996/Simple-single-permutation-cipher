using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;

namespace Шифр_простой_одинарной_перестановки
{
    class DictionaryHandler
    {
        private String path = @"r:\University\Вычислительная Практика\Dictionary\Dictionary.txt";
        private List<String> dictionary;
        public DictionaryHandler ()
        {
            dictionary = new List<string>();
            using (StreamReader reader = new StreamReader(path))
            {

                while (reader.EndOfStream != true)
                {
                    dictionary.Add(reader.ReadLine());
                }
            }
        }
        private List<char> getSymbols(String input)
        {
            List<Char> symbols = new List<char>();
            foreach(Char x in input)
            {
                symbols.Add(x);
            }
            return symbols;
        }
        private Boolean checkWord(String word,List<Char> symbols)
        {
            List<Char> matchWordSymbols = word.ToList();
            if(Enumerable.SequenceEqual(matchWordSymbols.OrderBy(x =>x),symbols.OrderBy(t =>t)))
            {
                return true;
            }
            return false;
        }
        public List<String> findAnswers(String input)
        {
          // List<String>.Enumerator iterator = matches.GetEnumerator();
          // iterator.MoveNext();
            List<Char> symbols = getSymbols(input.ToLower());
            List<String> matches = dictionary.FindAll(x => x.Length == input.Length).ToList();
            List<String> answers = new List<string>();
            foreach(String word in matches)
            {
                if(checkWord(word,symbols))
                {
                    answers.Add(word);
                }
            }
            return answers;

        }

    }
}
