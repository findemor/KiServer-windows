using System;
using System.Drawing;
using ZBar;

namespace TagDetector.Models
{
    public class Tag
    {
        internal Tag(Symbol symbol) {
            Data = symbol.Data;
            Quality = symbol.Quality;
            SymbolType = (SymbolType) symbol.Type;
            Polygon = symbol.Polygon;
        }

        public byte[] Data { get; }
        public int Quality { get; }
        public SymbolType SymbolType { get; }
        public Point[] Polygon { get; }
    }

    [Flags]
    public enum SymbolType
    {
        /// <summary>
        /// No symbol decoded
        /// </summary>
        None = 0,

        /// <summary>
        /// Intermediate status
        /// </summary>
        Partial = 1,

        /// <summary>
        /// EAN-8
        /// </summary>
        EAN8 = 8,

        /// <summary>
        /// UPC-E
        /// </summary>
        UPCE = 9,

        /// <summary>
        /// ISBN-10 (from EAN-13)
        /// </summary>
        ISBN10 = 10,

        /// <summary>
        /// UPC-A
        /// </summary>
        UPCA = 12,

        /// <summary>
        /// EAN-13
        /// </summary>
        EAN13 = 13,

        /// <summary>
        /// ISBN-13 (from EAN-13)
        /// </summary>
        ISBN13 = 14,

        /// <summary>
        /// Interleaved 2 of 5.
        /// </summary>
        I25 = 25,

        /// <summary>
        /// Code 39.
        /// </summary>
        CODE39 = 39,

        /// <summary>
        /// PDF417
        /// </summary>
        PDF417 = 57,

        /// <summary>
        /// QR Code
        /// </summary>
        QRCODE = 64,

        /// <summary>
        /// Code 128
        /// </summary>
        CODE128 = 128,

        /// <summary>
        /// mask for base symbol type
        /// </summary>
        Symbole = 0x00ff,

        /// <summary>
        /// 2-digit add-on flag
        /// </summary>
        Addon2 = 0x0200,

        /// <summary>
        /// 5-digit add-on flag
        /// </summary>
        Addon5 = 0x0500,

        /// <summary>
        /// add-on flag mask
        /// </summary>
        Addon = 0x0700
    }
}
