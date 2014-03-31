using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GarantiVP
{
    public interface IGarantiVPBuilder
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="posUrl"></param>
        /// <param name="mode">TEST, PROD</param>
        /// <returns></returns>
        IGarantiVPBuilder Server(string posUrl);

        /// <summary>
        /// İşlemler Test sunucusuna yönlendirilir.
        /// </summary>
        /// <returns></returns>
        IGarantiVPBuilder Test(bool test = true);

        /// <summary>
        /// Firma bilgileri
        /// </summary>
        /// <param name="terminalId"></param>
        /// <param name="MerchantID"></param>
        /// <param name="userID"></param>
        /// <param name="userPassword"></param>
        /// <returns></returns>
        IGarantiVPBuilder Company(string terminalId, string MerchantID, string userID, string userPassword);

        /// <summary>
        /// Müşteri Bilgileri
        /// </summary>
        /// <param name="customerMail">Müşteri eposta adresi</param>
        /// <param name="customerIP">Müşterinin IP adresi</param>
        /// <returns></returns>
        IGarantiVPBuilder Customer(string customerMail, string customerIP);


        IGarantiVPBuilder CreditCard(string number, string cvv2, int month, int year);

        /// <summary>
        /// Sipariş Numarası. Boş bırakılırsa sistem otomatik bir numara belirler
        /// </summary>
        /// <param name="orderID"></param>
        /// <param name="groupID"></param>
        /// <returns></returns>
        IGarantiVPBuilder Order(string orderID, string groupID = "");

        /// <summary>
        /// Kredi Kartından çekilecek tutar
        /// </summary>
        /// <param name="totalAmount">Tutar</param>
        /// <param name="currencyCode">Para Birimi. Varsayılan: TRL</param>
        /// <returns></returns>
        IGarantiVPBuilder Amount(double totalAmount, CurrencyCode currencyCode = CurrencyCode.TRL);

        /// <summary>
        /// Taksitli işlemlerde işlem tutarının üzerinden belli bir oranda peşinat alınmasının sağlanması için kullanılan işlemdir.
        /// </summary>
        /// <param name="rate">Peşinat oranı yüzde olarak belirlenir. %10 ise 10 girilir</param>
        /// <returns></returns>
        IGarantiVPBuilder DownPaymentRate(int rate);

        /// <summary>
        /// Taksitli İşlem
        /// </summary>
        /// <param name="installment">Taksit Sayısı</param>
        /// <returns></returns>
        IGarantiVPBuilder Installment(int installment);

        /// <summary>
        /// Ötelemeli Satış
        /// </summary>
        /// <param name="day">Ötelenecek gün sayısı.</param>
        /// <returns></returns>
        IGarantiVPBuilder Delay(int day);

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
    }
}
