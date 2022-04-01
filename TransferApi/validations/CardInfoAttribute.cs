using System;

using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
sealed public class CardInfoMaskAttribute : ValidationAttribute
{


    public override bool IsValid(object value)
    {
        var cardInfo = (TransferApi.Models.CartInfo)value;
        bool result = true;

        result = MatchesMask(cardInfo.CardNumber, cardInfo.ExpireDate, cardInfo.CVV2.ToString());

        return result;
    }

    internal bool MatchesMask(string cardNo, string expiryDate, string cvv)


    {
        var cardCheck = new Regex(@"^(1298|1267|4512|4567|8901|8933)([\-\s]?[0-9]{4}){3}$");
        var monthCheck = new Regex(@"^(0[1-9]|1[0-2])$");
        var yearCheck = new Regex(@"^20[0-9]{2}$");
        var cvvCheck = new Regex(@"^\d{3}$");

        if (!cardCheck.IsMatch(cardNo)) // <1>check card number is valid
            return false;
        if (!cvvCheck.IsMatch(cvv)) // <2>check cvv is valid as "999"
            return false;

        var dateParts = expiryDate.Split('/'); //expiry date in from MM/yyyy            
        if (!monthCheck.IsMatch(dateParts[0]) || !yearCheck.IsMatch(dateParts[1])) // <3 - 6>
            return false; // ^ check date format is valid as "MM/yyyy"

        var year = int.Parse(dateParts[1]);
        var month = int.Parse(dateParts[0]);
        var lastDateOfExpiryMonth = DateTime.DaysInMonth(year, month); //get actual expiry date
        var cardExpiry = new DateTime(year, month, lastDateOfExpiryMonth, 23, 59, 59);

        //check expiry greater than today & within next 6 years <7, 8>>
        return (cardExpiry > DateTime.Now && cardExpiry < DateTime.Now.AddYears(6));
    }




}