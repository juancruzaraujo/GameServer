using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleOutputFormater
{
    public class OutputFormatterAttributes
    {
        internal const string C_ESCAPE = "\x1b[";
        internal const string C_P = ";";
        internal const string C_M = "m";
        internal const string C_RESET = "\x1b[0m";

        internal const string C_NORMAL = "0";  //NORMAL
        internal const string C_BOLD = "1";  //BOLD
        internal const string C_DECREASED = "2";  //DECREASED
        internal const string C_ITALIC = "3";  //ITALIC
        internal const string C_UNDERLINE = "4";  //UNDERLINE
        internal const string C_BLINK = "5";  //BLINK
        internal const string C_REVERSE = "7";  //REVERSE
        internal const string C_CROSED_OUT = "9";  //CROSED OUT
        internal const string C_DOUBLY = "21"; //DOUBLY UNDERLINE

        private bool _normal;
        private bool _bold;
        private bool _decrased;
        private bool _italic;
        private bool _underline;
        private bool _blink;
        private bool _reverse;
        private bool _hide;
        private bool _crossedOut;
        private bool _doublyUnderline;
        private int _textColorFG;
        private int _textColorBG;

        public enum TextColorFG
        {
            Black = 30,
            Red = 31,
            Green = 32,
            Yellow = 33,
            Blue = 34,
            Magenta = 35,
            Cyan = 36,
            White = 37,
            Bright_Black = 90,
            Bright_Red = 91,
            Bright_Green = 92,
            Bright_Yellow = 93,
            Bright_Blue = 94,
            Bright_Magenta = 95,
            Bright_Cyan = 96,
            Bright_White = 97
        }

        public enum TextColorBG
        {
            Black = 40,
            Red = 41,
            Green = 42,
            Yellow = 43,
            Blue = 44,
            Magenta = 45,
            Cyan = 46,
            White = 47,
            Bright_Black = 100,
            Bright_Red = 101,
            Bright_Green = 102,
            Bright_Yellow = 103,
            Bright_Blue = 104,
            Bright_Magenta = 105,
            Bright_Cyan = 106,
            Bright_White = 107
        }


        public OutputFormatterAttributes()
        {
            ResetAttributes();
        }

        public OutputFormatterAttributes SetNormal(bool val)
        {
            _normal = val;
            return this;
        }

        public OutputFormatterAttributes SetBold(bool val)
        {
            _bold = val;
            return this;
        }

        public OutputFormatterAttributes SetDecreased(bool val)
        {
            _decrased = val;
            return this;
        }

        public OutputFormatterAttributes SetItalic(bool val)
        {
            _italic = val;
            return this;
        }

        public OutputFormatterAttributes SetUnderline(bool val)
        {
            _underline = val;
            return this;
        }

        public OutputFormatterAttributes SetBlink(bool val)
        {
            _blink = val;
            return this;
        }

        public OutputFormatterAttributes SetReverse(bool val)
        {
            _reverse = val;
            return this;
        }

        public OutputFormatterAttributes SetHide(bool val)
        {
            _hide = val;
            return this;
        }

        public OutputFormatterAttributes SetCrossedOut(bool val)
        {
            _crossedOut = val;
            return this;
        }

        public OutputFormatterAttributes SetDoublyUnderline(bool val)
        {
            _doublyUnderline = val;
            return this;
        }

        public OutputFormatterAttributes SetColorBG(TextColorBG textColorBG)
        {
            _textColorBG = (int)textColorBG;
            return this;
        }

        public OutputFormatterAttributes SetColorFG(TextColorFG textColorFG)
        {
            _textColorFG = (int)textColorFG;
            return this;
        }

        public void ResetAttributes()
        {
            _normal = true;
            _textColorBG = (int)TextColorBG.Black;
            _textColorFG = (int)TextColorFG.White;


            _bold = false;
            _decrased = false;
            _italic = false;
            _underline = false;
            _blink = false;
            _reverse = false;
            _hide = false;
            _crossedOut = false;
            _doublyUnderline = false;

        }

        internal string GetAttributes
        {
            get
            {
                string attributes = "";
                // "\x1b[0;3;9;21;37;42mNormal,Fondo azul, letra verde\x1b[0m\n\r");

                if (_normal)
                {
                    attributes = C_NORMAL + C_P;
                }
                else if (_bold)
                {
                    attributes = C_BOLD + C_P;
                }

                if (_decrased)
                {
                    attributes = attributes + C_DECREASED + C_P;
                }

                if (_italic)
                {
                    attributes = attributes + C_ITALIC + C_P;
                }

                if (_underline)
                {
                    attributes = attributes + C_UNDERLINE + C_P;
                }

                if (_blink)
                {
                    attributes = attributes + C_BLINK + C_P;
                }

                if (_reverse)
                {
                    attributes = attributes + C_REVERSE + C_P;
                }

                if (_crossedOut)
                {
                    attributes = attributes + C_CROSED_OUT + C_P;
                }

                if (_doublyUnderline)
                {
                    attributes = attributes + C_UNDERLINE + C_P;
                }


                return attributes;
            }
        }

        internal int GetTextFG
        {
            get
            {
                return _textColorFG;
            }
        }

        internal int GetTextBG
        {
            get
            {
                return _textColorBG;
            }
        }
    }
}
