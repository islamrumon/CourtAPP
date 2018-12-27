using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CourtApp.halper
{
    public class convertDate
    {
        public static string toBangla(string enNumInp)
        {
            //enNumInp = this.txtboxInput.Text.Trim();  //12-February-2018
            char[] bnNum = { '০', '১', '২', '৩', '৪', '৫', '৬', '৭', '৮', '৯' };
            string[] bn = { "জানু", "ফেব্রু", "মার্চ", "এপ্রি", "মে", "জুন", "জুলা", "আগ", "সেপ্টে", "অক্টো", "নভে", "ডিসে" };
            string[] bnFull = { "জানুয়ারি", "ফেব্রুয়ারি", "মার্চ", "এপ্রিল", "মে", "জুন", "জুলাই", "আগস্ট", "সেপ্টেম্বর", "অক্টোবর", "নভেম্বর", "ডিসেম্বর" };
            string[] enFull = { "January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December" };
            //   string[] en = { "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec" };
            string banNumOutput = "";

            foreach (var c in enNumInp.ToArray())
            {
                char c1 = c;
                for (int i = 0; i <= 9; i++)
                {
                    if (c1.ToString() == i.ToString())
                    {
                        c1 = bnNum[i];
                        break;
                    }
                }
                banNumOutput += c1.ToString();  // 
            }
            for (int i = 0; i < 12; i++)
            {
                if (banNumOutput.Contains(enFull[i]))
                {
                    banNumOutput = banNumOutput.Replace(enFull[i], bnFull[i].ToString());
                    break;
                }
                else if (banNumOutput.Contains(enFull[i].Substring(0, 3)))
                {
                    banNumOutput = banNumOutput.Replace(enFull[i].Substring(0, 3).ToString(), bn[i].ToString());
                }
            }
            //this.lblShow.Content = banNumOutput;


            return banNumOutput;
        }
    }
}