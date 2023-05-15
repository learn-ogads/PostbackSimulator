using System.Security.Cryptography;
using System.Text.RegularExpressions;
using System.Web;
using Bogus;
using OfferSimulation.Models;

namespace OfferSimulation;

public class PostbackUrl
{
    private string _url;
    private readonly Offer _offer;
    private readonly string _ipAddress;
    private readonly Faker _faker;

    public PostbackUrl(string url, Offer offer, string ipAddress)
    {
        _url = url;
        _offer = offer;
        _ipAddress = ipAddress;
        _faker = new Faker();
    }

    /// <summary>
    /// Replaces all possible parameters in the URL and returns the updated URL
    /// For example this would convert as follows:
    /// https://website.com/postback.php?id={offer_id}&payout={payout}&ip={session_ip}&offer_name={offer_name}&datetime={datetime}&ran={ran}
    /// https://website.com/postback.php?id=49377&payout=6.59&ip=0.0.0.0&offer_name=WWE+Champions&datetime=2023-05-11 12:14:04&ran=78639
    /// </summary>
    public string ReplaceAll()
    {
        ReplaceOfferId();
        ReplaceOfferName();
        ReplaceAffiliateId();
        ReplaceSource();
        ReplaceSessionIp();
        ReplacePayout();
        ReplaceDate();
        ReplaceTime();
        ReplaceDateTime();
        ReplaceSessionTimeStamp();
        ReplaceAffSub();
        ReplaceAffSub2();
        ReplaceAffSub3();
        ReplaceAffSub4();
        ReplaceAffSub5();
        ReplaceRan();
        return _url;
    }

    private void ReplaceOfferId() => _url = _url.Replace("{offer_id}", _offer.OfferId.ToString());

    private void ReplaceOfferName() => _url = _url.Replace("{offer_name}", HttpUtility.UrlEncode(_offer.NameShort));

    private void ReplaceAffiliateId()
    {
        var random = new Random();
        var affId = random.Next(1, 100000);
        _url = _url.Replace("{affiliate_id}", affId.ToString());
    }

    private void ReplaceSource()
    {
        if (_url.Contains("{source}"))
        {
            Console.Write("Provide the source or press enter to leave blank:");
            var source = Console.ReadLine();
            _url = _url.Replace("{source}", source);
        }
    }

    private void ReplaceSessionIp() => _url = _url.Replace("{session_ip}", _ipAddress);

    private void ReplacePayout() => _url = _url.Replace("{payout}", _offer.Payout);

    private void ReplaceDate()
    {
        var date = DateTime.Now.ToString("yyyy-MM-dd");
        _url = _url.Replace("{date}", date);
    }

    private void ReplaceTime()
    {
        var time = DateTime.Now.ToString("HH:mm:ss");
        _url = _url.Replace("{time}", time);
    }

    private void ReplaceDateTime()
    {
        var dateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        _url = _url.Replace("{datetime}", dateTime);
    }

    private void ReplaceSessionTimeStamp()
    {
        var timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        _url = _url.Replace("{session_timestamp}", timestamp);
    }

    private void ReplaceAffSub()
    {
        _url = _url.Replace("{aff_sub}", "ContentLocker");
    }

    /// <summary>
    /// AffSub2 is a randomly generated 5 numerical and character string.
    /// This isn't the exact way OGAds does it, but it gives the end user the right idea.
    /// </summary>
    private void ReplaceAffSub2()
    {
        var bytes = RandomNumberGenerator.GetBytes(5);
        var token = Convert.ToBase64String(bytes);

        // Remove special characters and shorten string
        var reg = new Regex("[*'\",_&#^@=]");
        token = reg.Replace(token, string.Empty);
        token = token[..5];

        _url = _url.Replace("{aff_sub2}", token);
    }

    /// <summary>
    /// AffSub3 is a long JWT token.
    /// While this isn't a JWT token, it gives the end user the right idea.
    /// </summary>
    private void ReplaceAffSub3()
    {
        var bytes = RandomNumberGenerator.GetBytes(20);
        var token = Convert.ToBase64String(bytes);
        
        // Remove special characters and shorten string
        var reg = new Regex("[*'\",_&#^@=]");
        token = reg.Replace(token, string.Empty);
        
        _url = _url.Replace("{aff_sub3}", token);
    }
    
    /// <summary>
    /// AffSub4 is randomly generated or can be provided by the end user.
    /// This only triggers if aff_sub4 is in the url.
    /// </summary>
    private void ReplaceAffSub4()
    {
        if (_url.Contains("{aff_sub4}"))
        {
            // Check if the user wants to manually provide or generate the aff_sub4
            Console.Write("Generate aff_sub4? (y/n):");
            var key = Console.ReadKey().Key;
            Console.Write("\n");
        
            string? affSub4;
            if (key == ConsoleKey.N)
            {
                Console.Write("Provide aff_sub4, or press enter to leave blank:");
                affSub4 = Console.ReadLine();
            }
            else
            {
                affSub4 = _faker.Internet.Email();
            }
            
            _url = _url.Replace("{aff_sub4}", affSub4);
        }
    }
    
    /// <summary>
    /// AffSub5 is randomly generated or provided by the user.
    /// This only triggers if aff_sub5 is in the url.
    /// </summary>
    private void ReplaceAffSub5()
    {
        if (_url.Contains("{aff_sub5}"))
        {
            // Check if the user wants to manually provide or generate the aff_sub5
            Console.Write("Generate aff_sub5? (y/n):");
            var key = Console.ReadKey().Key;
            Console.Write("\n");

            string? affSub5;
            if (key == ConsoleKey.N)
            {
                Console.WriteLine("Provide aff_sub5, or press enter to leave blank:");
                affSub5 = Console.ReadLine();
            }
            else
            {
                affSub5 = _faker.Person.FirstName;
            }

            _url = _url.Replace("{aff_sub5}", affSub5);
        }
    }
    
    private void ReplaceRan()
    {
        var random = new Random();
        var ranNum = random.Next(1, 100000);
        _url = _url.Replace("{ran}", ranNum.ToString());
    }
}