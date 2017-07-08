using System.Xml.Serialization;

namespace GarantiVP
{
    //TODO Description
    /// <summary>
    /// 
    /// </summary>
    [XmlType(AnonymousType = true)]
    public class GVPSTransaction
    {
        /// <summary>
        /// Transaction type
        /// <para>Size 32 Byte alfanumeric</para>
        /// <para lang="tr">İşlem tipi</para> 
        /// </summary>
        [XmlElement(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        [FormElement("txntype")]
        public GVPSTransactionTypeEnum Type { get; set; }

        /// <summary>
        /// Number of installments. If it is sent empty, no installment will be made.
        /// <para lang="tr">Taksit Sayısı. Eğer boş gönderilirse, taksit yapılamaz.</para> 
        /// </summary>
        [XmlElement(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        [FormElement("txninstallmentcount")]
        public string InstallmentCnt { get; set; }

        /// <summary>
        /// Amount
        /// <para>Size 19 Byte numeric</para> 
        /// <para>WARNING : Do not send more than 2 digits after the decimal slash. Example 1234.567 -> 123456</para>
        /// <para lang="tr">Tutar</para> 
        /// </summary>
        [XmlElement(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        [FormElement("txnamount")]
        public ulong Amount { get; set; }

        /// <summary>
        /// Currency code
        /// <para>Size 3 Byte</para>
        /// <para lang="tr">Para birimi kodu</para> 
        /// </summary>
        [XmlElement(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        [FormElement("txncurrencycode")]
        public GVPSCurrencyCodeEnum CurrencyCode { get; set; }

        /// <summary>
        /// Cancel - the first generated number for the call
        /// <para>Size 12 Byte numeric</para>
        /// <para lang="tr">İptal - arama için ilk üretilen numara</para> 
        /// </summary>
        [XmlElement]
        public string OriginalRetrefNum { get; set; }

        /// <summary>
        /// Number of days shifted
        /// <para>Size 2 Byte numeric</para>
        /// <para lang="tr">Ötelenmiş gün sayısı</para> 
        /// </summary>
        [XmlElement]
        [FormElement("txndelaydaycnt")]
        public string DelayDayCount { get; set; }

        /// <summary>
        /// Down payment rate
        /// <para>Size 2 Byte numeric</para>
        /// <para lang="tr">Peşinat payı / oranı</para> 
        /// </summary>
        [XmlElement]
        [FormElement("txndownpayrate")]
        public string DownPaymentRate { get; set; }


        /// <summary>
        /// For normal operations value is 0. For 3D secure operations value is 13.
        /// <para>Size 2 Byte numeric</para> 
        /// <para lang="tr">Normal işlemler için değer 0'dır. 3D güvenli işlemler için değer 13'tür.</para> 
        /// </summary>
        [XmlElement(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        [FormElement("txncardholderpresentcode")]
        public GVPSCardholderPresentCodeEnum CardholderPresentCode { get; set; }

        /// <summary>
        /// For ECommerce operations value is N. For Moto operations value is Y.
        /// <para>Size 1 Byte alfanumeric</para>
        /// <para lang="tr">E-ticaret operasyonları için değer N'dir. Moto işlemleri için değer Y'dir.</para> 
        /// </summary>
        [XmlElement(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        [FormElement("txnmotoind")]
        public GVPSMotoIndEnum MotoInd { get; set; }

        /// <summary>
        /// Trading card transaction information
        /// As much as the installment number, it should be installment information.
        /// <para lang="tr">Ortak kart işlemleri bilgisi</para> 
        /// <para lang="tr">Taksit sayısı kadar vade bilgisi olmalıdır.</para> 
        /// </summary>
        [XmlElement(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public GVPSCommercialCardExtendedCredit CommercialCardExtendedCredit { get; set; }

        /// <summary>
        /// Corporate payment inquiry
        /// <para lang="tr">Kurum ödeme sorgulaması</para> 
        /// </summary>
        [XmlElement(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public GVPSUtilityPaymentInq UtilityPaymentInq { get; set; }

        /// <summary>
        /// Corporate payment cancellation inquiry
        /// <para lang="tr">Kurumsal ödeme iptali sorgusu</para> 
        /// </summary>
        [XmlElement(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public GVPSUtilityPaymentVoidInq UtilityPaymentVoidInq { get; set; }

        /// <summary>
        /// GSM money uploading, query processing parameters
        /// <para lang="tr">GSM para yükleme, sorgulama işlemi parametreleri</para> 
        /// </summary>
        [XmlElement(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public GVPSGSMUnitInq GSMUnitInq { get; set; }

        /// <summary>
        /// Information required for invoice payment transactions
        /// <para lang="tr">Fatura ödeme işlemleri için gerekli bilgiler</para> 
        /// </summary>
        [XmlElement(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public GVPSUtilityPayment UtilityPayment { get; set; }

        /// <summary>
        /// GSM money uploading, query processing parameters
        /// <para lang="tr">GSM para yükleme, sorgulama işlemi parametreleri</para> 
        /// </summary>
        [XmlElement(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public GVPSGSMUnitSales GSMUnitSales { get; set; }

        /// <summary>
        /// For CepBank operation information
        /// <para lang="tr">CepBank işlem bilgileri için</para> 
        /// </summary>
        [XmlElement(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public GVPSCepBank CepBank { get; set; }

        /// <summary>
        /// For CepBank reward inquiry
        /// <para lang="tr">CepBank bonus / ödül sorgulama için</para> 
        /// </summary>
        [XmlElement(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public GVPSCepBankIng CepBankInq { get; set; }

        /// <summary>
        /// Information required for 3D secure operations.
        /// <para>The PayerAuthcode field should be 13 for 3D operations.</para>
        /// <para lang="tr">3D secure işlemleri için gerekli bilgiler.</para> 
        /// <para lang="tr">PayerAuthcode alanı 3D işlemler için 13 değerinde olmalıdır.</para> 
        /// </summary>
        [XmlElement(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public GVPSSecure3D Secure3D { get; set; }

        /// <summary>
        /// Reward information
        /// <para lang="tr">Bonus / Ödül bilgisi</para> 
        /// </summary>
        [XmlElement(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public GVPSRewardList RewardList { get; set; }

        //TODO Description
        /// <summary>
        /// 
        /// </summary>
        [XmlElement(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public GVPSChequeList ChequeList { get; set; }

        //TODO Description
        /// <summary>
        /// 
        /// </summary>
        [XmlElement(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public GVPSMoneyCard MoneyCard { get; set; }

        //TODO Description
        /// <summary>
        /// 
        /// </summary>
        [XmlElement]
        public GVPSVerification Verification { get; set; }


        #region For response fields

        /// <summary>
        /// Number generated for cancellation, refund and variable payment transactions
        /// <para lang="tr">İptal, geri ödeme ve değişken ödeme işlemleri için üretilen numara</para> 
        /// </summary>
        [XmlElement]
        public string RetrefNum { get; set; }

        //TODO Description
        /// <summary>
        /// The field where the confirmation code is returned.
        /// </summary>
        [XmlElement]
        public string AuthCode { get; set; }

        //TODO Description
        /// <summary>
        /// The field where the transaction sequence number is returned.
        /// </summary>
        [XmlElement]
        public string SequenceNum { get; set; }

        //TODO Description
        /// <summary>
        /// The area where the provision date is rotated.
        /// </summary>
        [XmlElement]
        public string ProvDate { get; set; }

        //TODO Description
        /// <summary>
        /// 
        /// </summary>
        [XmlElement]
        public GVPSTransactionResponse Response { get; set; }

        //TODO Description
        /// <summary>
        /// 
        /// </summary>
        [XmlElement("HostMsgList")]
        public GVPSHostMsgList HostMsgList { get; set; }

        //TODO Description
        /// <summary>
        /// 
        /// </summary>
        [XmlElement("RewardInqResult")]
        public GVPSRewardInqResult RewardInqResult { get; set; }

        //TODO Description
        /// <summary>
        /// 
        /// </summary>
        [XmlElement("BatchNum")]
        public string BatchNum { get; set; }

        #endregion
    }
}