﻿using System.Xml.Serialization;

namespace GarantiVP
{
    /// <summary>
    /// Order comment
    /// </summary>
    [XmlType(AnonymousType = true)]
    public class GVPSRequestOrderCommentListComment
    {

        /// <summary>
        /// Comment no
        /// <para>But the virtual pos must be sent in the defined and defined order in the screens.</para>
        /// </summary>
        [XmlElement(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public uint Number { get; set; }


        /// <summary>
        /// Comment
        /// </summary>
        [XmlElement(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string Text { get; set; }
    }
}