using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UceniSlovicek
{
    public enum KindOfVocabulary { Czech, English };
    public class Vocabulary
    {
        public KindOfVocabulary Kind_Voc;
        public string Noun { get; private set; }
        public string Adjective { get; private set; }
        public string Verb { get; private set; }

        public Vocabulary(KindOfVocabulary K_V, string Noun, string Adj, string Verb)
        {
            this.Kind_Voc = K_V;
            this.Noun = Noun;
            this.Adjective = Adj;
            this.Verb = Verb;
        }

        public void SetNoun(string item)
        {
            this.Noun = item;
        }

        public void SetAdjective(string item)
        {
            this.Adjective = item;
        }

        public void SetVerb(string item)
        {
            this.Verb = item;
        }

    }
}
