using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Agfa.Common
{
    /// <summary>
    /// Alignment for the text
    /// </summary>
    [Serializable]
    [SoapType]
    [XmlRoot]
    public enum Alignments
    {
        /// <summary>
        /// Align Left
        /// </summary>
        [XmlElement]
        [SoapElement]
        Left = 0,
        /// <summary>
        /// Align Right
        /// </summary>
        [XmlElement]
        [SoapElement]
        Right = 1,
        /// <summary>
        /// Centered
        /// </summary>
        [XmlElement]
        [SoapElement]
        Center = 2
    }
}
