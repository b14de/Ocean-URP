using UnityEngine;

namespace Code.Utilities.View
{
    public class HexColor
    {
        #region Fields
        private const string 			COLOR_HTML 			= "<color={0}>{1}</color>";
        private const string 			COLOR_HEX 			= "#{0:X2}{1:X2}{2:X2}";
        private readonly byte 			_hexR;
        private readonly byte 			_hexG;
        private readonly byte 			_hexB;
        #endregion
		
        #region Constructor
        public HexColor(Color color)
        {
            _hexR	= HexByteChannel(color.r);
            _hexG 	= HexByteChannel(color.g);
            _hexB 	= HexByteChannel(color.b);
        }
        #endregion

        #region Methods
        public string ColorString(string stringToColor)
        {
            var hex 		= string.Format(COLOR_HEX, _hexR, _hexG, _hexB);
            return string.Format(COLOR_HTML, hex, stringToColor);
        }

        public static string HexRepresent(Color color)
        {
            return string.Format(COLOR_HEX, HexByteChannel(color.r), HexByteChannel(color.g), HexByteChannel(color.b));
        }
        #endregion
		
        #region Implementation
        private static byte HexByteChannel(float normalisedChannel)
        {
            return (byte)(normalisedChannel * 255f);
        }
        #endregion
    }
}