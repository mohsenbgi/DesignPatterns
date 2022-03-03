using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestPlatform.ObjectModel.Client.Payloads;

namespace FlyWeight
{

    class BadTextFormatting
    {
        private readonly string _text;
        private readonly bool[] _capitalization;

        public BadTextFormatting(string text)
        {
            _text = text;
            _capitalization = new bool[text.Length];
        }

        public void Capitalize(int start, int end)
        {
            for (int i = start; i < end; i++)
            {
                _capitalization[i] = true;
            }
        }

        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();
            for (int i = 0; i < _text.Length; i++)
            {
                char c = _text[i];
                stringBuilder.Append(_capitalization[i] ? char.ToUpper(c) : c);
            }

            return stringBuilder.ToString();
        }
    }

    enum TextFormatTypes
    {
        Capitalize,
        Bold,
        Italic
    }

    class GoodTextFormatting
    {
        private readonly string _text;
        private readonly List<RangeFormatted> _rangeFormats;


        public GoodTextFormatting(string text)
        {
            _text = text;
        }


        public void AddFormat(int start, int end, TextFormatTypes type)
        {
            _rangeFormats.Add(new RangeFormatted(start, end, type));
        }

        class RangeFormatted
        {
            private int _start, _end;
            private readonly TextFormatTypes _type;

            public RangeFormatted(int start, int end, TextFormatTypes type)
            {
                _start = start;
                _end = end;
                _type = type;
            }

            public char Formatted(int index, char c)
            {
                if (_start < index && index <= _end)
                {
                    if (_type == TextFormatTypes.Capitalize)
                        return Capitalize(c);
                }

                return c;
            }

            private char Capitalize(char c)
            {
                return char.ToUpper(c);
            }
        }

        public override string ToString()
        {
            var stringBuilder = new StringBuilder();

            for (int i = 0; i < _text.Length; i++)
            {
                foreach (var rangeFormatted in _rangeFormats)
                {
                    stringBuilder.Append(rangeFormatted.Formatted(i, _text[i]));
                }
            }

            return stringBuilder.ToString();
        }
    }


    class TextFormatting
    {
        void app()
        {
            string str = "This is a brave new world";
            
            var btf = new BadTextFormatting(str);
            btf.Capitalize(10, 15);

            var gtf = new GoodTextFormatting(str);
            gtf.AddFormat(10, 15, TextFormatTypes.Capitalize);

            Console.WriteLine(btf);
            Console.WriteLine(gtf);
        }
    }
}
