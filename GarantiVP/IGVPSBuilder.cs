using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace GarantiVP
{
    public interface IGVPSBuilder
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="posUrl"></param>
        /// <param name="mode">TEST, PROD</param>
        /// <returns></returns>
        IGVPSBuilder Server(string posUrl);

        /// <summary>
        /// İşlemler Test sunucusuna yönlendirilir.
        /// </summary>
        /// <returns></returns>
        IGVPSBuilder Test(bool IsTest = true);

        /// <summary>
        /// Firma bilgileri
        /// </summary>
        /// <param name="terminalId"></param>
        /// <param name="MerchantID"></param>
        /// <param name="userID"></param>
        /// <param name="userPassword"></param>
        /// <returns></returns>
        IGVPSBuilder Company(string terminalId, string MerchantID, string userID, string userPassword, string SubMerchantID = null);

        /// <summary>
        /// Müşteri Bilgileri
        /// </summary>
        /// <param name="customerMail">Müşteri eposta adresi</param>
        /// <param name="customerIP">Müşterinin IP adresi</param>
        /// <returns></returns>
        IGVPSBuilder Customer(string customerMail, string customerIP);

        IGVPSBuilder CreditCard(string number, string cvv2, int month, int year);

        /// <summary>
        /// Sipariş Numarası. Boş bırakılırsa sistem otomatik bir numara belirler
        /// </summary>
        /// <param name="orderID"></param>
        /// <param name="groupID"></param>
        /// <returns></returns>
        IGVPSBuilder Order(string orderID, string groupID = "");

        /// <summary>
        /// Sipariş ile ilgili adres bilgilerinin eklenmesi sağlar.
        /// </summary>
        /// <param name="type">Adres türü</param>
        /// <param name="city"></param>
        /// <param name="district"></param>
        /// <param name="addressText"></param>
        /// <param name="phone"></param>
        /// <param name="name"></param>
        /// <param name="lastName"></param>
        /// <param name="Company"></param>
        /// <param name="postalCode"></param>
        /// <returns></returns>
        IGVPSBuilder AddOrderAddress(GVPSAddressTypeEnum type, string city, string district, string addressText, string phone, string name, string lastName, string Company = null, string postalCode = null);

        /// <summary>
        /// Sipariş ile ilgili adres bilgilerinin eklenmesi sağlar.
        /// </summary>
        /// <param name="address"></param>
        /// <returns></returns>
        IGVPSBuilder AddOrderAddress(GVPSAddress address);

        /// <summary>
        /// Siparişe ait ürün / hizmet detayı eklenmesini sağlar.
        /// </summary>
        /// <param name="number"></param>
        /// <param name="productCode"></param>
        /// <param name="productId"></param>
        /// <param name="price"></param>
        /// <param name="quantity"></param>
        /// <param name="totalAmount"></param>
        /// <param name="description"></param>
        /// <returns></returns>
        IGVPSBuilder AddOrderItem(uint number, string productCode, string productId, double price, double quantity, string description = null, double totalAmount = 0.0);

        /// <summary>
        /// Siparişe ait ürün / hizmet detayı eklenmesini sağlar.
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        IGVPSBuilder AddOrderItem(GVPSItem item);

        /// <summary>
        /// Raporlama ekranlarında kullanılmak üzere özel açıklama eklemek için kullanılır.
        /// </summary>
        /// <param name="number">Sanal pos ekranında tanımlanan parametre numarası</param>
        /// <param name="text"></param>
        /// <returns></returns>
        IGVPSBuilder AddOrderComment(uint number, string text);

        /// <summary>
        /// Raporlama ekranlarında kullanılmak üzere özel açıklama eklemek için kullanılır.
        /// </summary>
        /// <param name="comment"></param>
        /// <returns></returns>
        IGVPSBuilder AddOrderComment(GVPSComment comment);

        /// <summary>
        /// Kredi Kartından çekilecek tutar
        /// </summary>
        /// <param name="totalAmount">Tutar</param>
        /// <param name="currencyCode">Para Birimi. Varsayılan: TRL</param>
        /// <returns></returns>
        IGVPSBuilder Amount(double totalAmount, GVPSCurrencyCodeEnum currencyCode = GVPSCurrencyCodeEnum.TRL);

        /// <summary>
        /// Taksitli işlemlerde işlem tutarının üzerinden belli bir oranda peşinat alınmasının sağlanması için kullanılan işlemdir.
        /// </summary>
        /// <param name="rate">Peşinat oranı yüzde olarak belirlenir. %10 ise 10 girilir</param>
        /// <returns></returns>
        IGVPSBuilder DownPaymentRate(int rate);

        /// <summary>
        /// Taksitli İşlem
        /// </summary>
        /// <param name="installment">Taksit Sayısı</param>
        /// <returns></returns>
        IGVPSBuilder Installment(int installment);

        /// <summary>
        /// Ötelemeli Satış
        /// </summary>
        /// <param name="day">Ötelenecek gün sayısı.</param>
        /// <returns></returns>
        IGVPSBuilder Delay(int day);

        /// <summary>
        /// Satış
        /// </summary>
        /// <returns></returns>
        GVPSResponse Sales();

        /// <summary>
        /// Geri İade
        /// </summary>
        /// <returns></returns>
        GVPSResponse Refund();

        /// <summary>
        /// Geri İade İptali
        /// </summary>
        /// <param name="RefundRetrefNum"></param>
        /// <returns></returns>
        GVPSResponse RefundCancel(string RefundRetrefNum);

        /// <summary>
        /// İptal
        /// </summary>
        /// <param name="RetrefNum"></param>
        /// <returns></returns>
        GVPSResponse Cancel(string RetrefNum);

        /// <summary>
        /// Önotorizasyon
        /// </summary>
        /// <returns></returns>
        GVPSResponse Preauth();

        /// <summary>
        /// Önotorizasyon Tamamlama/Onaylama
        /// </summary>
        /// <param name="RetrefNum">Önotorizasyon Numarası</param>
        /// <returns></returns>
        GVPSResponse Postauth(string RetrefNum);

        /// <summary>
        /// Önotorizasyon Tamamlama/Onaylama İptali
        /// </summary>
        /// <param name="RetrefNum">Önotorizasyon Numarası</param>
        /// <returns></returns>
        GVPSResponse PostauthCancel(string RetrefNum);

        /// <summary>
        /// Kredi Kartı TC Kimlik No Doğrulaması.
        /// </summary>
        /// <param name="TCKN">TC Kimlik No</param>
        /// <returns></returns>
        GVPSResponse Verification(string TCKN);

        /// <summary>
        /// 3D Secure destekli satış
        /// </summary>
        /// <returns></returns>
        XmlElement Sale3DRequest(string StoreKeyFor3D, Uri SuccessUri, Uri FailUri, ushort RefreshTime = 0, string Lang = "tr");
    }
}
