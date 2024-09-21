using System;
using System.Data;
using System.Globalization;
using System.Net;
using System.Net.Mail;
using System.Reflection;
using FluentEmail.Core;
using FluentEmail.Razor;
using FluentEmail.Smtp;
using PhoneNumbers;
using SoKHCNVTAPI.Models;
using SoKHCNVTAPI.Models.Base;

namespace SoKHCNVTAPI.Helpers;

public static class Utils
{
    public static bool IsValidEmail(string? email)
    {
        if (string.IsNullOrWhiteSpace(email))
        {
            return false;
        }

        try
        {
            var addr = new MailAddress(email);
            return addr.Address == email;
        }
        catch
        {
            return false;
        }
    }

    public static string FormatPhoneNumber(string phone)
    {
        var phoneNumberUtil = PhoneNumberUtil.GetInstance();
        var phoneNumber = phoneNumberUtil.Parse(phone, "VN");
        return phoneNumberUtil.Format(phoneNumber, PhoneNumberFormat.E164);
    }

    public static bool IsValidPhoneNumber(string? phone)
    {
        if (string.IsNullOrWhiteSpace(phone))
        {
            return false;
        }

        try
        {
            var phoneNumberUtil = PhoneNumberUtil.GetInstance();
            return phoneNumberUtil.IsPossibleNumber(phoneNumberUtil.Parse(phone, "VN"));
        }
        catch
        {
            return false;
        }
    }

    public static DataTable ToDataTable<T>(List<T> items)
    {
        var dataTable = new DataTable(typeof(T).Name);
        //Get all the properties
        var props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
        foreach (var prop in props)
        {
            //Setting column names as Property names
            dataTable.Columns.Add(prop.Name);
        }

        foreach (var item in items)
        {
            var values = new object?[props.Length];
            for (var i = 0; i < props.Length; i++)
            {
                //inserting property values to datatable rows
                values[i] = props[i].GetValue(item, null);
            }

            dataTable.Rows.Add(values);
        }

        //put a breakpoint here and check datatable
        return dataTable;
    }

    /// 
    /// Chuyển phần nguyên của số thành chữ
    /// 
    /// Số double cần chuyển thành chữ
    /// Chuỗi kết quả chuyển từ số
    public static string NumberToVietNamText(double inputNumber, bool suffix = true)
    {
        string[] unitNumbers = new string[] { "không", "một", "hai", "ba", "bốn", "năm", "sáu", "bảy", "tám", "chín" };
        string[] placeValues = new string[] { "", "nghìn", "triệu", "tỷ" };
        bool isNegative = false;
        // -12345678.3445435 => "-12345678"
        string sNumber = inputNumber.ToString("#");
        double number = Convert.ToDouble(sNumber);
        if (number < 0)
        {
            number = -number;
            sNumber = number.ToString("");
            isNegative = true;
        }

        int ones, tens, hundreds;

        int positionDigit = sNumber.Length;   // last -> first

        string result = " ";

        if (positionDigit == 0)
            result = unitNumbers[0] + result;
        else
        {
            // 0:       ###
            // 1: nghìn ###,###
            // 2: triệu ###,###,###
            // 3: tỷ    ###,###,###,###
            int placeValue = 0;

            while (positionDigit > 0)
            {
                // Check last 3 digits remain ### (hundreds tens ones)
                tens = hundreds = -1;
                ones = Convert.ToInt32(sNumber.Substring(positionDigit - 1, 1));
                positionDigit--;
                if (positionDigit > 0)
                {
                    tens = Convert.ToInt32(sNumber.Substring(positionDigit - 1, 1));
                    positionDigit--;
                    if (positionDigit > 0)
                    {
                        hundreds = Convert.ToInt32(sNumber.Substring(positionDigit - 1, 1));
                        positionDigit--;
                    }
                }

                if ((ones > 0) || (tens > 0) || (hundreds > 0) || (placeValue == 3))
                    result = placeValues[placeValue] + result;

                placeValue++;
                if (placeValue > 3) placeValue = 1;

                if ((ones == 1) && (tens > 1))
                    result = "một " + result;
                else
                {
                    if ((ones == 5) && (tens > 0))
                        result = "lăm " + result;
                    else if (ones > 0)
                        result = unitNumbers[ones] + " " + result;
                }
                if (tens < 0)
                    break;
                else
                {
                    if ((tens == 0) && (ones > 0)) result = "lẻ " + result;
                    if (tens == 1) result = "mười " + result;
                    if (tens > 1) result = unitNumbers[tens] + " mươi " + result;
                }
                if (hundreds < 0) break;
                else
                {
                    if ((hundreds > 0) || (tens > 0) || (ones > 0))
                        result = unitNumbers[hundreds] + " trăm " + result;
                }
                result = " " + result;
            }
        }
        result = result.Trim();
        if (isNegative) result = "Âm " + result;
        return result + (suffix ? " đồng chẵn" : "");
    }


    // Return diff two before and after
    /// <summary>
    /// //
    /// </summary>
    /// <param name="oldArray"></param>
    /// <param name="newArray"></param>
    /// <returns></returns>
    public static ResultTwoValCompare<T> CompareTwoDiffArray<T>(List<T> oldArray, List<T> newArray)
    {

        var firstArray = new List<T>();
        var secondArray = new List<T>();

        if (oldArray.Count <= 0 && newArray.Count <= 0)
        {
            return new ResultTwoValCompare<T>
            {
                firstArray = firstArray,
                secondArray = secondArray
            };
        }

        if (oldArray.Count <= 0)
        {
            return new ResultTwoValCompare<T>
            {
                firstArray = firstArray,
                secondArray = newArray
            };
        };


        if (newArray.Count <= 0)
        {
            return new ResultTwoValCompare<T>
            {
                firstArray = oldArray,
                secondArray = secondArray
            };
        };


        foreach (T item in oldArray)
        {
            var isCheckExistItem = newArray.Contains(item);

            if (!isCheckExistItem)
            {
                firstArray.Add(item);
            }
        }

        foreach (T item in newArray)
        {
            var isCheckExistItem = oldArray.Contains(item);

            if (!isCheckExistItem)
            {
                secondArray.Add(item);
            }
        }
        return new ResultTwoValCompare<T>
        {
            firstArray = firstArray,
            secondArray = secondArray
        };
    }

    public static string GetUUID()
    {
        Guid myUuid = Guid.NewGuid();
        return myUuid.ToString();
    }

    public static bool SendEmailAction(string email, string cc, string subject, string attachedFile)
    {
        SmtpClient smtp = new SmtpClient
        {
            //smtp Server address
            Host = "in-v3.mailjet.com",
            Port = 587,
            UseDefaultCredentials = false,
            DeliveryMethod = SmtpDeliveryMethod.Network,
            // Enter here that you are sending smtp User name and password for the server 
            Credentials = new NetworkCredential("7a6d4404a7c1b3ae22b05c3fc6372b48", "cc7f0253d768d0d1ec9e4af960bd074d"),
            //EnableSsl = true
        };

        Email.DefaultSender = new SmtpSender(smtp);

        Email.DefaultRenderer = new RazorRenderer();
        //var template = "Xin chào, @Model.Fullname You are totally @Model.WorkflowStepName.";

        var tem = $"{Directory.GetCurrentDirectory()}/Templates/Email/workflow_step_action.cshtml";

        var sendReponse = Email
            .From("neolock18@gmail.com")
            .To(email)
            //.CC(cc)
            .Subject(subject)
            //.UsingTemplate(template, new { Fullname = "Công Nguyễn", WorkflowStepName = "SKHCN" }
            .UsingTemplateFromFile(tem, new { Fullname = "Công Nguyễn", WorkflowStepName = "SKHCN" })
            
            //.Body("This is the second email")
            .Send();

        return sendReponse.Successful;
    }

    public static DateTime getCurrentDate()
    {
       return TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.FindSystemTimeZoneById("Asia/Ho_Chi_Minh"));
    }
}