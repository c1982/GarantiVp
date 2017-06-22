using System.Xml.Serialization;

namespace GarantiVP
{
    /// <summary>
    /// Amount information for payment
    /// <para lang="tr">Ödeme için miktar bilgisi</para> 
    /// </summary>
    [XmlType(AnonymousType = true)]
    public abstract class GVPSPaymentBase
    {
        #region For public use
        
        /// <summary>
        /// Amount
        /// <para>WARNING : Do not send more than 2 digits after the decimal slash. Example 1234.567 -> 123456</para>
        /// <para lang="tr">Tutar</para> 
        /// </summary>
        [XmlElement]
        public string Amount { get; set; }

        #endregion
    }

}